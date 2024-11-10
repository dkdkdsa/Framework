using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public static class Singletons
    {

        private readonly static Dictionary<Type, object> _singletons = new();

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            UnityEditor.EditorApplication.quitting += Dispose;
        }

        private static void Dispose()
        {
            UnityEditor.EditorApplication.quitting -= Dispose;
            _singletons.Clear();
        }
#endif
        public static void RegisterSingleton<T>(T instance) where T : class
        {
            if (!_singletons.ContainsKey(typeof(T)))
            {
                _singletons.Add(typeof(T), instance);
            }
            else
            {
                Debug.LogWarning($"싱글톤 인스턴스가 이미 존재합니다 타입: {typeof(T).Name}");
            }
        }

        public static void UnRegisterSingleton<T>() where T : class
        {
            if (_singletons.ContainsKey(typeof(T)))
            {
                _singletons.Remove(typeof(T));
            }
            else
            {
                Debug.LogWarning($"싱글톤 인스턴스가 존재하지 않습니다 타입: {typeof(T).Name}");
            }
        }

        public static T GetSingleton<T>() where T : class
        {
            if (_singletons.ContainsKey(typeof(T)))
            {
                return _singletons[typeof(T)] as T;
            }

            Debug.LogWarning($"싱글톤 인스턴스가 존재하지 않습니다 타입: {typeof(T).Name}");

            return default;
        }

        public static IReadOnlyDictionary<Type, object> GetSingletons() => _singletons;

    }
}
