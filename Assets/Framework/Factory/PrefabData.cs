using System;

namespace Framework.Factory
{
    [Serializable]
    public struct PrefabData : IEquatable<PrefabData>
    {

        public string prefabKey;

        public bool Equals(PrefabData other)
        {

            return prefabKey.Equals(other.prefabKey);

        }

    }
}