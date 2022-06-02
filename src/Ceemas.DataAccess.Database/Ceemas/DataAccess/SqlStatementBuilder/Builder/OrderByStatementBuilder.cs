using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;

namespace Ceemas.DataAccess.SqlStatementBuilder.Builder
{
    public class OrderByStatementBuilder
    {
        private List<OrderByElement> elementList;

        public OrderByStatementBuilder(List<OrderByElement> elementList)
        {
            this.elementList = elementList;
        }

        public OrderByStatementBuilder Column(string columnAliasName, OrderByMode mode)
        {
            elementList.Add(Element.ColumnOrderBy(Element.Column(columnAliasName), mode));

            return this;
        }
    }
}