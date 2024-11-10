using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Framework.Core;

namespace Framework
{
    public class TagManager : MonoSingleton<TagManager>
    {

        private Dictionary<int, GameTag> _tagContainer = new();

        public void AddGameTag(int hash, GameTag tag)
        {
            _tagContainer.Add(hash, tag);
        }

        public void RemoveGameTag(int hash)
        {
            _tagContainer.Remove(hash);
        }

        public GameTag FindGameTag(int hash)
        {
            _tagContainer.TryGetValue(hash, out GameTag tag);

            return tag;
        }

        public GameObject GetObject(Tags tag)
        {
            var obj = _tagContainer.Values.FirstOrDefault(x => x.HasTag(tag));
            return obj.gameObject;
        }
    }
}