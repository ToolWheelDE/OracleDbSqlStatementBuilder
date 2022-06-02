namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class FromStatementElement : StatementElement
    {
        public FromStatementElement(TableElement tableElement, ColumnElement[] columnElements)
        {
            FromElement = tableElement;
            ColumnElements = columnElements;
        }

        public TableElement FromElement { get; }

        public ColumnElement[] ColumnElements { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> {FromElement} [Columns:{ColumnElements.Length}]";
        }
    }
}