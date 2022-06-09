namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class JoinOnStatementElement : StatementElement
    {
        public JoinOnStatementElement(Element element)
        {
            Expression = element;
        }

        public Element Expression { get; }
    }
}