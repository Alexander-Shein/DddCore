using DddCore.Contracts.BLL.Domain.BusinessRules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddCore.Contracts.BLL.Domain.Services
{
    public static class AsyncResultExtensionsLeftOperand
    {
        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<T, Result<K>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result> resultTask, Func<Result<T>> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> resultTask, Func<Result<K>> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result> Then<T>(this Task<Result<T>> resultTask, Func<T, Result> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result> Then(this Task<Result> resultTask, Func<Result> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }

        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, int errorCode, string errorMessage, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorCode, errorMessage);
        }

        public static async Task<Result> Ensure(this Task<Result> resultTask, Func<bool> predicate, int errorCode, string errorMessage, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorCode, errorMessage);
        }

        public static async Task<Result<K>> Map<T, K>(this Task<Result<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<Result<T>> Map<T>(this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<Result<T>> Then<T>(this Task<Result<T>> resultTask, Action<T> action, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(action);
        }

        public static async Task<Result> Then(this Task<Result> resultTask, Action action, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(action);
        }

        public static async Task<T> Always<T>(this Task<Result> resultTask, Func<Result, T> func, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Always(func);
        }

        public static async Task<K> Always<T, K>(this Task<Result<T>> resultTask, Func<Result<T>, K> func, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Always(func);
        }

        public static async Task<Result<T>> Fail<T>(this Task<Result<T>> resultTask, Action action, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Fail(action);
        }

        public static async Task<Result> Fail(this Task<Result> resultTask, Action action, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Fail(action);
        }

        public static async Task<Result<T>> Fail<T>(this Task<Result<T>> resultTask, Action<IEnumerable<BusinessError>> action, bool continueOnCapturedContext = false)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Fail(action);
        }

        public static async Task<Result> Fail(this Task<Result> resultTask, Action<IEnumerable<BusinessError>> action, bool continueOnCapturedContext = false)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Fail(action);
        }
    }
}