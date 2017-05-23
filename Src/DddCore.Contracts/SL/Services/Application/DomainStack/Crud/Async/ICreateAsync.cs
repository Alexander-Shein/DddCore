﻿using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateAsync<TViewModel, in TInputModel>
    {
        /// <summary>
        /// Example: POST /cars/ HTTP/1.1.
        /// Creates a new entity by InputModel.
        /// </summary>
        /// <param name="im">InputModel has no Id property because when we send request to create new object we don't know id.</param>
        /// <returns>ViewModel contains generated Id property.</returns>
        Task<(TViewModel Vm, OperationResult OperationResult)> CreateAsync(TInputModel im);
    }
}
