using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder
{
    public class SqlStringBuilder
    {
        private StringBuilder sqlBuilder;

        public SqlStringBuilder()
        {
            sqlBuilder = new StringBuilder();
        }

        public void Append(string value)
        {
            sqlBuilder.Append(value);
        }

        public override string ToString()
        {
            return sqlBuilder.ToString();
        }
    }
}
