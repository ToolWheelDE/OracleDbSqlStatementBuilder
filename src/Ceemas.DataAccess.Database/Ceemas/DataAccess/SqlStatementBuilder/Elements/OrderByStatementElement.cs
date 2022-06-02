namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class OrderByStatementElement : StatementElement
    {
        internal OrderByStatementElement(OrderByElement[] orderByElements)
        {
            OrderByElements = orderByElements;
        }

        public OrderByElement[] OrderByElements { get; }

        public override string ToString()
        {
            return base.ToString() + $" -> [Orders:{OrderByElements.Length}]";
        }
    }
}