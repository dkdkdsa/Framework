using Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Framework.Pooling
{
    internal class PoolManager : IPoolManager
    {

        private readonly Dictionary<string, Pool> _poolContainer = new Dictionary<string, Pool>();
        private readonly Dictionary<int, string> _usedObjectKey = new Dictionary<int, string>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            var ins = new PoolManager();
            Singletons.RegisterSingleton<IPoolManager>(ins);
        }

        private PoolManager() 
        {

            var rootObj = new GameObject("@#$%PoolRoot%$#@");
            var data = Resources.Load<PoolingData>("PoolingData");
            Object.DontDestroyOnLoad(rootObj);

            foreach(var item in data.poolDatas)
            {
                _poolContainer.Add(item.key, 
                    new Pool(rootObj.transform, item.originObject, item.poolCount));
            }

        }

        public void PutObject(GameObject obj)
        {
            var key = _usedObjectKey[obj.GetInstanceID()];
            _usedObjectKey.Remove(obj.GetInstanceID());
            _poolContainer[key].PutPool(obj);
        }

        public void PutObject<T>(T obj) where T : Component
        {
            PutObject(obj.gameObject);
        }

        public void PutPoolable(IPoolable obj)
        {
            PutObject(obj.Get());
        }

        public GameObject TakeObject(string key, 
            Vector3 positon = default, 
            Quaternion rotation = default, 
            Transform parent = null)
        {
            var obj = _poolContainer[key].TakePool();
            _usedObjectKey.Add(obj.GetInstanceID(), key);
            InitObj(obj, positon, rotation, parent);
            return obj;
        }

        public T TakeObject<T>(string key, 
            Vector3 positon = default,
            Quaternion rotation = default,
            Transform parent = null) where T : Component
        {
            return TakeObject(key, positon, rotation, parent).GetComponent<T>();
        }

        public T TakePoolable<T>(string key, 
            Vector3 positon = default,
            Quaternion rotation = default,
            Transform parent = null) where T : IPoolable
        {
            var obj = _poolContainer[key].TakePool<T>();
            var go = obj.Get();
            InitObj(go, positon, rotation, parent);
            _usedObjectKey.Add(go.GetInstanceID(), key);
            return obj;
        }

        private void InitObj(GameObject obj, 
            Vector3 positon,
            Quaternion rotation,
            Transform parent)
        {
            obj.SetActive(true);
            obj.transform.SetParent(parent);
            obj.transform.position = positon;
            obj.transform.rotation = rotation;
        }

    }
}
