using DelimitedSeperatedValueTextParsers.Common;
using System.Collections.Generic;

namespace DelimitedSeperatedValueTextParsers
{
    public class StrongTypedTextParser<T> : BaseTextParser<T>
        where T : new()
    {
        //private readonly T _currentDataItem = new T();
        protected readonly List<string> _currentDataItem = new List<string>();
        private readonly ColumnPropertyInfo[] _properties;

        public StrongTypedTextParser(ParserConfigurations parserConfigurations) : base(parserConfigurations)
        {
            _properties = new ParserUtilities().GetPublicGetSetPropertyNames(typeof(T));
        }

        protected override void OnColumnFound()
        {
            _currentDataItem.Add(_currentDataText.ToString());
            _currentDataText.Clear();
        }

        protected override void OnLineFound()
        {
            var data = new T();
            //TODO: fill the required data
            RaiseTextLineParsed(data);
        }

        private void SetPropertyValue(object obj, string propertyName, string value)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            propertyInfo.SetValue(obj, value, null);
        }
    }
}
