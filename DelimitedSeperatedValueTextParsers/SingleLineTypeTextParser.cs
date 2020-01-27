using DelimitedSeperatedValueTextParsers.Common;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    public class SingleLineTypeTextParser<T> where T : new()
    {
        private readonly ParserConfigurations _parserConfigurations;
        private readonly ColumnPropertyInfo[] _properties;
        private readonly Queue<char> _parserBuffer = new Queue<char>();

        private ParserStates _parserState = ParserStates.NotStarted;

        private readonly List<T> _currentDataList = new List<T>();
        private T _currentDataItem;
        private readonly StringBuilder _currentDataText = new StringBuilder();
        private int _currentColumnNumber;
        
        public SingleLineTypeTextParser(ParserConfigurations parserConfigurations)
        {
            _parserConfigurations = parserConfigurations;
            _properties = new ParserUtilities().GetPublicGetSetPropertyNames(typeof(T));
        }

        public void Read(char c)
        {
            _parserBuffer.Enqueue(c);
            ParseBuffer();
        }

        public void Read(string text)
        {
            foreach (var c in text.ToCharArray())
            {
                _parserBuffer.Enqueue(c);
            }
            ParseBuffer();
        }

        public List<T> GetData()
        {
            var data = new List<T>();
            data.AddRange(_currentDataList);
            _currentDataList.Clear();
            return data;
        }

        private void ParseBuffer()
        {

            while (_parserBuffer.Count > 0)
            {
                var c = _parserBuffer.Dequeue();

                var isLineDelimiter = (c == _parserConfigurations.LineDelimiter);
                var isColumnDelimiter = (c == _parserConfigurations.ColumnDelimiter);

                switch (_parserState)
                {
                    case ParserStates.NotStarted:
                        if (isColumnDelimiter)
                        {
                            //no text found for the first column

                        }
                        else
                        {
                            _currentDataText.Append(c);
                        }
                        break;
                    case ParserStates.LineStarted:
                        break;
                    case ParserStates.LineEnded:
                        break;
                    case ParserStates.ColumnStarted:
                        break;
                    case ParserStates.ColumnEnded:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}