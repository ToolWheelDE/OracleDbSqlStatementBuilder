using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    public sealed class StatementElementVisitor :ElementVisitor
    {
        private SqlStringBuilder sqlBuilder;

        public StatementElementVisitor(SqlStringBuilder sqlBuilder)
        {
            this.sqlBuilder = sqlBuilder;
        }

        protected internal override void VisitStatementElement(StatementElement statementElement)
        {
            switch (statementElement)
            {
                case SelectStatementElement selectStatementElement:
                    {
                        var visitor = new SeletctStatementElementVisitor(sqlBuilder);
                        visitor.Visit(selectStatementElement);
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Das Element {statementElement.GetType().Name} kann in diesem Visitor nicht varbeitet werden.");
            }
        }
    }
}
