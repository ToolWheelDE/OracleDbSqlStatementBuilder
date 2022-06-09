using System;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class HavingStatementElement : StatementElement
    {
        internal HavingStatementElement(Element element)
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