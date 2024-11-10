using System;
using System.Collections.Generic;

namespace Framework.Factory
{
    public static class Factorys
    {

        private static Dictionary<Type, IFactoryable> _factoryBindContainer = new Dictionary<Type, IFactoryable>();

        public static void AddFactory(Type type, IFactoryable factory)
        {
            _factoryBindContainer.Add(type, factory);
        }

        public static void RemoveFactory(Type type)
        {
            _factoryBindContainer.Remove(type);
        }

        public static T GetFactory<T>() where T : class, IFactoryable
        {

            if (_factoryBindContainer.TryGetValue(typeof(T), out var factory))
            {
                return factory as T;
            }

            return null;

        }
    }
}