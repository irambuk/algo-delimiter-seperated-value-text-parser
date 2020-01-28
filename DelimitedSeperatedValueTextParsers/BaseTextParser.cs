using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    public class TextLineParsedEventArgs<T> : EventArgs
        where T : new()
    {
        public T Line { get; set; }
    }

    public abstract class BaseTextParser<T> where T : new()
    {
        public delegate void TextLineParsedEventHandler(object sender, TextLineParsedEventArgs<T> args);
        public event TextLineParsedEventHandler TextLineParsed;

        protected readonly ParserConfigurations _parserConfigurations;
        protected readonly Queue<char> _parserBuffer = new Queue<char>();

        protected ParserStates _parserState = ParserStates.NotStarted;
        protected readonly StringBuilder _currentDataText = new StringBuilder();

        public BaseTextParser(ParserConfigurations parserConfigurations)
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

        protected abstract void OnColumnFound();

        protected abstract void OnLineFound();

        protected void RaiseTextLineParsed(T t)
        {
            TextLineParsed?.Invoke(this, new TextLineParsedEventArgs<T> { Line = t });
        }
    }
}
