using DddCore.Contracts.BLL.Domain.BusinessRules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddCore.Contracts.BLL.Domain.Services
{
    public static class AsyncResultExtensionsBothOperands
    {
        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            K value = await func(result.Data);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<T, Task<Result<K>>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return await func(result.Data);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result> resultTask, Func<Task<Result<T>>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<Task<Result<K>>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> Then<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail(result.Errors);

            return await func(result.Data).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> Then(this Task<Result> resultTask, Func<Task<Result>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, string errorCode, string errorMessage, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            if (!await predicate(result.Data).ConfigureAwait(continueOnCapturedContext))
                return Result.Fail<T>(errorCode, errorMessage);

            return Result.Ok(result.Data);
        }

        public static async Task<Result> Ensure(this Task<Result> resultTask, Func<Task<bool>> predicate, string errorCode, string errorMessage, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail(result.Errors);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return Result.Fail(errorCode, errorMessage);

            return Result.Success;
        }

        public static async Task<Result<K>> Map<T, K>(this Task<Result<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            K value = await func(result.Data).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> Map<T>(this Task<Result> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return Result.Fail<T>(result.Errors);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result<T>> resultTask, Func<T, Task> action, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action(result.Data).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> Then(this Task<Result> resultTask, Func<Task> action, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> Always<T>(this Task<Result> resultTask, Func<Result, Task<T>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> Always<T, K>(this Task<Result<T>> resultTask, Func<Result<T>, Task<K>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> Fail<T>(this Task<Result<T>> resultTask, Func<Task> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> Fail(this Task<Result> resultTask, Func<Task> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<T>> Fail<T>(this Task<Result<T>> resultTask, Func<IEnumerable<BusinessError>, Task> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func(result.Errors).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}