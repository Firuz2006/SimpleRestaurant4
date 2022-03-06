using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace HaoRestaurant.EditedScripts
{
    public class Server
    {
        private readonly TableRequest tableRequest=new TableRequest();
        private readonly List<Drink> waters=new List<Drink>();
        private byte CustomerIndex = 0;
        private readonly Cook cook=new Cook();
        public void Receive(int EggQuantity,int ChickenQuantity,Drink WaterType)
        {
            if (CustomerIndex == 8)
            {
                MessageBox.Show("maximum 8");
                return;
            }
            waters.Add( WaterType);
            for (int i = 0; i <EggQuantity ; i++)
            {
                tableRequest.Add(CustomerIndex,new Egg());
            }
            for (int i = 0; i <ChickenQuantity ; i++)
            {
                tableRequest.Add(CustomerIndex,new Chicken());
            }
            
            CustomerIndex++;
        }

        public delegate void ReadyCook(string msg);

        public event ReadyCook Ready;
        
        public void SendToCook()
        {
            cook.Process(tableRequest);
        }

        public List<string> Serve()
        {
            int eggQuantity, chickenQuantity;
            List<string> res = new List<string>();
            for (byte i = 0; i < CustomerIndex; i++)
            {
                eggQuantity = 0;
                chickenQuantity = 0;
                var req = tableRequest[i];
                foreach (var order in req)
                {
                    if (order is Chicken)
                    {
                        chickenQuantity++;
                    }
                    else
                    {
                        eggQuantity++;
                    }
                }
                waters[i].Obtain();
                waters[i].Serve();
                res.Add("Customer " + i + " is served " + chickenQuantity + " chicken, " + eggQuantity + " egg, " +
                         waters[i].ToString().Split('.')[2]);
            }
            tableRequest.Reset();
            waters.Clear();
            CustomerIndex = 0;
            return res;
        }
    }
}