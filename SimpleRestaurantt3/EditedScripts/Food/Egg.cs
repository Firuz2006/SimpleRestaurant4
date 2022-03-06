using System;

namespace HaoRestaurant.EditedScripts
{
    public sealed class Egg:CookedFood
    {
        public bool Crack()
        {
            return getQuality() > 25;
        }

        private int getQuality()
        {
            return new Random().Next(30,100);
        }
        private void DiscardShell()
        {
            
        }

        public void Dispose()
        {
            DiscardShell();
        }
    }
}