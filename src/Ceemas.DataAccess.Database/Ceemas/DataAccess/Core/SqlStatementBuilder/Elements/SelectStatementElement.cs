using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class SelectStatementElement : StatementElement
    {
        internal SelectStatementElement(FromStatementElement[] fromStatementElements, JoinStatementElement[] joinStatementElements, WhereStatementElement whereStatementElement, GroupByStatementElement groupByStatementElement, OrderByStatementElement orderStatement)
        {
            FromStatementElements = fromStatementElements;
            JoinStatementElements = joinStatementElements;
            WhereStatementElement = whereStatementElement;
            GroupByStatementElement = groupByStatementElement;
            OrderStatementElement = orderStatement;
        }

        public FromStatementElement[] FromStatementElements { get; }

        public JoinStatementElement[] JoinStatementElements { get; }

        public WhereStatementElement WhereStatementElement { get; }
        public GroupByStatementElement GroupByStatementElement { get; }

        public OrderByStatementElement OrderStatementElement { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> [FROM:{FromStatementElements.Length} WHERE:{WhereStatementElement != null} GROUP:{GroupByStatementElement != null} ORDER:{OrderStatementElement != null}]";
        }
    }
}
