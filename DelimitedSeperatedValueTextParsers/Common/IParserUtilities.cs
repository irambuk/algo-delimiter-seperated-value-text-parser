using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers.Common
{
    public interface IParserUtilities
    {
        ColumnPropertyInfo[] GetPublicGetSetPropertyNames(Type type);
    }
}
