using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Elements
{
    public abstract class ElementVisitor
    {
        public virtual void Visit(Element element)
        {
            element.Accept(this);
        }

        public virtual void Visit(IEnumerable<Element> elements)
        {
            foreach (var element in elements)
            {
                element.Accept(this);
            }
        }

        protected internal virtual void VisitStatementElement(StatementElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitTableElement(TableElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitColumnElement(ColumnElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitTableColumnElement(TableColumnElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitGroupColumnElement(GroupByColumnElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitAggregateElement(AggregateElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitBinaryElement(BinaryElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitUnaryElement(UnaryElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitValueElement(ValueElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected internal virtual void VisitOrderByElement(OrderByElement element)
        {
            throw new NotImplementedException($"Die Elementgruppe '{element.GetType().Name}' kann in diesem Visitor nicht verarbeitet werden.");
        }

        protected void ArrayJoinEnumerator<T>(T[] source, Action<T> seperator, Action<T> element)
        {
            var arrayLength = source.Length;

            for (int index = 0; index < arrayLength; index++)
            {
                element?.Invoke(source[index]);

                if (index < arrayLength - 1)
                {
                    seperator?.Invoke(source[index]);
                }
            }
        }
    }
}
