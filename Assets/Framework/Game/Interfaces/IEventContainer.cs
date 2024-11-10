namespace Framework
{
    public interface IEventContainer<TKey>
    {
        public delegate void Event(params object[] param);

        /// <summary>
        /// Event를 구독
        /// </summary>
        /// <param name="key">발행할 이밴트의 키</param>
        /// <param name="evt">이밴트 함수</param>
        public void RegisterEvent(TKey key, Event evt);

        /// <summary>
        /// Event구독을 해제
        /// </summary>
        /// <param name="key">해제할 이밴트의 키</param>
        /// <param name="evt">이밴트 함수</param>
        public void UnregisterEvent(TKey key, Event evt);

        /// <summary>
        /// 구독한 이밴트를 발행
        /// </summary>
        /// <param name="key">발행할 Event의 키</param>
        public void NotifyEvent(TKey key, params object[] value);
    }
}
