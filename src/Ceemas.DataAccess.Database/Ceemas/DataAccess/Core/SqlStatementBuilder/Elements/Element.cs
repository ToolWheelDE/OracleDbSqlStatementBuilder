using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class Element
    {
        public abstract ElementTypes ElementType { get; }

        protected internal abstract void Accept(ElementVisitor elementVisitor);

        #region Factory-Methodes

        #region Statements

        public static SelectStatementElement SelectStatement(IEnumerable<FromStatementElement> fromStatementElement, IEnumerable<JoinStatementElement> joinStatementElements, WhereStatementElement whereExpression, GroupByStatementElement groupByStatementElement, OrderByStatementElement orderByStatement)
        {
            if (fromStatementElement == null)
            {
                fromStatementElement = new FromStatementElement[] { };
            }

            if (joinStatementElements == null)
            {
                joinStatementElements = new JoinStatementElement[] { };
            }

            return new SelectStatementElement(fromStatementElement.ToArray(), joinStatementElements.ToArray(), whereExpression, groupByStatementElement, orderByStatement);
        }

        public static ColumnOrderByElement ColumnOrderBy(ColumnElement columnElement, OrderByMode mode)
        {
            return new ColumnOrderByElement(columnElement, mode);
        }

        public static JoinStatementElement JoinStatement(TableElement tableLement, JoinOnStatementElement joinExpression, IEnumerable<ColumnElement> tableColumns)
        {
            if (tableColumns == null)
            {
                tableColumns = new ColumnElement[] { };
            }

            return new JoinStatementElement(tableLement, joinExpression, tableColumns.ToArray());
        }

        public static JoinOnStatementElement JoinOnStatement(Element element)
        {
            return new JoinOnStatementElement(element);
        }

        public static WhereStatementElement WhereStatement(Element element)
        {
            return new WhereStatementElement(element);
        }

        public static FromStatementElement FromStatement(TableElement tableElement, IEnumerable<ColumnElement> tableColumns)
        {
            if (tableColumns == null)
            {
                tableColumns = new ColumnElement[] { };
            }

            return new FromStatementElement(tableElement, tableColumns.ToArray());
        }

        public static GroupByStatementElement GroupByStatement(IEnumerable<GroupByColumnElement> columnElements)
        {
            return new GroupByStatementElement(columnElements.ToArray());
        }

        public static GroupByColumnElement GroupColumn(ColumnElement columnElement)
        {
            return new GroupByColumnElement(columnElement);
        }

        public static OrderByStatementElement OrderByStatement(IEnumerable<OrderByElement> orderByElements)
        {
            return new OrderByStatementElement(orderByElements.ToArray());
        }

        public static TableElement From(string schemaName, string tableName, string aliasName)
        {
            return new TableElement(schemaName, tableName, aliasName);
        }

        #endregion

        #region Binary

        public static BinaryElement Equal(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.Equal, right);
        }

        public static BinaryElement NotEqual(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.NotEqual, right);
        }

        public static BinaryElement GreaterThan(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.GreaterThan, right);
        }

        public static BinaryElement GreaterThanEqual(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.GreaterThanEqual, right);
        }

        public static BinaryElement LessThan(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.LessThan, right);
        }

        public static BinaryElement LessThanEqual(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.LessThanEqual, right);
        }

        public static BinaryElement Is(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.Is, right);
        }

        public static BinaryElement And(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.And, right);
        }

        public static BinaryElement Or(Element left, Element right)
        {
            return new BinaryElement(left, BinaryElementType.Or, right);
        }

        public static BinaryElement Add(ColumnElement left, ColumnElement right)
        {
            return new BinaryElement(left, BinaryElementType.Add, right);
        }

        public static BinaryElement Subtract(ColumnElement left, ColumnElement right)
        {
            return new BinaryElement(left, BinaryElementType.Subtract, right);
        }

        public static BinaryElement Multiply(ColumnElement left, ColumnElement right)
        {
            return new BinaryElement(left, BinaryElementType.Multiply, right);
        }

        public static BinaryElement Divide(ColumnElement left, ColumnElement right)
        {
            return new BinaryElement(left, BinaryElementType.Divide, right);
        }

        public static BinaryElement Modulo(ColumnElement left, ColumnElement right)
        {
            return new BinaryElement(left, BinaryElementType.Modulo, right);
        }

        public static BinaryElement In(Element left, IEnumerable<Element> right)
        {
            var container = new ContainerElement(right.ToArray());

            return new BinaryElement(left, BinaryElementType.In, container);
        }

        #endregion

        #region Unary

        public static UnaryElement Block(Element element)
        {
            return new UnaryElement(UnaryElementType.Block, element);
        }

        public static UnaryElement Not(Element element)
        {
            return new UnaryElement(UnaryElementType.Not, element);
        }

        #endregion

        #region Values

        public static ValueElement Value(object value)
        {
            return new ValueElement(value);
        }

        #endregion

        #region Column

        public static ColumnElement Column(string columnAliasName)
        {
            return Column(null, columnAliasName);
        }

        public static ColumnElement Column(Element element)
        {
            //TODO: Check auf nicht gültige Elemente
            return Column(element, null);
        }

        public static ColumnElement Column(Element element, string columnAlias)
        {
            //TODO: Check auf nicht gültige Elemente
            return new ColumnElement(element, columnAlias);
        }

        public static ColumnElement TableColumn(string tableAliasname, string columnName, string columnAliasName)
        {
            return new ColumnElement(new TableColumnElement(tableAliasname, columnName), columnAliasName);
        }

        #endregion

        #region Aggregate-Functions

        public static AggregateElement Count(Element element)
        {
            return new AggregateElement(AggregateType.Count, element);
        }

        #endregion

        #endregion

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public override string ToString()
        {
            return GetType().Name + " - " + ElementType;
        }

    }
}
