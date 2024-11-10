using UnityEngine;

namespace Framework.Pooling
{
    public interface IPoolManager
    {
        public GameObject TakeObject(string key, 
            Vector3 positon = default,
            Quaternion rotation = default,
            Transform parent = null);
        public T TakeObject<T>(string key, 
            Vector3 positon = default,
            Quaternion rotation = default,
            Transform parent = null) where T : Component;
        public T TakePoolable<T>(string key, 
            Vector3 positon = default,
            Quaternion rotation = default, 
            Transform parent = null) where T : IPoolable;

        public void PutObject(GameObject obj);
        public void PutObject<T>(T obj) where T : Component;
        public void PutPoolable(IPoolable obj);
    }
}
