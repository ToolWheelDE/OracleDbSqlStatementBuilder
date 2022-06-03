using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    public sealed class ColumnElementVisitor : ElementVisitor
    {
        private readonly SqlStringBuilder sqlBuilder;
        private readonly bool isSelect;
        private readonly ExpressionElementVisitor expressionElementVisitor;
        private readonly AggregateElementVisitor aggregateElementVisitor;

        public ColumnElementVisitor(SqlStringBuilder sqlBuilder, bool isSelect)
        {
            this.sqlBuilder = sqlBuilder;
            this.isSelect = isSelect;
            this.expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder, this);
            this.aggregateElementVisitor = new AggregateElementVisitor(sqlBuilder, this);
        }

        public ColumnElementVisitor(SqlStringBuilder sqlBuilder, ExpressionElementVisitor expressionElementVisitor, bool isSelect)
        {
            this.sqlBuilder = sqlBuilder;
            this.isSelect = isSelect;
            this.expressionElementVisitor = expressionElementVisitor;
            this.aggregateElementVisitor = new AggregateElementVisitor(sqlBuilder);
        }

        protected internal override void VisitColumnElement(ColumnElement element)
        {
            Visit(element.Column);

            if (!string.IsNullOrEmpty(element.AliasName) && isSelect)
            {
                sqlBuilder.Append(" AS ");
                sqlBuilder.Append(element.AliasName);
            }
        }

        protected internal override void VisitTableColumnElement(TableColumnElement element)
        {
            sqlBuilder.Append(element.TableAliasName);
            sqlBuilder.Append(".");
            sqlBuilder.Append(element.ColumnName);
        }

        protected internal override void VisitAggregateElement(AggregateElement element)
        {
            aggregateElementVisitor.Visit(element);
        }

        //public static void ExpressionTableColumn(SqlStringBuilder sqlBuilder, ColumnElement element)
        //{
        //    switch (element.Column)
        //    {
        //        case TableColumnElement columnElement:
        //            {
        //                sqlBuilder.Append(columnElement.TableAliasName);
        //                sqlBuilder.Append(".");
        //                sqlBuilder.Append(columnElement.ColumnName);
        //            }
        //            break;

        //        case AggregateElement aggregateElement:
        //            {

        //            }
        //            break;

        //        default:
        //            break;
        //    }

        //}
    }
}