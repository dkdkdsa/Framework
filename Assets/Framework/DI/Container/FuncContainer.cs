using System;

namespace Framework.DI
{
    internal class FuncContainer : IBindContainer
    {

        private Func<object> _bindFunc;

        internal FuncContainer(Func<object> bindFunc)
        {
            _bindFunc = bindFunc;
        }

        object IBindContainer.GetInstance()
        {
            return _bindFunc();
        }
    }
}
