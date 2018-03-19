using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;
using Vilka.Helpers;
using VilkaConsole.Mappings;
using WebSocketSharp;

namespace VilkaConsole.Betters
{
	public class BetterVivaro
	{
		private BetterMappings mappings = BetterMappingsFactory.Get(typeof(BetterVivaroMappings));
		private VilkaEntities context = new VilkaEntities();

		private string sessionID;
		private string getReq;
		private string sessionReq;
		private string wsURL;

		public BetterVivaro()
		{
			wsURL = "wss://eu-swarm-ws.betconstruct.com/";
			sessionID = "eyJpZCI6IjQzZjk0NjZmM2VjNjEyMzgwOWJhNTkwNDM3Y2JjNWNiIiwidHMiOjE1MTgyNTE5MTM0ODd9";
			sessionReq =  "{\"command\":\"request_session\",\"params\":{\"language\":\"eng\",\"site_id\":1,\"afec\":\"" + sessionID + "\"}}";
			getReq = "{\"command\":\"get\",\"params\":{\"source\":\"betting\",\"what\":{\"sport\":[\"id\",\"name\",\"alias\",\"order\"],\"competition\":[\"id\",\"order\",\"name\"],\"region\":[\"id\",\"name\",\"alias\",\"order\"],\"game\":[[\"id\",\"start_ts\",\"team1_name\",\"team2_name\",\"team1_reg_name\",\"team2_reg_name\",\"type\",\"info\",\"events_count\",\"markets_count\",\"is_blocked\",\"stats\",\"tv_type\",\"video_id\",\"video_id2\",\"video_id3\",\"video_provider\",\"is_stat_available\",\"show_type\",\"game_external_id\",\"team1_external_id\",\"team2_external_id\"]],\"market\":[\"base\",\"type\",\"name\",\"express_id\"],\"event\":[]},\"where\":{},\"subscribe\":false}}";

		}
		private WebSocket socket;
		private JObject Data { get; set; }
		private bool dataArrived
		{
			get
			{
				return Data != null;
			}
		}
		public async void FillDB()
		{
			
			socket = new WebSocket(wsURL);
			socket.Connect();
			socket.OnMessage += Socket_OnMessage;
			socket.Send(sessionReq);
			while (!dataArrived)
			{
				Task.Delay(1000).ConfigureAwait(false);
			}
			foreach(var sport in Data["sport"].First)
			{
				if (!mappings.SportMappings.ContainsKey(sport["alias"].ToObject<string>())) continue;
				foreach (var region in sport["region"])
				{
					JObject regionObj = (JObject)region.First;
					string regionName = regionObj["alias"].ToObject<string>();
					foreach (var competition in regionObj["competition"])
					{
						JObject league = (JObject)competition.First;
						string leagueName = league["name"].ToObject<string>();
						foreach(var game in league["game"])
						{
							try
							{
								Event ev = new Event();
								JObject gameObj = (JObject)game.First;
								ev.Home = gameObj["team1_name"].ToObject<string>();
								ev.Away = gameObj["team2_name"].ToObject<string>();
								ev.Region = regionName;
								ev.League = leagueName;
								ev.Start = DateHelpers.TimeStampToDateTime(gameObj["start_ts"].ToObject<double>());
								var a = gameObj["start_ts"];
								ev.PrematchEnd = ev.Start;
								if (ev.IsLive()) continue;
								ev.SportID = mappings.SportMappings[sport["alias"].ToObject<string>()].ID;
								Event dbEvent = DBFinder.FindEvent(ev).DBCopy(context);
								if (dbEvent != null)
								{
									Console.WriteLine("Found: " + dbEvent.Home + " - " +dbEvent.Away) ;
									ev = dbEvent;
								}
								else
								{

									context.Events.Add(ev);
									if(context.SaveChanges() > 0)
										Console.WriteLine("Added: " + ev.Home + " - " + ev.Away);
								}
								foreach (var market in gameObj["market"])
								{
									JObject marketObj = (JObject)market.First;
									if (!mappings.BetTypeMappings.ContainsKey(marketObj["type"].ToObject<string>())) continue;
									BetOffer offer = new BetOffer();
									offer.BetType = mappings.BetTypeMappings[marketObj["type"].ToObject<string>()].DBCopy(context);
									if (!mappings.BetTargetMappings.ContainsKey(marketObj["name"].ToObject<string>())) continue;
									offer.BetTarget = mappings.BetTargetMappings[marketObj["name"].ToObject<string>()].DBCopy(context);
									offer.EventID = ev.ID;
									context.BetOffers.Add(offer);
									context.SaveChanges();
								}
							}
							catch
							{

								continue;
							}
							
						}
					}

				}
			}
			
		}

		private void Socket_OnOpen(object sender, EventArgs e)
		{
			
			
		}

		private void Socket_OnMessage(object sender, MessageEventArgs e)
		{
			JObject response = JObject.Parse(e.Data);
			if (response.ContainsKey("data") && ((JObject)response["data"]).ContainsKey("sid"))
				socket.Send(getReq);
			else if(response.ContainsKey("data") && ((JObject)response["data"]).ContainsKey("data"))
			{
				Data = (JObject)((JObject)response["data"])["data"];
			}
			
		}

		public List<BetOffer> GetEventBetOffers(int eventID)
		{
			throw new NotImplementedException();
		}
	}
}
