using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.DB
{
	public class LeagueDictionary : IDictionary<Tuple<string, Region, Sport>, League>
	{


		private VilkaEntities context = new VilkaEntities();

        private static LeagueDictionary _instance;

        public ICollection<Tuple<string, Region, Sport>> Keys
        {
            get
            {
                return context.LeagueDictionaryElements.Select(el => new Tuple<string, Region, Sport>(el.Name, el.Region, el.Sport)).ToList();
            }
        }

        public ICollection<League> Values
        {
            get
            {
                return context.LeagueDictionaryElements.Select(el => el.League).ToList();
            }
        }

        public int Count
        {
            get
            {
                return context.LeagueDictionaryElements.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public League this[Tuple<string, Region, Sport> key]
        {
            get
            {
                League l = null;
                if (!TryGetValue(key, out l))
                    throw new ArgumentOutOfRangeException();
                return l;
            }

            set
            {
                League l = null;
                if (!TryGetValue(key, out l))
                {
                    Add(key, value);
                }
                else
                {
                    if (value == null || value.ID == 0) throw new ArgumentException("League is invalid.");

                    context.LeagueDictionaryElements.Where(el => el.Name == key.Item1 && el.SportID == key.Item2.ID && el.RegionID == key.Item3.ID).First().LeagueID = value.ID;
                    context.SaveChanges();
                }
            }
        }

        private LeagueDictionary()
        {

        }

        public static LeagueDictionary GetInstance()
        {
            if (_instance == null)
                _instance = new LeagueDictionary();
            return _instance;
        }

        public bool ContainsKey(Tuple<string, Region, Sport> key)
        {
            League l = null;
            if (!TryGetValue(key, out l))
                return false;
            return true;
        }

        public void Add(Tuple<string, Region, Sport> key, League value)
        {
            LeagueDictionaryElement newElement = new LeagueDictionaryElement();
            newElement.Name = key.Item1;
            newElement.SportID = key.Item3.ID;
            newElement.RegionID = key.Item2.ID;
            if (value == null) newElement.League = null;
            else newElement.LeagueID = value.ID;
            context.LeagueDictionaryElements.Add(newElement);
            context.SaveChanges();
        }

        public bool Remove(Tuple<string, Region, Sport> key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Tuple<string, Region, Sport> key, out League value)
        {
            LeagueDictionaryElement element = context.LeagueDictionaryElements.Where((el) => el.Name == key.Item1 && el.SportID == key.Item3.ID && el.RegionID == key.Item2.ID).FirstOrDefault();
            value = element?.League;
            if (element == null)
                return false;
            return true;
        }

        public void Add(KeyValuePair<Tuple<string, Region, Sport>, League> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<Tuple<string, Region, Sport>, League> item)
        {
            League l = null;
            if (!TryGetValue(item.Key, out l))
                return false;
            if (item.Value.ID != l.ID)
                return false;
            return true;
        }

        public void CopyTo(KeyValuePair<Tuple<string, Region, Sport>, League>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Tuple<string, Region, Sport>, League> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Tuple<string, Region, Sport>, League>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
