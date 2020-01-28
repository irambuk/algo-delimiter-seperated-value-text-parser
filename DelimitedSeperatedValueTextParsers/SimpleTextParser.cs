using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    public class SimpleTextParser
    {
        public delegate void SimpleTextLineFoundEventHandler(object sender, SimpleTextLineFoundEventArgs args);
        public event SimpleTextLineFoundEventHandler SimpleTextLineFound;

        private readonly ParserConfigurations _parserConfigurations;
        private readonly Queue<char> _parserBuffer = new Queue<char>();

        private ParserStates _parserState = ParserStates.NotStarted;
        private readonly List<string> _currentDataItem = new List<string>();
        private readonly StringBuilder _currentDataText = new StringBuilder();

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
                            _parserState = ParserStates.ColumnEnded;
                        }
                        else
                        {
                            _currentDataText.Append(c);
                            _parserState = ParserStates.ColumnStarted;
                        }
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
        }

        private void OnLineFound()
        {
            var data = new List<string>(_currentDataItem);
            _currentDataItem.Clear();

            SimpleTextLineFound?.Invoke(this, new SimpleTextLineFoundEventArgs { TextFields = data });
        }
    }
}