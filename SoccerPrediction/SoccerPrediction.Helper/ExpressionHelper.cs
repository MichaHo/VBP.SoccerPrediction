using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SoccerPrediction.Helper
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// kompiliert einen Ausdruck und ruft den Rückgabewert der Funktionen ab
        /// </ summary>
        /// <typeparam name = "T"> der Typ des Rückgabewerts </ typeparam>
        /// <param name = "lambda"> der zu kompilierende Ausdruck </ param>
        /// <returns> </ returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Setzt den zugrunde liegenden Eigenschaftswert auf den angegebenen Wert
        /// aus einem Ausdruck, der die Eigenschaft enthält
        /// </ summary>
        /// <typeparam name = "T"> festzulegender Werttyp </ typeparam>
        /// <param name = "lambda"> der Ausdruck </ param>
        /// /// <param name = "value"> Der Wert zum Festlegen der Eigenschaft </ param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // konvertiert einen lambda () => some.Property to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            propertyInfo.SetValue(target, value);
        }
    }
}
