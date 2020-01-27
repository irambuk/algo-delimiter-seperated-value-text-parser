using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DelimitedSeperatedValueTextParsers.Common
{
    public class ParserUtilities : IParserUtilities
    {
        public ColumnPropertyInfo[] GetPublicGetSetPropertyNames(Type type)
        {
            var properties = type.GetProperties()
                .ToList()
                .Select(p => 
                new ColumnPropertyInfo { 
                    DisplayName = p.Name, 
                    PropertyName = p.GetCustomAttribute<DisplayAttribute>().Description, 
                    DisplayOrder = p.GetCustomAttribute<DisplayAttribute>().Order 
                });
            return properties.ToArray();
        }
    }
}
