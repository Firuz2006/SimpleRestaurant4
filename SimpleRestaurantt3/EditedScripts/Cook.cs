namespace HaoRestaurant.EditedScripts
{
    public class Cook
    {
        public void Process(TableRequest tableRequest)
        {
            var ChickenAll = tableRequest[typeof(Chicken)];

            var EggAll = tableRequest[typeof(Egg)];
            
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