using System;
using System.Collections;
using System.Collections.Generic;
namespace HaoRestaurant.EditedScripts
{
    public class TableRequest:IEnumerable,IDisposable
    {
        private readonly Dictionary<string, List<IMenuItem>> Items = new Dictionary<string, List<IMenuItem>>();
        public void Add<T>(string customerName)
        {
            if (Items.Count==0)
            {
                Items.Add(customerName,new List<IMenuItem>());
            }
            Items[customerName].Add((IMenuItem) Activator.CreateInstance<T>());
        }

        public List<IMenuItem> Get<T>()
        {
            List<IMenuItem> resList = new List<IMenuItem>();
            foreach( string key in Items.Keys)
            {
                foreach (var order in Items[key])
                {
                    if (order.GetType()==typeof(T))
                    {
                        resList.Add(order);
                    }
                }
            }
            return resList;
        }
        private List<IMenuItem> this[string customerName] => Items[customerName];

        private IEnumerator GetEnumerator()
        {
            foreach (var V in Items.Keys)
            {
                yield return this[V];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}