using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    public sealed class ColumnElementGenerator
    {
        public static void SelectTableColumn(SqlStringBuilder sqlBuilder, ColumnElement element)
        {
            if (element.Column is TableColumnElement columnElement)
            {
                sqlBuilder.Append(columnElement.TableAliasName);
                sqlBuilder.Append(".");
                sqlBuilder.Append(columnElement.ColumnName);

                if (!string.IsNullOrEmpty(element.AliasName))
                {
                    sqlBuilder.Append(" AS ");
                    sqlBuilder.Append(element.AliasName);
                }
            }
        }

        public static void ExpressionTableColumn(SqlStringBuilder sqlBuilder, ColumnElement element)
        {
            if (element.Column is TableColumnElement columnElement)
            {
                sqlBuilder.Append(columnElement.TableAliasName);
                sqlBuilder.Append(".");
                sqlBuilder.Append(columnElement.ColumnName);                                
            }
        }
    }
}