using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers
{
    public enum ParserStates
    {
        NotStarted = 0,
        LineStarted,
        LineEnded,
        ColumnStarted,
        ColumnEnded,
    }
}
