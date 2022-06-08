using Ceemas.DataAccess.Core.SqlStatementBuilder.Elements;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ceemas
{
    internal static class ExceptionHelper
    {
        internal static string StatementElementNotSupported(Element element)
        {
            return $"Das Element '{element.GetType().Name}' kann nicht untersucht werden.";
        }

        internal static string NotImplemented(Element element)
        {
            return $"Das Visitor für das Element '{element.GetType().Name}' ist nicht implementiert";
        }

        internal static string InvalidOperation(Element element)
        {
            return $"Das Element '{element.GetType().Name}' wurde nicht erwartet.";
        }
    }
}
