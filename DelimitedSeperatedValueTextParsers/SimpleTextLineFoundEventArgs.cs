using System;
using System.Collections.Generic;

namespace DelimitedSeperatedValueTextParsers
{
    public class SimpleTextLineFoundEventArgs : EventArgs
    {
        public List<string> TextFields { get; set; }
    }
}
