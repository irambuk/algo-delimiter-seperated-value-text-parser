using NUnit.Framework;
using System.Collections.Generic;

namespace DelimitedSeperatedValueTextParsers.Tests
{
    public class SimpleTextParserTests
    {
        [Test]
        public void GivenText_WhenParse_ReturnsCorrectData()
        {
            var config = new ParserConfigurations { IsFirstLineHeaderLine = false, ColumnDelimiter = ',', LineDelimiter = '\n' };
            var parser = new SimpleTextParser(config);

            var linesFound = new List<List<string>>();

            parser.TextLineParsed += (sender, eventArgs) => { linesFound.Add(eventArgs.Line); };

            parser.Read("a,b,c,d\n1,2,3,4\n");
            parser.Read("p,q,r,s\n");

            Assert.IsNotEmpty(linesFound);
        }
    }
}
