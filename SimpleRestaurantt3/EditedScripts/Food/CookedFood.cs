namespace HaoRestaurant.EditedScripts
{
    //TODO: actually Obtain, Serve and Cook methods should be abstract not virtual
    public abstract class CookedFood : IMenuItem
    {


        public virtual void Obtain()
        {
        }

        public virtual void Serve()
        {
        }

        public virtual void Cook()
        {

        }
    }
}