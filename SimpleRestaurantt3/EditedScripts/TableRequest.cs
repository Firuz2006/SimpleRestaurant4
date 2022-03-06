using System;
using System.Windows;
namespace HaoRestaurant.EditedScripts
{
    public class TableRequest
    {
        private IMenuItem[][] Items=new IMenuItem[0][];
        public void Add(int Customer, IMenuItem i)
        { 
            if (Items.Length <= Customer)
            {
                Array.Resize(ref Items,Customer+1);
                Items[Customer] = new IMenuItem[0];
            }
            Array.Resize(ref Items[Customer], Items[Customer].Length + 1);
            Items[Customer][Items[Customer].Length-1] = i;
            //MessageBox.Show(Items.Length.ToString());
        }

        public IMenuItem[] this[Type i]
        {
            get
            {
                IMenuItem[] menu=new IMenuItem[0];
                foreach (var Customer in Items)
                {
                    foreach (var Request in Customer)
                    {
                       if (i == Request.GetType())
                       {
                           Array.Resize(ref menu, menu.Length + 1);
                           menu[menu.Length-1] = Request;
                       }
                    }
                }
                return menu;
            }
        }
        public IMenuItem[] this[int Customer]
        {
            get
            {
                return Items[Customer];
            }
        }

        public void Reset()
        {
            Items = new IMenuItem[0][];
        }
    }
}