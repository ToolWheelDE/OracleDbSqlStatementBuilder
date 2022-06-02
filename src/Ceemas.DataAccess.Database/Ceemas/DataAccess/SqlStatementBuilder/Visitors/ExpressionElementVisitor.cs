using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    public class ExpressionElementVisitor : ElementVisitor
    {
        private SqlStringBuilder sqlBuilder;
        private ColumnElementVisitor columnElementVisitor;
        private bool notInFlag;

        public ExpressionElementVisitor(SqlStringBuilder sqlBuilder)
        {
            this.sqlBuilder = sqlBuilder;

            columnElementVisitor = new ColumnElementVisitor(sqlBuilder, false);
        }

        public override void Visit(Element element)
        {
            if (element is BinaryElement || element is UnaryElement)
            {
                base.Visit(element);
            }
            else if (element is WhereStatementElement whereStatementElement)
            {
                sqlBuilder.Append(" WHERE ");
                Visit(whereStatementElement.Expression);
            }
            else if (element is JoinOnStatementElement joinOnStatementElement)
            {
                sqlBuilder.Append(" ON ");
                Visit(joinOnStatementElement.Expression);
            }
            else
            {
                throw new InvalidOperationException("Der ExpressionElementVisitor erlaubt nur Elemente vom Type 'BinaryElement' oder 'UnaryElement'");
            }
        }

        protected internal override void VisitBinaryElement(BinaryElement element)
        {
            element.Left.Accept(this);

            if (notInFlag)
            {
                var notText = UnaryElement.UnaryOperatiorMap[UnaryElementType.Not];
                sqlBuilder.Append(" ");
                sqlBuilder.Append(notText.Keyword);
            }

            sqlBuilder.Append(" " + BinaryElement.BinaryOperatorMap[element.BinaryType].Keyword + " ");

            if (element.Right is ContainerElement container)
            {
                sqlBuilder.Append("(");
                ArrayJoinEnumerator(container.Elements, seperatorAction => sqlBuilder.Append(", "), itemAction => itemAction.Accept(this));
                sqlBuilder.Append(")");
            }
            else
            {
                element.Right.Accept(this);
            }
        }

        protected internal override void VisitUnaryElement(UnaryElement element)
        {
            switch (element.UnaryType)
            {
                case UnaryElementType.Not:
                    if (element.Expression is BinaryElement binaryElement && binaryElement.BinaryType == BinaryElementType.In)
                    {
                        notInFlag = true;
                    }
                    else
                    {
                        notInFlag = false;

                        var notText = UnaryElement.UnaryOperatiorMap[UnaryElementType.Not];
                        sqlBuilder.Append(" ");
                        sqlBuilder.Append(notText.Keyword);
                        sqlBuilder.Append(" ");
                    }

                    Visit(element.Expression);
                    break;

                case UnaryElementType.Block:
                    {
                        sqlBuilder.Append("(");
                        element.Expression.Accept(this);
                        sqlBuilder.Append(")");
                    }
                    break;
                default:
                    break;
            }
        }

        protected internal override void VisitValueElement(ValueElement element)
        {
            //TODO: Hier ein Value-Parameter einfügen
            sqlBuilder.Append(element.Value.ToString());
        }

        protected internal override void VisitColumnElement(ColumnElement columnElement)
        {
            columnElementVisitor.Visit(columnElement);
        }
    }
}
