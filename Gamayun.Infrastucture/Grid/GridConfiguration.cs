using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Gamayun.Infrastucture.Grid
{
    public class GridConfiguration<TResult> : IGridConfiguration where TResult : IGridResultModel
    {
        public string GridSelector { get; set; }

        public string SelectHref { get; set; }

        public bool Selectable => !string.IsNullOrWhiteSpace(SelectHref);

        public string DataUrl { get; set; }

        public List<GridAction> Actions { get; set; } = new List<GridAction>();

        public IEnumerable<GridProperty> GetGridProperties()
        {
            var gridProps = new List<GridProperty>();
            var props = typeof(TResult).GetProperties();

            foreach (var prop in props)
            {
                if(prop.Name == nameof(IGridResultModel.Id))
                    continue;
                
                var gridProp = new GridProperty();

                var titleAttr = prop.GetCustomAttribute<PropertyTitleAttribute>();
                var dontFilterAttr = prop.GetCustomAttribute<DontFilterAttribute>();

                gridProp.Name = Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1); // to camelcase
                gridProp.Title = (titleAttr != null)
                    ? titleAttr.Title
                    : prop.Name;
                gridProp.Type = prop.PropertyType.GetGridType().GetName();
                gridProp.Filter = (dontFilterAttr == null);
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
            else if (@this.Equals(typeof(bool)))
            {
                return GridPropertyType.Boolean;
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
                case GridPropertyType.Boolean:
                    return nameof(GridPropertyType.Boolean).ToLower();
                default:
                    return default;
            }
        }

    }


}
