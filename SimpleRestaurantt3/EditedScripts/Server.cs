using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HaoRestaurant.EditedScripts
{
    public static class Server
    {
        //TODO: remove events which comes from project #4
        public delegate void ready(List<string> lis);
        public static event ready Ready;

        private static readonly TableRequest TableRequest = new();

        private static List<Cook> _cooks = new();

        //TODO: It will be better to decouple server and cook classes. It means, we should not create new server in cook class and not create new cook in server class.
        public static async void Send()
        {
            var cook = _cooks.SingleOrDefault(c => !c.IsBussed);
            if (cook != null)
                await cook.Process(TableRequest);
            else
            {
                //TODO: Do not create free cook in every "send"
                Cook newCook = new Cook();
                newCook.Serve += Serve;
                await newCook.Process(TableRequest);
                _cooks.Add(newCook);
            }
            Thread.Sleep(1000);
        }

        public static void Receive(string customerName, int eggQuantity, int chickenQuantity, Type waterType)
        {
            if (waterType == typeof(Coca_Cola))
            {
                TableRequest.Add<Coca_Cola>(customerName);
            }
            else if (waterType == typeof(Pepsi))
            {
                TableRequest.Add<Pepsi>(customerName);
            }
            else
            {
                TableRequest.Add<Tea>(customerName);
            }

            for (int i = 0; i < eggQuantity; i++)
            {
                TableRequest.Add<Egg>(customerName);
            }

            for (int i = 0; i < chickenQuantity; i++)
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
                string drink = null;
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
                res.Add("Customer " + customerName + " is served " + drink + ", " + chickenQuantity + " chicken, " + eggQuantity + " egg");
            }

            Ready.Invoke(res);
        }
    }
}