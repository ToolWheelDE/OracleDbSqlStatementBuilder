namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class AggregateElement : Element
    {
        internal AggregateElement(AggregateType aggregateType, Element expression)
        {
            AggregateType = aggregateType;
            Expression = expression;
        }

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitAggregateElement(this);
        }

        public override ElementTypes ElementType => ElementTypes.AggregateFunction;

        public AggregateType AggregateType { get; }

        public Element Expression { get; }
    }
}