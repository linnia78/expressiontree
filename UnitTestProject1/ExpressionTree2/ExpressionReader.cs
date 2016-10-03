﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public interface IExpressionReader
    {
        string ReadExpression(TextReader reader);
    }
    public abstract class ExpressionReader : IExpressionReader
    {
        protected readonly Func<char, bool> _readCondition;
        public ExpressionReader(Func<char, bool> readCondition)
        {
            this._readCondition = readCondition;
        }

        public virtual string ReadExpression(TextReader textReader)
        {
            return this.ReadWhile(textReader, this._readCondition);
        }
        
        protected string ReadWhile(TextReader textReader, Func<char, bool> readCondition)
        {
            var expression = string.Empty;
            int peek;
            while ((peek = textReader.Peek()) > -1)
            {
                var next = (char)peek;
                if (readCondition(next))
                {
                    expression += (char)textReader.Read();
                }
                else
                {
                    break;
                }
            }
            return expression;
        }

        protected void SkipOneChar(TextReader textReader)
        {
            if (textReader.Peek() > -1) { textReader.Read(); }
        }
    }
}
