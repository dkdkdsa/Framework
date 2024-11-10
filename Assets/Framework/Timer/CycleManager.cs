using System;

namespace Framework.Timer
{
    public class CycleManager : MonoSingleton<CycleManager>
    {

        public event Action UpdateEvent;

        private void Awake()
        {

            DontDestroyOnLoad(this);

        }

        private void Update()
        {

            UpdateEvent?.Invoke();

        }

    }

}