namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class ColumnOrderByElement : OrderByElement
    {
        internal ColumnOrderByElement(ColumnElement columnElement, OrderByMode mode)
        {
            ColumnElement = columnElement;
            Mode = mode;
        }

        public ColumnElement ColumnElement { get; }

        public OrderByMode Mode { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> [{ColumnElement.AliasName}, {Mode}]";
        }
    }
}