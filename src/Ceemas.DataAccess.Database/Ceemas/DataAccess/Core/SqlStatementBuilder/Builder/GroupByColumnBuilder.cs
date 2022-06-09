using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System.Collections.Generic;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Builder
{
    public class GroupByColumnBuilder
    {
        private HashSet<GroupByColumnElement> columns = new HashSet<GroupByColumnElement>();
        private string tableAliasName;

        public GroupByColumnBuilder()
        { }

        internal IEnumerable<GroupByColumnElement> ColumnElements { get => columns; }

        public GroupByColumnBuilder TableColumn(string tableAliasName, string columnName)
        {
            columns.Add(Element.GroupColumn(Element.TableColumn(tableAliasName, columnName, null)));
            return this;
        }
    }
}