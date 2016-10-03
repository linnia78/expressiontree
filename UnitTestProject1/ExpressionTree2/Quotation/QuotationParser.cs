﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class QuotationParser : ExpressionParser
    {
        public QuotationParser(IExpressionReader expressionReader) : base(expressionReader, (@char) => @char == '"') { }

        public override Expression ParseExpression(TextReader textReader)
        {
            string expression = base._expressionReader.ReadExpression(textReader);
            return Expression.Constant(expression);
        }
    }
}