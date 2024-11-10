using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Support
{
    public static class Support
    {

        public static IReadOnlyList<Transform> GetChilds(this GameObject gameObject)
        {
            return GetChilds(gameObject.transform);
        }

        public static IReadOnlyList<Transform> GetChilds(this Transform trm)
        {
            List<Transform> children = new();
            int cnt = trm.childCount;

            for (int i = 0; i < cnt; i++)
            {
                children.Add(trm.GetChild(i));
            }

            return children;
        }

        public static T Clone<T>(this ICloneable origin) where T : ICloneable
        {
            return (T)origin.Clone();
        }

        public static int GetHash(this string value)
        {
            return value.GetHashCode();
        }

        public static T Casting<T>(this object source)
        {
            return (T)source;
        }

        public static int GetGameObjectId(this Component comp)
        {
            return comp.gameObject.GetInstanceID();
        }

        public static int GetGameObjectId(this RaycastHit hit)
        {
            return hit.transform.gameObject.GetInstanceID();
        }

        public static T GetRandom<T>(this List<T> target)
        {
            return target[UnityEngine.Random.Range(0, target.Count)];
        }

    }
}
