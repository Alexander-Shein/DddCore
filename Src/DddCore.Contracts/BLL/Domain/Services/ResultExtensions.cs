using DddCore.Contracts.BLL.Domain.BusinessRules;
using System;
using System.Collections.Generic;

namespace DddCore.Contracts.BLL.Domain.Services
{
    public static class ResultExtensions
    {
        public static Result<K> Then<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return Result.Ok(func(result.Data));
        }

        public static Result<T> Then<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            return Result.Ok(func());
        }

        public static Result<K> Then<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return func(result.Data);
        }

        public static Result<T> Then<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            return func();
        }

        public static Result<K> Then<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return func();
        }

        public static Result Then<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Errors);

            return func(result.Data);
        }

        public static Result Then(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorCode, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            if (!predicate(result.Data))
                return Result.Fail<T>(errorCode, errorMessage);

            return Result.Ok(result.Data);
        }

        public static Result Ensure(this Result result, Func<bool> predicate, string errorCode, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail(result.Errors);

            if (!predicate())
                return Result.Fail(errorCode, errorMessage);

            return Result.Success;
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return Result.Ok(func(result.Data));
        }

        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            return Result.Ok(func());
        }

        public static Result<T> Then<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Data);
            }

            return result;
        }

        public static Result Then(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T Always<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static K Always<T, K>(this Result<T> result, Func<Result<T>, K> func)
        {
            return func(result);
        }

        public static Result<T> Fail<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result Fail(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result<T> Fail<T>(this Result<T> result, Action<IEnumerable<BusinessError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Errors);
            }

            return result;
        }

        public static Result Fail(this Result result, Action<IEnumerable<BusinessError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Errors);
            }

            return result;
        }
    }
}