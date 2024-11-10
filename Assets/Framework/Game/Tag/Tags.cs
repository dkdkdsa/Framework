using System;

namespace Framework
{
    [Flags]
    public enum Tags
    {

        Hit = 1 << 0,
        Damageable = 1 << 1,
        Ground = 1 << 2,
        Enemy = 1 << 3,
        Player = 1 << 4

    }

}