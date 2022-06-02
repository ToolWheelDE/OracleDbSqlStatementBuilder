using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceemas.DataAccess.SqlStatementBuilder.Elements
{
    public class ContainerElement : Element
    {
        internal ContainerElement(Element[] elements)
        {
            Elements = elements;
        }

        public override ElementTypes ElementType => ElementTypes.Container;

        public Element[] Elements { get; }

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            throw new InvalidOperationException("Das Element 'ContainerElement' kann nicht aufgelöst werden");
        }
    }
}
