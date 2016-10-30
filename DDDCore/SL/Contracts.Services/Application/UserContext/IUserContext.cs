using System;

namespace Contracts.Services.Application.UserContext
{
    public interface IUserContext
    {
        Guid Id { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}
