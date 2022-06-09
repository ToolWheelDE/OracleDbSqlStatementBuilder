using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class ValueElement : Element
    {
        internal ValueElement(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public override ElementTypes ElementType => ElementTypes.Value;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitValueElement(this);
        }

        public override string ToString()
        {
            return base.ToString() + " -> " + Value.ToString();
        }
    }
}
