using System;
using UnityEngine;

namespace Framework.DI
{
    internal interface ITargetInstaller
    {

        internal Type GetGenericType();

    }

    public abstract class TargetInstaller<T> : MonoInstallerBase, ITargetInstaller where T : Component
    {

        private protected override void Install()
        {
            var compo = GetComponent<T>();

            SetFields(compo);
            SetMethods(compo);
            SetPropertys(compo);
        }

        Type ITargetInstaller.GetGenericType()
        {
            return typeof(T);
        }
    }
}
