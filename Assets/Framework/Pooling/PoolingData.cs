using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pooling
{
    [CreateAssetMenu(menuName = "SO/Pooling/Data")]
    public class PoolingData : ScriptableObject
    {

        public List<PoolData> poolDatas = new List<PoolData>();


        [System.Serializable]
        public class PoolData
        {
            public string key;
            public int poolCount;
            public GameObject originObject;
        }

    }
}
