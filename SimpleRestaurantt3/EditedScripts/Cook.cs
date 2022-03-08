using System.Collections.Generic;

namespace HaoRestaurant.EditedScripts
{
    public class Cook
    {
        
        public delegate List<string> processed();

        public static event processed Processed;
        
        public Cook()
        {
            Server.Ready += Process;
        }
        public void Process(TableRequest tableRequest)
        {
            var ChickenAll = tableRequest.Get<Chicken>();
        
            var EggAll = tableRequest.Get<Egg>();
            
            foreach (var chicken in ChickenAll)
            {
                ((Chicken)chicken).Obtain();
                ((Chicken)chicken).CutUp();
                ((Chicken)chicken).Cook();
            }
            foreach (var egg in EggAll)
            {
                ((Egg)egg).Obtain();
                ((Egg)egg).Crack();
                ((Egg)egg).Dispose();
                ((Egg)egg).Cook();
                ((Egg)egg).Serve();
            }
        
        } 
    }
}