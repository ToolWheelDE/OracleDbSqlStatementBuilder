using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Visitors
{
    public sealed class StatementElementVisitor : ElementVisitor
    {
        private SqlStringBuilder sqlBuilder;
        private ColumnElementVisitor columnElementVisitor;
        private ExpressionElementVisitor expressionVisitor;

        public StatementElementVisitor(SqlStringBuilder sqlBuilder)
        {
            this.sqlBuilder = sqlBuilder;

            columnElementVisitor = new ColumnElementVisitor(sqlBuilder, true);
            expressionVisitor = new ExpressionElementVisitor(sqlBuilder, columnElementVisitor);
        }

        protected internal override void VisitStatementElement(StatementElement statementElement)
        {
            switch (statementElement)
            {
                case SelectStatementElement selectStatementElement:
                    {
                        var visitor = new SeletctStatementElementVisitor(sqlBuilder, columnElementVisitor, expressionVisitor);
                        visitor.Visit(selectStatementElement);
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Das Element {statementElement.GetType().Name} kann in diesem Visitor nicht varbeitet werden.");
            }
        }
    }
}
