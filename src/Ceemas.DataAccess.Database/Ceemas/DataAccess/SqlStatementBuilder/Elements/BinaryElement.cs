using System.Collections.Generic;

namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class BinaryElement : Element
    {
        internal BinaryElement(Element left, BinaryElementType binaryType, Element right)
        {
            Left = left;
            BinaryType = binaryType;
            Right = right;
        }

        public override ElementTypes ElementType => ElementTypes.Binary;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitBinaryElement(this);
        }

        public static Dictionary<BinaryElementType, (string Keyword, int Priority)> BinaryOperatorMap = new Dictionary<BinaryElementType, (string Keyword, int Priority)>()
        {
            { BinaryElementType.And,              ("AND", 9) },
            { BinaryElementType.Or,               ("OR",  10) },
            { BinaryElementType.Equal,            ("=" , 8) },
            { BinaryElementType.NotEqual,         ("<>", 8) },
            { BinaryElementType.GreaterThan,      (">" , 7) },
            { BinaryElementType.GreaterThanEqual, (">=", 7) },
            { BinaryElementType.LessThan,         ("<" , 7) },
            { BinaryElementType.LessThanEqual,    ("<=", 7) },
            { BinaryElementType.In,               ("IN", 8) },
            { BinaryElementType.Is,               ("IS", 8) },
            { BinaryElementType.Add,              ("+" , 5) },
            { BinaryElementType.Subtract,         ("-" , 5) },
            { BinaryElementType.Multiply,         ("*" , 4) },
            { BinaryElementType.Divide,           ("/" , 4) },
            { BinaryElementType.Modulo,           ("%" , 4) }
        };

        public BinaryElementType BinaryType { get; }

        public Element Left { get; internal set; }

        public Element Right { get; internal set; }

        public override string ToString()
        {
            return base.ToString() + " -> {" + BinaryType +"}";
        }
    }
}