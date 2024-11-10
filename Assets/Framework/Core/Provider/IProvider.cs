namespace Framework.Core
{
    public interface IProvider<T>
    {
        public T Get();
    }
}
