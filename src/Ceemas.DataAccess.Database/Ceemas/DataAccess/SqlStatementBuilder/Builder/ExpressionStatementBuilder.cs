using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;

namespace Ceemas.DataAccess.SqlStatementBuilder.Builder
{
    public class ExpressionStatementBuilder
    {
        private List<Element> elementList;
        private ExpressionCombinder expressionCombinder;

        public class ExpressionCombinder
        {
            private List<Element> elementList;
            private ExpressionStatementBuilder expressionStatementBuilder;

            internal ExpressionCombinder(List<Element> elementList, ExpressionStatementBuilder expressionStatementBuilder)
            {
                this.elementList = elementList;
                this.expressionStatementBuilder = expressionStatementBuilder;
            }

            public ExpressionStatementBuilder And()
            {
                elementList.Add(Element.And(null, null));
                return expressionStatementBuilder;
            }

            public ExpressionStatementBuilder Or()
            {
                elementList.Add(Element.Or(null, null));
                return expressionStatementBuilder;
            }

            public ExpressionStatementBuilder Block(Action<ExpressionStatementBuilder> block)
            {
                var blockElementList = new List<Element>();
                var whereBuilder = new ExpressionStatementBuilder(blockElementList);
                block?.Invoke(whereBuilder);

                elementList.Add(Element.Block(ExpressionElementInfixConverter.ConvertElementList(blockElementList)));

                return expressionStatementBuilder;
            }

            public ExpressionStatementBuilder Not(Action<ExpressionStatementBuilder> block)
            {
                var blockElementList = new List<Element>();
                var whereBuilder = new ExpressionStatementBuilder(blockElementList);
                block?.Invoke(whereBuilder);

                elementList.Add(Element.Not(ExpressionElementInfixConverter.ConvertElementList(blockElementList)));

                return expressionStatementBuilder;
            }
        }

        internal ExpressionStatementBuilder(List<Element> elementList)
        {
            this.elementList = elementList;

            expressionCombinder = new ExpressionCombinder(elementList, this);
        }

        public ExpressionStatementBuilder Block(Action<ExpressionStatementBuilder> block)
        {
            var blockElementList = new List<Element>();
            var whereBuilder = new ExpressionStatementBuilder(blockElementList);
            block?.Invoke(whereBuilder);

            elementList.Add(Element.Block(ExpressionElementInfixConverter.ConvertElementList(blockElementList)));

            return this;
        }

        public ExpressionStatementBuilder Not(Action<ExpressionStatementBuilder> block)
        {
            var blockElementList = new List<Element>();
            var whereBuilder = new ExpressionStatementBuilder(blockElementList);
            block?.Invoke(whereBuilder);

            elementList.Add(Element.Not(ExpressionElementInfixConverter.ConvertElementList(blockElementList)));

            return this;
        }

        public ExpressionCombinder Equal(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.Equal(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }

        public ExpressionCombinder Equal(string leftTableAlias, string leftColumnName, string rightTableAlias, string rightColumnName)
        {
            elementList.Add(Element.Equal(Element.TableColumn(leftTableAlias, leftColumnName, ""), Element.TableColumn(rightTableAlias, rightColumnName, "")));

            return expressionCombinder;
        }

        public ExpressionCombinder NotEqual(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.NotEqual(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }

        public ExpressionCombinder LessThan(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.LessThan(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }

        public ExpressionCombinder LessThanEqual(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.LessThanEqual(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }

        public ExpressionCombinder GreaterThan(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.GreaterThan(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }

        public ExpressionCombinder GreaterThanEqual(string tableAlias, string columnName, object value)
        {
            elementList.Add(Element.GreaterThanEqual(Element.TableColumn(tableAlias, columnName, ""), Element.Value(value)));

            return expressionCombinder;
        }
    }
}