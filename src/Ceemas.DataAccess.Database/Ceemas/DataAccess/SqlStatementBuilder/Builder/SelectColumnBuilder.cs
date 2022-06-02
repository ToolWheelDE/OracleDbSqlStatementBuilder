using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;

namespace Ceemas.DataAccess.SqlStatementBuilder.Builder
{
    public class SelectColumnBuilder
    {
        private HashSet<ColumnElement> columns = new HashSet<ColumnElement>();
        private string tableAliasName;

        public SelectColumnBuilder(string tableAliasName)
        {
            this.tableAliasName = tableAliasName;
        }

        internal IEnumerable<ColumnElement> ColumnElements { get => columns; }

        public SelectColumnBuilder TableColumn(string columnName, string aliasName)
        {
            columns.Add(Element.TableColumn(tableAliasName, columnName, aliasName));
            return this;
        }

        public void Count(string columnAliasName)
        {
            columns.Add(Element.Column(Element.Count(null), columnAliasName));
        }
    }
}