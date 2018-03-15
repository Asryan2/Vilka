using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VilkaConsole.Helpers;
using VilkaConsole.Mappings;

namespace VilkaConsole
{
	class Better888
	{
        private VilkaEntities context = new VilkaEntities();
		private string _allEventsApiUrl = "https://e4-api.kambi.com/offering/api/v3/888/listView/football.json?lang=en_GB&market=ZZ";
		private BetterMappings mappings = BetterMappingsFactory.Get(typeof(Better888Mappings));

        private string GetEventOffersApiUrl(int eventID)
        {
            return "https://e4-api.kambi.com/offering/api/v2/888/betoffer/event/" + eventID + ".json";
        }
		public void FillDB()
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_allEventsApiUrl);
			request.Method = "GET";
			string json;
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				json = reader.ReadToEnd();
				reader.Close();
				dataStream.Close();
			}
			JObject obj;
			try
			{
				 obj = JObject.Parse(json);
				foreach (var item in obj["events"])
				{
					JObject itemObj = ((JObject)item);
					JObject eventObj = (JObject)itemObj["event"];
					Event ev = new Event();
					ev.SiteID = mappings.ID;
					string eventData = eventObj["id"].ToObject<string>();

					ev.Home = eventObj["homeName"].ToObject<string>();
					ev.Away = eventObj["awayName"].ToObject<string>();
					ev.Start = DateHelpers.TimeStampToDateTime(eventObj["start"].ToObject<double>());
					ev.PrematchEnd = DateHelpers.TimeStampToDateTime(eventObj["prematchEnd"].ToObject<double>());
					if (ev.IsLive()) continue;
					JArray paths = ((JArray)eventObj["path"]);
					if (!mappings.SportMappings.ContainsKey(paths[0]["name"].ToObject<string>())) continue;
					Sport sport = mappings.SportMappings[paths[0]["name"].ToObject<string>()].DBCopy(context);
					if (sport != null)
						ev.Sport = sport;
					else
					{
						break;
					}
					if (paths.Count == 2)
					{
						ev.League = paths[1]["name"].ToObject<string>();
					}
					else if (paths.Count == 3)
					{
						ev.Region = paths[1]["name"].ToObject<string>();
						ev.League = paths[2]["name"].ToObject<string>();
					}
					Event dbEvent = DBFinder.FindEvent(ev).DBCopy(context);
					int eventID;
					if (dbEvent != null) continue;
					else
					{
						context.Events.Add(ev);
						context.SaveChanges();
						EventSiteData data = new EventSiteData();
						dbEvent = ev;
						data.EventID = ev.ID;
						data.SiteID = mappings.ID;
						data.Data = eventData;
						context.EventSiteDatas.Add(data);
						Int32.TryParse(eventData, out eventID);
					}
					if (eventID == 0) continue;
					string offersUrl = GetEventOffersApiUrl(eventID);
					request = (HttpWebRequest)WebRequest.Create(offersUrl);
					try
					{
						using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
						{
							Stream dataStream = response.GetResponseStream();
							StreamReader reader = new StreamReader(dataStream);
							json = reader.ReadToEnd();
							reader.Close();
							dataStream.Close();
						}
					}
					catch (WebException ex)
					{
						continue;
					}
					obj = JObject.Parse(json);
					foreach (JObject offer in obj["betoffers"])
					{
						string betOfferType = ((JObject)offer["betOfferType"])["name"].ToObject<string>();
						string betTarget = ((JObject)offer["criterion"])["label"].ToObject<string>();
						if (!mappings.BetTypeMappings.ContainsKey(betOfferType)) continue;
						if (!mappings.BetTargetMappings.ContainsKey(betTarget)) continue;
						BetType type = mappings.BetTypeMappings[betOfferType].DBCopy(context);
						BetTarget target = mappings.BetTargetMappings[betTarget].DBCopy(context);
						BetOffer betOffer = new BetOffer();
						betOffer.BetTypeID = type.ID;
						if(target != null) betOffer.BetTargetID = target.ID;
						dbEvent.BetOffers.Add(betOffer);
						context.SaveChanges();
						foreach (JObject outcome in offer["outcomes"])
						{
							Outcome o = new Outcome();
							if (!mappings.OutcomeTypeMappings.ContainsKey(outcome["label"].ToObject<string>())) continue;
							o.OutcomeType = mappings.OutcomeTypeMappings[outcome["label"].ToObject<string>()].DBCopy(context);
							o.Odds = outcome["oddsAmerican"].ToObject<string>();
							o.BetOfferID = betOffer.ID;
							if(outcome.ContainsKey("line")) o.Line = outcome["line"].ToObject<int>();
							context.Outcomes.Add(o);
						}
					}
					
				}
				context.SaveChanges();

			}
			catch (Exception ex)
			{
				Console.WriteLine("888 invalid json, or changed json formatting");
			}
		}

		public List<BetOffer> GetEventBetOffers(int eventID)
		{
            return new List<BetOffer>();
		}
	}
}
