using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class WhereStatementElement : StatementElement
    {
        internal WhereStatementElement(Element element)
        {
            if (!(element is BinaryElement) && !(element is UnaryElement))
            {
                throw new ArgumentException("Das Argument muss vom Typ BinaryElement oder UnaryElement sein", nameof(element));
            }

            Expression = element;
        }

        public Element Expression { get; }
    }
}
