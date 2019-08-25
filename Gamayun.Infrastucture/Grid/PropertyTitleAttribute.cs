using System;

namespace Gamayun.Infrastucture.Grid
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    sealed class PropertyTitleAttribute : Attribute
    {
        public PropertyTitleAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; private set; }
        
    }
}
