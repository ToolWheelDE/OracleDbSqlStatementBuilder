namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public abstract class StatementElement : Element
    {
        public override ElementTypes ElementType => ElementTypes.Statement;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitStatementElement(this);
        }
    }
}