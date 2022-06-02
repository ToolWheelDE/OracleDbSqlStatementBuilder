using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    public sealed class ColumnElementVisitor : ElementVisitor
    {
        private readonly SqlStringBuilder sqlBuilder;
        private readonly bool isSelect;

        public ColumnElementVisitor(SqlStringBuilder sqlBuilder, bool isSelect)
        {
            this.sqlBuilder = sqlBuilder;
            this.isSelect = isSelect;
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
            switch (element.AggregateType)
            {
                case AggregateType.Count:
                    {
                        sqlBuilder.Append("COUNT");
                        sqlBuilder.Append("(");
                        if(element.Expression == null)
                        {
                            sqlBuilder.Append("*");
                        }
                        sqlBuilder.Append(")");
                    }
                    break;

                default:
                    throw new NotImplementedException($"Aggregatfunktion {element.AggregateType} nicht implementiert");
            }
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