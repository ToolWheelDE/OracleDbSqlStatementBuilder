namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class TableColumnElement : Element
    {
        public TableColumnElement(string tableAliasName, string columnName)
        {
            TableAliasName = tableAliasName;
            ColumnName = columnName;
        }

        public override ElementTypes ElementType => ElementTypes.TableColumn;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitTableColumnElement(this);
        }

        public string TableAliasName { get; }

        public string ColumnName { get; }

        public override string ToString()
        {
            return $"{base.ToString()}  {TableAliasName}.{ColumnName}";
        }
    }
}