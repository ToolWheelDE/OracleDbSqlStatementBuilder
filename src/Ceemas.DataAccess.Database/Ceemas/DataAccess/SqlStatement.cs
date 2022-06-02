using Ceemas.DataAccess.SqlStatementBuilder;
using Ceemas.DataAccess.SqlStatementBuilder.Builder;
using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using Ceemas.DataAccess.SqlStatementBuilder.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess
{
    public sealed class SqlStatement
    {
        private SqlStringBuilder sqlStringBuilder;

        public SqlStatement(SqlStringBuilder sqlStringBuilder)
        {
            this.sqlStringBuilder = sqlStringBuilder;
        }

        public override string ToString()
        {
            return sqlStringBuilder.ToString();
        }

        #region Factory Methods
        public static SqlStatement Select(Action<SelectStatementBuilder> selectBuilder)
        {
            var builder = new SelectStatementBuilder();
            selectBuilder?.Invoke(builder);

            var selectElement = Element.SelectStatement(builder.SelectFromElements, builder.SelectJoinElements, builder.WhereExpression, builder.OrderByStatement);

            return new SqlStatement(VisitElements(selectElement));
        }

        private static SqlStringBuilder VisitElements(SelectStatementElement selectElement)
        {
            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SqlStatementBuilder.Visitors.StatementElementVisitor(sqlBuilder);
            visitor.Visit(selectElement);

            return sqlBuilder;
        }
        #endregion
    }

}
