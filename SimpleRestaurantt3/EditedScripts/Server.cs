using System;
using System.Collections.Generic;
using System.Windows;
namespace HaoRestaurant.EditedScripts
{
    public class Server
    {
        private readonly TableRequest tableRequest=new TableRequest();

        // public TableRequest getTableRequest => tableRequest;

        private readonly List<Drink> waters=new List<Drink>();
        private byte CustomerIndex = 0;

        public delegate void ReadyCook(TableRequest tableRequest);

        public static event ReadyCook? Ready;

        public void Invoke()
        {
            Ready?.Invoke(tableRequest);
        }
        
        public Server()
        {
            Cook.Processed += Serve;
        }
        public void Receive(string customerName ,int EggQuantity,int ChickenQuantity,Type WaterType)
        {
            if (CustomerIndex == 8)
            {
                MessageBox.Show("maximum 8");
                return;
            }

            if (WaterType == typeof(Coca_Cola))
            {
                tableRequest.Add<Coca_Cola>(customerName);
            }else if (WaterType==typeof(Pepsi))
            {
                tableRequest.Add<Pepsi>(customerName);
            }
            else
            {
                tableRequest.Add<Tea>(customerName);
            }
            
            for (int i = 0; i <EggQuantity ; i++)
            {
                tableRequest.Add<Egg>(customerName);
            }
            
            for (int i = 0; i <ChickenQuantity ; i++)
            {
                tableRequest.Add<Chicken>(customerName);
            }
            
            CustomerIndex++;
        }
        
        public List<string> Serve()
        {
            int eggQuantity, chickenQuantity;
            List<string> res = new List<string>();
            for (byte i = 0; i < CustomerIndex; i++)
            {
                eggQuantity = 0;
                chickenQuantity = 0;
                string drink = "";
                foreach (var order in tableRequest)
                {
                    if (order is Chicken)
                    {
                        chickenQuantity++;
                    }
                    else if(order is Egg)
                    {
                        eggQuantity++;
                    }
                    else if(order is Coca_Cola cola)
                    {
                        drink = "CocaCola ";
                        cola.Obtain();
                        cola.Serve();
                    }
                    else if(order is Pepsi pepsi)
                    {
                        drink = "Pepsi ";
                        pepsi.Obtain();
                        pepsi.Serve();
                    }
                    else if(order is Tea tea)
                    {
                        drink = "Tea ";
                        tea.Obtain();
                        tea.Serve();
                    }
                }
                res.Add("Customer " + i + " is served " + drink + chickenQuantity + " chicken, " + eggQuantity + " egg");
            }
            tableRequest.Dispose();
            CustomerIndex = 0;
            return res;
        }
    }
}