using System;

namespace Framework.Timer
{
    public interface ITimer<T> : ICloneable, IDisposable
    {

        public event Action<T> OnTickEvent;
        public event Action<T> OnEndEvent;
        public bool IsStarted { get; }
        public T Value { get; }

        public void SetTime(T value);
        public void StartTimer();
        public void ResetTimer();
        public void StopTimer();

    }
}