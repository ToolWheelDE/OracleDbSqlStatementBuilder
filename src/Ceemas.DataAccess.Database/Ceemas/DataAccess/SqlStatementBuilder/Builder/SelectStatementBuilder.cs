using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.SqlStatementBuilder.Builder
{
    public class SelectStatementBuilder
    {
        public SelectStatementBuilder From(string schemaName, string tableName, string tableAliasName, Action<SelectColumnBuilder> columns)
        {
            var columnBuilder = new SelectColumnBuilder(tableAliasName);
            columns?.Invoke(columnBuilder);

            SelectFromElements.Add(Element.FromStatement(Element.From(schemaName, tableAliasName, tableAliasName), columnBuilder.ColumnElements));

            return this;
        }

        public SelectStatementBuilder Join(string schemaName, string tableName, string tableAliasName, Action<ExpressionStatementBuilder> joinExpression, Action<SelectColumnBuilder> columns)
        {
            var elementList = new List<Element>();
            var whereBuilder = new ExpressionStatementBuilder(elementList);
            joinExpression?.Invoke(whereBuilder);

            var columnBuilder = new SelectColumnBuilder(tableAliasName);
            columns?.Invoke(columnBuilder);

            SelectJoinElements.Add(
                Element.JoinStatement(Element.From(schemaName, tableAliasName, tableAliasName),
                Element.JoinOnStatement(ExpressionElementInfixConverter.ConvertElementList(elementList)),
                columnBuilder.ColumnElements)
                );

            return this;
        }

        public SelectStatementBuilder Where(Action<ExpressionStatementBuilder> where)
        {
            var elementList = new List<Element>();
            var whereBuilder = new ExpressionStatementBuilder(elementList);
            where?.Invoke(whereBuilder);

            WhereExpression = Element.WhereStatement(ExpressionElementInfixConverter.ConvertElementList(elementList));

            return this;
        }

        public SelectStatementBuilder Order(Action<OrderByStatementBuilder> order)
        {
            var elementList = new List<OrderByElement>();
            var orderBuilder = new OrderByStatementBuilder(elementList);
            order?.Invoke(orderBuilder);

            OrderByStatement = Element.OrderByStatement(elementList);

            return this;
        }

        internal List<FromStatementElement> SelectFromElements { get; private set; } = new List<FromStatementElement>();

        internal List<JoinStatementElement> SelectJoinElements { get; private set; } = new List<JoinStatementElement>();

        internal WhereStatementElement WhereExpression { get; private set; }

        internal OrderByStatementElement OrderByStatement { get; private set; }
    }
}
