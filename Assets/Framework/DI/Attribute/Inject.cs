using System;

namespace Framework.DI
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public class Inject : Attribute
    {
    }
}
