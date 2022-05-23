using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HaoRestaurant.EditedScripts
{
    public class Cook
    {
        public delegate void serve(TableRequest tableRequest);
        //TODO: remove Serve event. The event should be in only project #4.
        public event serve Serve;
        public bool IsBussed { get; private set; }
        
        public async Task Process(TableRequest tableRequest)
        {
            IsBussed = true;
            Task t = new Task(() =>
            {
                var chickenAll = tableRequest.Get<Chicken>();
        
                var eggAll = tableRequest.Get<Egg>();
            
                foreach (var chicken in chickenAll)
                {
                    ((Chicken)chicken).Obtain();
                    ((Chicken)chicken).CutUp();
                    ((Chicken)chicken).Cook();
                }
                foreach (var egg in eggAll)
                {
                    ((Egg)egg).Obtain();
                    ((Egg)egg).Crack();
                    ((Egg)egg).Dispose();
                    ((Egg)egg).Cook();
                    ((Egg)egg).Serve();
                }
                Thread.Sleep(10000);
                IsBussed = false;
                Serve.Invoke(tableRequest);
            });
            t.Start();
            await t;
        }
    }
}