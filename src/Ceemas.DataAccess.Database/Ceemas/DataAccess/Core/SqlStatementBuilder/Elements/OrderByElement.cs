using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public class OrderByElement : Element
    {
        public override ElementTypes ElementType =>  ElementTypes.OrderBy;

        protected internal override void Accept(ElementVisitor elementVisitor)
        {
            elementVisitor.VisitOrderByElement(this);
        }
    }
}
