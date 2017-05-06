using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace DddCore.Crosscutting.UserContext
{
    public class IdentityUserContext : IUserContext<Guid>
    {
        #region Private Methods

        readonly IHttpContextAccessor httpContextAccessor;

        ClaimsPrincipal User
        {
            get
            {
                var user =
                    httpContextAccessor
                        .HttpContext
                        .User;

                return user;
            }
        }

        #endregion

        public IdentityUserContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid Id
        {
            get
            {
                if (!IsAuthenticated) throw new ArgumentException("User is not authenticated.");

                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (String.IsNullOrEmpty(userIdString))
                {
                    userIdString = User.FindFirst("sub")?.Value;
                }

                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    userId = Guid.Empty;
                }
                return userId;
            }
        }

        public string UserName => User.Identity.Name;

        public bool IsAuthenticated => User.Identity.IsAuthenticated;
    }
}
