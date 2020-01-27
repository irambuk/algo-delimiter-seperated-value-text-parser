using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedSeperatedValueTextParsers.Tests
{
    public class SimpleTextParserTests
    {
        [Test]
        public void GivenText_WhenParse_ReturnsCorrectData()
        {
            var config = new ParserConfigurations {IsFirstLineHeaderLine = false, ColumnDelimiter = ',', LineDelimiter = '\n' };
            var parser = new SimpleTextParser(config);

            parser.Read("a,b,c,d\n1,2,3,4\n");
            parser.Read("p,q,r,s\n");

            var data = parser.GetData();


            Assert.IsNotNull(data);
        }
    }
}
