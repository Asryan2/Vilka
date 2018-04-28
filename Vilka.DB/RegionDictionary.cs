using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.DB
{
    public class RegionDictionary : IDictionary<string, Region>
    {
        private VilkaEntities context = new VilkaEntities();

        private static RegionDictionary _instance;

        private RegionDictionary()
        {

        }

        public static RegionDictionary GetInstance()
        {
            if (_instance == null)
                _instance = new RegionDictionary();
            return _instance;
        }

        public Region this[string key]
        {
            get
            {
                Region l = null;
                if (!TryGetValue(key, out l))
                    throw new ArgumentOutOfRangeException();
                return l;
            }
            set
            {
                Region l = null;
                if (!TryGetValue(key, out l))
                {
                    Add(key, value);
                }
                else
                {
                    if (value == null || value.ID == 0) throw new ArgumentException("Region is invalid.");
                    context.RegionDictionaryElements.Where(el => el.RegionID == l.ID).First().RegionID = value.ID;
                    context.SaveChanges();
                }
            }
        }

        public int Count
        {
            get
            {
                return context.RegionDictionaryElements.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return context.RegionDictionaryElements.Select(el => el.Name).ToList();
            }
        }

        public ICollection<Region> Values
        {
            get
            {
                return context.RegionDictionaryElements.Select(el => el.Region).ToList();
            }
        }

        public void Add(KeyValuePair<string, Region> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, Region value)
        {
            RegionDictionaryElement newElement = new RegionDictionaryElement();
            newElement.Name = key;
            if (value == null) newElement.Region = null;
            else newElement.RegionID = value.ID;
            context.RegionDictionaryElements.Add(newElement);
            context.SaveChanges();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, Region> item)
        {
            Region l = null;
            if (!TryGetValue(item.Key, out l))
                return false;
            if (item.Value.ID != l.ID)
                return false;
            return true;
        }

        public bool ContainsKey(string key)
        {
            Region l = null;
            if (!TryGetValue(key, out l))
                return false;
            return true;
        }

        public void CopyTo(KeyValuePair<string, Region>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Region>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, Region> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key))
                return false;
            context.RegionDictionaryElements.Remove(context.RegionDictionaryElements.Where(el => el.Name == key).First());
            return context.SaveChanges() != 0;
        }

        public bool TryGetValue(string key, out Region value)
        {
            RegionDictionaryElement element = context.RegionDictionaryElements.Where((el) => el.Name == key).FirstOrDefault();
            value = element?.Region;
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
