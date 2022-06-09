using System.Collections.Generic;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class UnaryElement : Element
    {
        public UnaryElement(UnaryElementType unaryType, Element expression)
        {
            UnaryType = unaryType;
            Expression = expression;
        }

        public override ElementTypes ElementType => ElementTypes.Unary;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitUnaryElement(this);
        }

        public static Dictionary<UnaryElementType, (string Keyword, int Priority)> UnaryOperatiorMap = new Dictionary<UnaryElementType, (string Keyword, int Priority)>()
        {
            { UnaryElementType.Not, ("NOT",1 )},
            { UnaryElementType.Block,( "", 0) } // Das ist kein Operator sondern eine Lösung um Expression in Klammern zu setzen.
        };

        public UnaryElementType UnaryType { get; }


        public Element Expression { get; }

        public override string ToString()
        {
            return base.ToString() + " -> {" + UnaryType + "}";
        }
    }
}