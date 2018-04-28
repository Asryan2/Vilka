using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.DB
{
	class LeagueDictionary : IDictionary<Tuple<string, Sport>, League>
	{
		private VilkaEntities context = new VilkaEntities();

        public League this[Tuple<string, Sport> key]
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

                    context.LeagueDictionaryElements.Where(el => el.Name == key.Item1 && el.SportID == key.Item2.ID).First().LeagueID = value.ID;
                    context.SaveChanges();
                }
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

        public ICollection<Tuple<string, Sport>> Keys
        {
            get
            {
                return context.LeagueDictionaryElements.Select(el => new Tuple<string, Sport>(el.Name, el.Sport)).ToList();
            }
        }

        public ICollection<League> Values
        {
            get
            {
                return context.LeagueDictionaryElements.Select(el => el.League).ToList();
            }
        }

        public void Add(KeyValuePair<Tuple<string, Sport>, League> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(Tuple<string, Sport> key, League value)
        {
            LeagueDictionaryElement newElement = new LeagueDictionaryElement();
            newElement.Name = key.Item1;
            newElement.SportID = key.Item2.ID;
            if (value == null) newElement.League = null;
            else newElement.LeagueID = value.ID;
            context.LeagueDictionaryElements.Add(newElement);
            context.SaveChanges();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<Tuple<string, Sport>, League> item)
        {
            League l = null;
            if (!TryGetValue(item.Key, out l))
                return false;
            if (item.Value.ID != l.ID)
                return false;
            return true;
        }

        public bool ContainsKey(Tuple<string, Sport> key)
        {
            League l = null;
            if (!TryGetValue(key, out l))
                return false;
            return true;
        }

        public void CopyTo(KeyValuePair<Tuple<string, Sport>, League>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Tuple<string, Sport>, League>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Tuple<string, Sport>, League> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Tuple<string, Sport> key)
        {
            if (!ContainsKey(key))
                return false;
            context.LeagueDictionaryElements.Remove(context.LeagueDictionaryElements.Where(el => el.Name == key.Item1 && el.SportID == key.Item2.ID).First());
            return context.SaveChanges() != 0;
        }

        public bool TryGetValue(Tuple<string, Sport> key, out League value)
        {
            LeagueDictionaryElement element = context.LeagueDictionaryElements.Where((el) => el.Name == key.Item1 && el.SportID == key.Item2.ID).FirstOrDefault();
            value = element?.League;
            if (element == null)
                return false;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
