using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    public class SimpleTextParser
    {
        private readonly ParserConfigurations _parserConfigurations;
        //private readonly ColumnPropertyInfo[] _properties;
        private readonly Queue<char> _parserBuffer = new Queue<char>();

        private ParserStates _parserState = ParserStates.NotStarted;

        private readonly List<List<string>> _currentDataList = new List<List<string>>();
        private readonly List<string> _currentDataItem = new List<string>();
        private readonly StringBuilder _currentDataText = new StringBuilder();
        private int _currentColumnNumber;

        public SimpleTextParser(ParserConfigurations parserConfigurations)
        {
            _parserConfigurations = parserConfigurations;
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

        public string[][] GetData()
        {
            var data = _currentDataList.Select(s => s.ToArray()).ToArray();
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
                    case ParserStates.LineEnded:
                        if (isColumnDelimiter)
                        {
                            OnColumnFound();
                        }
                        else
                        {
                            _currentDataText.Append(c);
                        }
                        _parserState = ParserStates.ColumnEnded;
                        break;
                    case ParserStates.LineStarted:
                    case ParserStates.ColumnStarted:
                    case ParserStates.ColumnEnded:
                        if (isLineDelimiter)
                        {
                            OnColumnFound();
                            OnLineFound();
                            _parserState = ParserStates.LineEnded;
                        }
                        else if (isColumnDelimiter)
                        {
                            OnColumnFound();
                            _parserState = ParserStates.ColumnEnded;
                        }
                        else
                        {
                            _currentDataText.Append(c);
                        }
                        break;
                    default:
                        _currentDataText.Append(c);
                        break;
                }
            }
        }

        private void OnColumnFound()
        {
            _currentDataItem.Add(_currentDataText.ToString());
            _currentDataText.Clear();
            _currentColumnNumber++;
        }

        private void OnLineFound()
        {
            _currentDataList.Add(new List<string>(_currentDataItem));
            _currentDataItem.Clear();
            _currentColumnNumber = 0;
        }
    }
}