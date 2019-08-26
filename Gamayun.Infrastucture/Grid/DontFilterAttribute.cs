using System;

namespace Gamayun.Infrastucture.Grid
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    sealed class DontFilterAttribute : Attribute
    {
        public DontFilterAttribute()
        {
        }

    }
}
