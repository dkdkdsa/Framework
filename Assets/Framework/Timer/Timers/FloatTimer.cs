using System;
using UnityEngine;

namespace Framework.Timer
{
    public class FloatTimer : ITimer<float>
    {

        private float _origin;
        private float _value;

        public float Value => _value;

        public bool IsStarted { get; private set; }

        public event Action<float> OnTickEvent;
        public event Action<float> OnEndEvent;

        public object Clone()
        {

            return new FloatTimer();

        }

        public void StartTimer()
        {

            CycleManager.Instance.UpdateEvent += HandleUpdate;
            IsStarted = true;

        }

        private void HandleUpdate()
        {

            _value -= Time.deltaTime;
            OnTickEvent?.Invoke(_value);

            if (_value < 0)
            {

                OnEndEvent?.Invoke(_value);
                CycleManager.Instance.UpdateEvent -= HandleUpdate;
                IsStarted = false;

            }

        }

        public void Dispose()
        {

            OnTickEvent = null;
            OnEndEvent = null;

        }

        public void SetTime(float value)
        {
            _origin = _value = value;
        }

        public void ResetTimer()
        {

            _value = _origin;
            StartTimer();

        }

        public void StopTimer()
        {

            CycleManager.Instance.UpdateEvent -= HandleUpdate;
            IsStarted = false;

        }
    }
}