using DelimitedSeperatedValueTextParsers.Common;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace DelimitedSeperatedValueTextParsers.Tests.Common
{
    public class ParserUtilitiesTests
    {
        [Test]
        public void GivenType_WhenGetPropertyNames_ThenReturnAllNames()
        {
            var parserUtilities = new ParserUtilities();
            var properties = parserUtilities.GetPublicGetSetPropertyNames(typeof(SampleData));
            ArePropertiesEqual(properties[0], "Id", "Given Id", 1);
            ArePropertiesEqual(properties[1], "Name", "Name Property", 2);
            ArePropertiesEqual(properties[2], "Description", "Description Property", 12);
        }

        private bool ArePropertiesEqual(ColumnPropertyInfo columnPropertyInfo, string name, string description, int order)
        {
            return string.Equals(columnPropertyInfo.PropertyName, name, System.StringComparison.OrdinalIgnoreCase) &&
                string.Equals(columnPropertyInfo.DisplayName, description, System.StringComparison.OrdinalIgnoreCase) &&
                columnPropertyInfo.DisplayOrder == order;
        }

        private class SampleData
        {
            [Display(Name = "Given Id", Order = 1)]
            public int Id { get; set; }
            [Display(Name = "Name Property", Order = 2)]
            public string Name { get; set; }
            [Display(Name = "Description Property", Order = 12)]
            public string Description { get; set; }
        }
    }
}
