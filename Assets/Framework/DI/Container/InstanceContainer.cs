namespace Framework.DI
{
    internal class InstanceContainer : IBindContainer
    {

        private object _instance;

        internal InstanceContainer(object o)
        {
            _instance = o;
        }

        object IBindContainer.GetInstance()
        {
            return _instance;
        }
    }
}
