namespace Framework
{
    public interface IValueContainer<TKey> where TKey : struct
    {
        public void SetValue<T>(TKey key, T value);
        public T GetValue<T>(TKey key);
    }
}
