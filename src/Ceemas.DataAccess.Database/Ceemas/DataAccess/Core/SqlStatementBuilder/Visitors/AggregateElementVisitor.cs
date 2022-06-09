using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Visitors
{
    internal class AggregateElementVisitor : ElementVisitor
    {
        private SqlStringBuilder sqlBuilder;
        private readonly ExpressionElementVisitor expressionElementVisitor;

        public AggregateElementVisitor(SqlStringBuilder sqlBuilder)
        {
            this.sqlBuilder = sqlBuilder;
            expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);
        }

        public AggregateElementVisitor(SqlStringBuilder sqlBuilder, ColumnElementVisitor columnElementVisitor)
        {
            this.sqlBuilder = sqlBuilder;
            expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder,columnElementVisitor);
        }

        public AggregateElementVisitor(SqlStringBuilder sqlBuilder, ExpressionElementVisitor expressionElementVisitor)
        {
            this.sqlBuilder = sqlBuilder;
            this.expressionElementVisitor = expressionElementVisitor;
        }
        
        protected internal override void VisitAggregateElement(AggregateElement element)
        {
            switch (element.AggregateType)
            {
                case AggregateType.Count:
                    {
                        sqlBuilder.Append("COUNT");
                        sqlBuilder.Append("(");
                        if (element.Expression == null)
                        {
                            sqlBuilder.Append("*");
                        }
                        else
                        {

                        }
                        sqlBuilder.Append(")");
                    }
                    break;

                default:
                    throw new NotImplementedException($"Aggregatfunktion {element.AggregateType} nicht implementiert");
            }
        }
    }
}