namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class GroupByColumnElement : Element
    {
        public GroupByColumnElement(ColumnElement columnElement)
        {
            ColumnElement = columnElement;
        }

        public override ElementTypes ElementType => ElementTypes.GroupByColumn;

        public ColumnElement ColumnElement { get; }

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitGroupColumnElement(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}  {ColumnElement}";
        }
    }
}