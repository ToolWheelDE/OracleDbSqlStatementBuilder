namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class AggregateElement : Element
    {
        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitAggregateElement(elementVisitor);
        }

        public override ElementTypes ElementType => ElementTypes.AggregateFunction;
    }
}