using Framework.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Framework.DI
{
    [DefaultExecutionOrder(-100)]
    public abstract class MonoInstallerBase : MonoBehaviour
    {

        private DIContainer _container;

        private void Awake()
        {
            _container = new();
            InstallBinding(_container);
            Install();
        }

        protected abstract void InstallBinding(DIContainer container);

        private protected virtual void Install()
        {

            Stack<GameObject> objs = new Stack<GameObject>();
            objs.Push(gameObject);

            while(objs.Count > 0)
            {

                var obj = objs.Pop();

                if (obj != gameObject && obj.TryGetComponent<MonoInstallerBase>(out var _))
                    continue;

                var compos = GetComponents<Component>();

                foreach (var compo in compos)
                {
                    if (!CheckInstalling(compo))
                        continue;

                    SetFields(compo);
                    SetMethods(compo);
                    SetPropertys(compo);
                }

                var childs = obj.GetChilds();
                foreach (var o in childs)
                    objs.Push(o.gameObject);

            }
        }

        private bool CheckInstalling(Component compo)
        {

            var installers = GetComponents<ITargetInstaller>();

            foreach(var installer in installers)
            {
                if(installer.GetGenericType() == compo.GetType())
                {
                    return false;
                }
            }

            return true;

        }

        private protected void SetPropertys(Component compo)
        {

            var type = compo.GetType();

            HashSet<PropertyInfo> check = new();

            while (type != typeof(MonoBehaviour) && type != typeof(Component))
            {

                var propertys = compo.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttribute<Inject>() != null);

                foreach (var property in propertys)
                {
                    if (check.Contains(property))
                        continue;

                    property.SetValue(compo, FindInstnace(property.PropertyType));
                    check.Add(property);
                }

                type = type.BaseType;

            }

        }

        private protected void SetMethods(Component compo)
        {

            var type = compo.GetType();

            HashSet<MethodInfo> check = new();

            while(type != typeof(MonoBehaviour) && type != typeof(Component))
            {
                var methods = compo.GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttribute<Inject>() != null);

                foreach (var method in methods)
                {
                    if (check.Contains(method))
                        continue;

                    var @params = method.GetParameters();
                    object[] objs = new object[@params.Length];

                    for (int i = 0; i < @params.Length; i++)
                        objs[i] = FindInstnace(@params[i].ParameterType);

                    check.Add(method);
                    method.Invoke(compo, objs);
                }
                
                type = type.BaseType;
            }
        }

        private protected void SetFields(Component compo)
        {

            var type = compo.GetType();

            HashSet<FieldInfo> check = new();

            while (type != typeof(Component) && type != typeof(MonoBehaviour))
            {

                var fields = type
                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttribute<Inject>() != null);

                foreach (var field in fields)
                {
                    if (check.Contains(field))
                        continue;

                    field.SetValue(compo, FindInstnace(field.FieldType));
                    check.Add(field);
                }

                type = type.BaseType;

            }
 
        }

        private object FindInstnace(Type t)
        {
            var o = _container.Get(t);
            if(o == null)
                o = GetComponent(t);
            return o;
        }

    }
}