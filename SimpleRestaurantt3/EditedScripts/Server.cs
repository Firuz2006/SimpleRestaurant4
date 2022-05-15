using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HaoRestaurant.EditedScripts
{
    public static class Server
    {
        public delegate void ready(List<string> lis);
        public static event ready Ready;

        private static readonly TableRequest TableRequest=new();
        
        private static List<Cook> _cooks=new();


        public static async void Send()
        {
            var cook = _cooks.SingleOrDefault(c => !c.IsBussed);
            if (cook != null)
                await cook.Process(TableRequest);
            else
            {
                Cook newCook = new Cook();
                newCook.Serve += Serve;
                await newCook.Process(TableRequest);
                _cooks.Add(newCook);
            }
            Thread.Sleep(1000);
        }
        
        public static void Receive(string customerName ,int eggQuantity,int chickenQuantity,Type waterType)
        {
            if (waterType == typeof(Coca_Cola))
            {
                TableRequest.Add<Coca_Cola>(customerName);
            }else if (waterType==typeof(Pepsi))
            {
                TableRequest.Add<Pepsi>(customerName);
            }
            else
            {
                TableRequest.Add<Tea>(customerName);
            }
             
            for (int i = 0; i <eggQuantity ; i++)
            {
                TableRequest.Add<Egg>(customerName);
            }
            
            for (int i = 0; i <chickenQuantity ; i++)
            {
                TableRequest.Add<Chicken>(customerName);
            }
        }
        private static void Serve(TableRequest TableRequest)
        {
            int eggQuantity, chickenQuantity;
            List<string> res = new List<string>();
            foreach (string customerName in TableRequest)
            {
                eggQuantity = 0;
                chickenQuantity = 0;
                string drink=null;
                foreach (IMenuItem order in TableRequest[customerName])
                {
                    if (order is Chicken)
                    {
                        chickenQuantity++;
                    }
                    else if (order is Egg)
                    {
                        eggQuantity++;
                    }
                    else if (order is Drink drinkType)
                    {
                        drink = drinkType.GetType().Name;
                        drinkType.Obtain();
                        drinkType.Serve();
                    }
                    
                }
                res.Add("Customer " +customerName +" is served " + drink+", " + chickenQuantity + " chicken, " + eggQuantity + " egg");
            }

            Ready.Invoke(res);
        }
    }
}