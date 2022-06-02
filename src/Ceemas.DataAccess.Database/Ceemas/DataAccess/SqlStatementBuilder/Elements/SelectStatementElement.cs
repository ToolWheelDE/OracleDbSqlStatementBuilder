using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class SelectStatementElement : StatementElement
    {
        internal SelectStatementElement(FromStatementElement[] fromStatementElements, JoinStatementElement[] joinStatementElements, WhereStatementElement whereStatementElement, OrderByStatementElement orderStatement)
        {
            FromStatementElements = fromStatementElements;
            JoinStatementElements = joinStatementElements;
            WhereStatementElement = whereStatementElement;
            OrderStatementElement = orderStatement;
        }

        public FromStatementElement[] FromStatementElements { get; }

        public JoinStatementElement[] JoinStatementElements { get; }

        public WhereStatementElement WhereStatementElement { get; }

        public OrderByStatementElement OrderStatementElement { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> [FROM:{FromStatementElements.Length} WHERE:{WhereStatementElement != null} ORDER:{OrderStatementElement != null}]";
        }
    }
}
