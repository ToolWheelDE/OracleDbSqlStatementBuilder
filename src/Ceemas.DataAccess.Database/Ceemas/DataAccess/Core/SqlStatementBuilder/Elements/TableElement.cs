namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class TableElement : Element
    {
        internal TableElement(string schemaName, string tableName, string tableAliasName)
        {
            SchemaName = schemaName;
            TableName = tableName;
            TableAliasName = tableAliasName;
        }

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitTableElement(this);
        }

        public override ElementTypes ElementType => ElementTypes.From;

        public string SchemaName { get; }

        public string TableName { get; }

        public string TableAliasName { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" -> {SchemaName}.{TableName} {TableAliasName}";
        }
    }
}