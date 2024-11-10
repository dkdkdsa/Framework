using System;
using System.Collections.Generic;

namespace Framework.DI
{
    public sealed class DIContainer
    {

        private readonly Dictionary<Type, IBindContainer> _bindContainer = new();

        public void Bind<T>(T o)
        {
            _bindContainer.Add(typeof(T), new InstanceContainer(o));
        }

        public void Bind(Type t, object o)
        {
            _bindContainer.Add(t, new InstanceContainer(o));
        }

        public void Bind<T>(Func<object> o)
        {
            _bindContainer.Add(typeof(T), new FuncContainer(o));
        }

        public void Bint(Type t, Func<object> o) 
        {
            _bindContainer.Add(t, new FuncContainer(o));
        }

        internal object Get(Type type)
        {
            return _bindContainer.TryGetValue(type, out var o) ? o.GetInstance() : default;
        }
    }
}
