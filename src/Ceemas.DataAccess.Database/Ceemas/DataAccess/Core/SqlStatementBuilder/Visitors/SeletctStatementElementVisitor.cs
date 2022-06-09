using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Visitors
{
    public class SeletctStatementElementVisitor : ElementVisitor
    {
        private SqlStringBuilder sqlBuilder;
        private ExpressionElementVisitor expressionVisitor;
        private ColumnElementVisitor columnElementVisitor;

        public SeletctStatementElementVisitor(SqlStringBuilder sqlBuilder, ColumnElementVisitor columnElementVisitor, ExpressionElementVisitor expressionVisitor)
        {
            this.sqlBuilder = sqlBuilder;
            this.columnElementVisitor = columnElementVisitor;
            this.expressionVisitor = expressionVisitor;
        }

        protected internal override void VisitStatementElement(StatementElement statementElement)
        {
            switch (statementElement)
            {
                case SelectStatementElement selectStatementElement:
                    {
                        sqlBuilder.Append("SELECT ");

                        var fromColumns = from fromStatement in selectStatementElement.FromStatementElements
                                          from fromColumn in fromStatement.ColumnElements
                                          select fromColumn;

                        var joinColumns = from joinStatement in selectStatementElement.JoinStatementElements
                                          from joinColumn in joinStatement.ColumnElements
                                          select joinColumn;

                        var columns = fromColumns.Union(joinColumns).ToArray();

                        ArrayJoinEnumerator(columns, seperator => sqlBuilder.Append(", "), columnElement => columnElementVisitor.Visit(columnElement));

                        sqlBuilder.Append(" FROM ");
                        ArrayJoinEnumerator(selectStatementElement.FromStatementElements, seperator => sqlBuilder.Append(", "), element => Visit(element));

                        if (selectStatementElement.JoinStatementElements != null && selectStatementElement.JoinStatementElements.Length > 0)
                        {
                            ArrayJoinEnumerator(selectStatementElement.JoinStatementElements, seperator => { }, element => Visit(element));
                        }

                        if (selectStatementElement.WhereStatementElement != null)
                        {
                            Visit(selectStatementElement.WhereStatementElement);
                        }

                        if (selectStatementElement.GroupByStatementElement != null)
                        {
                            Visit(selectStatementElement.GroupByStatementElement);
                        }

                        if (selectStatementElement.OrderStatementElement != null)
                        {
                            Visit(selectStatementElement.OrderStatementElement);
                        }
                    }
                    break;

                case FromStatementElement fromStatementElement:
                    {
                        fromStatementElement.FromElement.Accept(this);
                    }
                    break;

                case JoinStatementElement joinStatementElement:
                    {
                        sqlBuilder.Append(" JOIN ");
                        joinStatementElement.TableElement.Accept(this);
                        expressionVisitor.Visit(joinStatementElement.JoinExpression);
                    }
                    break;

                case WhereStatementElement whereStatementElement:
                    {
                        sqlBuilder.Append(" WHERE ");
                        expressionVisitor.Visit(whereStatementElement.Expression);
                    }
                    break;

                case GroupByStatementElement groupByStatementElement:
                    {
                        sqlBuilder.Append(" GROUP BY ");
                        Visit(groupByStatementElement.ColumnElements);
                    }
                    break;

                case OrderByStatementElement orderByStatementElement:
                    {
                        sqlBuilder.Append(" ORDER BY ");

                        ArrayJoinEnumerator(orderByStatementElement.OrderByElements, seperator => sqlBuilder.Append(", "), element =>
                        {
                            element.Accept(this);
                        });
                    }
                    break;

                default:
                    var type = statementElement.GetType();
                    break;
            }
        }

        protected internal override void VisitTableElement(TableElement fromElement)
        {
            sqlBuilder.Append(fromElement.SchemaName);
            sqlBuilder.Append(".");
            sqlBuilder.Append(fromElement.TableName);

            if (!string.IsNullOrEmpty(fromElement.TableAliasName))
            {
                sqlBuilder.Append(" ");
                sqlBuilder.Append(fromElement.TableAliasName);
            }
        }

        protected internal override void VisitOrderByElement(OrderByElement orderByElement)
        {
            switch (orderByElement)
            {
                case ColumnOrderByElement columnOrderByElement:
                    {
                        sqlBuilder.Append(columnOrderByElement.ColumnElement.AliasName);
                        if (columnOrderByElement.Mode == OrderByMode.Descending)
                        {
                            sqlBuilder.Append(" DESC");
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        protected internal override void VisitGroupColumnElement(GroupByColumnElement element)
        {
            switch (element.ColumnElement.Column)
            {
                case TableColumnElement tableColumnElement:
                    columnElementVisitor.Visit(tableColumnElement);
                    break;

                default:
                    break;
            }
        }
    }
}
