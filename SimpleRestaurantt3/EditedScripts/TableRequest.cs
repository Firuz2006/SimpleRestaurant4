using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HaoRestaurant.EditedScripts
{
    public class TableRequest:IEnumerable<string>,IDisposable
    {
        private readonly Dictionary<string, List<IMenuItem>> _items = new Dictionary<string, List<IMenuItem>>();
        public void Add<T>(string customerName) where  T:IMenuItem ,new()
        {
            if (!_items.ContainsKey(customerName))
            {
                _items.Add(customerName,new List<IMenuItem>());
            }
            _items[customerName].Add(new T());
        }

        public List<IMenuItem> Get<T>()
        {
            return _items.Keys.SelectMany(key => _items[key]).OfType<T>().Cast<IMenuItem>().ToList();
        }
        public List<IMenuItem> this[string customerName] => _items[customerName];

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>) _items.Keys).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            _items.Clear();
        }
    }
}