namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class ColumnElement : Element
    {
        internal ColumnElement(Element columnElement, string aliasName)
        {
            Column = columnElement;
            AliasName = aliasName;
        }

        public override ElementTypes ElementType => ElementTypes.Column;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitColumnElement(this);
        }

        public Element Column { get; }

        public string AliasName { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> {Column} {AliasName}";
        }
    }
}