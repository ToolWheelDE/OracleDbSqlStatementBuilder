namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class JoinStatementElement : StatementElement
    {
        public JoinStatementElement(TableElement fromElement, JoinOnStatementElement joinExpression, ColumnElement[] columnElements)
        {
            TableElement = fromElement;
            JoinExpression = joinExpression;
            ColumnElements = columnElements;
        }

        public TableElement TableElement { get; }

        public JoinOnStatementElement JoinExpression { get; }

        public ColumnElement[] ColumnElements { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> ({TableElement}) [Columns:{ColumnElements.Length}]";
        }
    }
}