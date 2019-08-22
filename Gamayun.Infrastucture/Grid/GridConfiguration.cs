using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Gamayun.Infrastucture.Grid
{
    public class GridConfiguration<T> : IGridConfiguration where T : class, new()
    {
        public string GridSelector { get; set; }

        public IEnumerable<GridProperty> GetGridProperties()
        {
            var gridProps = new List<GridProperty>();
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var gridProp = new GridProperty();

                var titleAttr = prop.GetCustomAttribute<PropertyTitleAttribute>();

                gridProp.Name = prop.Name.ToLower();
                gridProp.Title = (titleAttr != null)
                    ? titleAttr.Title
                    : prop.Name;
                gridProp.Type = prop.PropertyType.GetGridType().GetName();
                gridProps.Add(gridProp);
            }
            return gridProps;
        }
    }

    public static class TypeExtension
    {
        public static GridPropertyType GetGridType(this Type @this)
        {
            if (@this.Equals(typeof(int)))
            {
                return GridPropertyType.Number;
            }
            else if (@this.Equals(typeof(double)))
            {
                return GridPropertyType.Number;
            }
            else if (@this.Equals(typeof(string)))
            {
                return GridPropertyType.Text;
            }
                
            return GridPropertyType.Text;
        }
    }

    public static class GridPropertyTypeExtension
    {
        public static string GetName(this GridPropertyType @this)
        {
            switch (@this)
            {
                case GridPropertyType.Text:
                    return nameof(GridPropertyType.Text).ToLower();
                case GridPropertyType.Number:
                    return nameof(GridPropertyType.Number).ToLower();
                default:
                    return default;
            }
        }

    }


}
