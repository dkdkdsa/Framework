using UnityEngine;

namespace Framework
{
    public interface IMoveable
    {
        public void Move(in Vector3 vec, in float speed);
    }
}