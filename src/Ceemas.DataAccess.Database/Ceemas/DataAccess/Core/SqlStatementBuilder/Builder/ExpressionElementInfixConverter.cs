using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ceemas.DataAccess.Core.SqlStatementBuilder.Builder
{
    internal class ExpressionElementInfixConverter
    {
        public static Element ConvertElementList(List<Element> elements)
        {
            var stack = new Stack<Element>(ResolveInfixNotation(elements));

            while (stack.Count > 1)
            {
                var element = stack.Pop();

                if (element is BinaryElement binaryElement && (binaryElement.BinaryType == BinaryElementType.And || binaryElement.BinaryType == BinaryElementType.Or))
                {
                    binaryElement.Left = stack.Pop();
                    binaryElement.Right = stack.Pop();
                }

                stack.Push(element);
            }

            return stack.Pop();
        }

        public static List<Element> ResolveInfixNotation(List<Element> elements)
        {
            var operatorStack = new Stack<Element>();
            var output = new List<Element>();

            foreach (var element in elements)
            {
                switch (element)
                {
                    case UnaryElement unaryElement:
                        break;

                    case BinaryElement binaryElement:
                        {
                            var currentPriority = GetElementPriority(binaryElement);

                            while (operatorStack.Count > 0 && GetElementPriority(operatorStack.Peek()) <= currentPriority)
                            {
                                output.Add(operatorStack.Pop());
                            }
                        }
                        break;

                    default:
                        throw new NotSupportedException($"Das Element '{element}' konnte nicht als Expression element aufgelöst werden. Es sind nur BinaryElement oder UnaryElement erlaubt.");
                }

                operatorStack.Push(element);
            }

            output.AddRange(operatorStack);

            return output;
        }

        public static int GetElementPriority(Element element)
        {
            switch (element)
            {
                case UnaryElement unaryElement:
                    return UnaryElement.UnaryOperatiorMap[unaryElement.UnaryType].Priority;

                case BinaryElement binaryElement:
                    return BinaryElement.BinaryOperatorMap[binaryElement.BinaryType].Priority;

                default:
                    return 99;
            }
        }
    }
}
