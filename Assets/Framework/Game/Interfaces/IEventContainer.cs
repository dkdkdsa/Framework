namespace Framework
{
    public interface IEventContainer<TKey>
    {
        public delegate void Event(params object[] param);

        /// <summary>
        /// Event�� ����
        /// </summary>
        /// <param name="key">������ �̹�Ʈ�� Ű</param>
        /// <param name="evt">�̹�Ʈ �Լ�</param>
        public void RegisterEvent(TKey key, Event evt);

        /// <summary>
        /// Event������ ����
        /// </summary>
        /// <param name="key">������ �̹�Ʈ�� Ű</param>
        /// <param name="evt">�̹�Ʈ �Լ�</param>
        public void UnregisterEvent(TKey key, Event evt);

        /// <summary>
        /// ������ �̹�Ʈ�� ����
        /// </summary>
        /// <param name="key">������ Event�� Ű</param>
        public void NotifyEvent(TKey key, params object[] value);
    }
}
