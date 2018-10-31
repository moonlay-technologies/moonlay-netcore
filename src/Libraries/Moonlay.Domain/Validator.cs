using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Moonlay
{
    public static class Validator
    {
        public static void ThrowIfNull(Expression<Func<object>> expression)
        {
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException(
                  "expected property or field expression.");
            }
            var compiled = expression.Compile();
            var value = compiled();
            if (value == null)
            {
                throw new ArgumentNullException(body.Member.Name);
            }
        }

        public static void ThrowWhenTrue(Expression<Func<bool>> expression, string message = "invalid")
        {
            //if (!(expression.Body is MemberExpression body))
            //{
            //    throw new ArgumentException("expected property or field expression.");
            //}

            var compiled = expression.Compile();
            var value = compiled();
            if (value)
                throw new ArgumentException(message);
        }

        public static void ThrowIfNullOrEmpty<T>(Expression<Func<IEnumerable<T>>> expression)
        {
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException("expected property or field expression.");
            }

            var compiled = expression.Compile();
            var value = compiled();
            if (value == null || !value.Any())
            {
                throw new ArgumentNullException(body.Member.Name);
            }
        }

        public static void ThrowIfNullOrEmpty(Expression<Func<string>> expression)
        {
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException("expected property or field expression.");
            }

            var compiled = expression.Compile();
            var value = compiled();

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("String is null or empty", body.Member.Name);
            }
        }

        public static ValidationException ErrorValidation(params (string propName, string message)[] errors)
        {
            IEnumerable<FluentValidation.Results.ValidationFailure> failures = errors.Select(o => new FluentValidation.Results.ValidationFailure(o.propName, o.message));

            return new ValidationException(failures);
        }
    }
}
