using System.Collections.Generic;

namespace DelimitedSeperatedValueTextParsers
{
    public class SimpleTextParser : BaseTextParser<List<string>>
    {        
        protected readonly List<string> _currentDataItem = new List<string>();

        public SimpleTextParser(ParserConfigurations parserConfigurations) : base(parserConfigurations)
        {
        }

        protected override void OnColumnFound()
        {
            _currentDataItem.Add(_currentDataText.ToString());
            _currentDataText.Clear();
        }

        protected override void OnLineFound()
        {
            var data = new List<string>(_currentDataItem);
            _currentDataItem.Clear();
            RaiseTextLineParsed(data);
        }
    }
}