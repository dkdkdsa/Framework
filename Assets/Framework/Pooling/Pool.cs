using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Pooling
{
    internal class Pool
    {

        private class PoolObject
        {

            private readonly Dictionary<Type, IPoolable> _poolableContaier;
            public GameObject gameObject;

            public PoolObject(GameObject obj)
            {
                gameObject = obj;
                _poolableContaier = new Dictionary<Type, IPoolable>();

                foreach (var compo in obj.GetComponents<IPoolable>())
                {
                    _poolableContaier.Add(compo.GetType(), compo);
                }
            }

            public T GetPoolable<T>() where T : IPoolable
            {
                return (T)_poolableContaier[typeof(T)];
            }

        }

        public Pool(Transform root, GameObject obj, int count)
        {
            _origin = obj;
            _pool = new();
            _root = root;
            _usePoolObject = new();

            for (int i = 0; i < count; i++)
            {
                var o = Object.Instantiate(obj);
                _pool.Push(new PoolObject(o));
                o.SetActive(false);
                o.transform.SetParent(root);
            }
        }

        private readonly Stack<PoolObject> _pool;
        private readonly Dictionary<int, PoolObject> _usePoolObject;
        private readonly GameObject _origin;
        private readonly Transform _root;

        public GameObject TakePool()
        {
            PoolObject obj = _pool.Count > 0 ? _pool.Pop() : new PoolObject(Object.Instantiate(_origin));
            _usePoolObject.Add(obj.gameObject.GetInstanceID(), obj);
            obj.gameObject.SetActive(true);
            return obj.gameObject;
        }

        public T TakePool<T>() where T : IPoolable
        {
            PoolObject obj = _pool.Count > 0 ? _pool.Pop() : new PoolObject(Object.Instantiate(_origin));
            _usePoolObject.Add(obj.gameObject.GetInstanceID(), obj);
            obj.gameObject.SetActive(true);
            return obj.GetPoolable<T>();
        }

        public void PutPool(GameObject obj)
        {
            var poolObj = _usePoolObject[obj.GetInstanceID()];
            _usePoolObject.Remove(obj.GetInstanceID());
            obj.transform.SetParent(_root);
            obj.gameObject.SetActive(false);
            _pool.Push(poolObj);
        }

        public void PutPool(IPoolable obj)
        {
            PutPool(obj.Get());
        }

    }
}