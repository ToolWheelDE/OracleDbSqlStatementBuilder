namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class GroupByStatementElement : StatementElement
    {
        internal GroupByStatementElement(GroupByColumnElement[] columnElements)
        {
            ColumnElements = columnElements;
        }

        public GroupByColumnElement[] ColumnElements { get; }
    }
}