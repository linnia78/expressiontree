using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ReservedWordParser : IExpressionParser
    {
        //public static bool IsReservedWord(string word)
        //{
        //    return ReservedWord.Words.ContainsKey(word);
        //}

        //public static Expression GetExpression(string word)
        //{
        //    return ((ReservedWord)word).Expression;
        //}
        private readonly ExpressionReader _reader;
        private readonly Func<char, bool> _condition;
        public ReservedWordParser(ExpressionReader reader)
        {
            this._reader = reader;
            this._condition = char.IsLetter;
        }
        public bool CanParse(char @char)
        {
            return this._condition(@char);
        }

        public Expression ReadExpression(TextReader reader)
        {
            string expression = this._reader.ReadExpression(reader);
            return ((ReservedWord)expression).Expression;
        }
    }
}
