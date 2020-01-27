using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    /// <summary>
    /// Defines the nature of the delimitor-separated-value text
    /// </summary>
    public class ParserConfigurations
    {
        public bool IsFirstLineHeaderLine { get; set; }
        public char LineDelimiter { get; set; }
        public char ColumnDelimiter { get; set; }
    }
}
