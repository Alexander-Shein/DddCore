using DddCore.Contracts.Crosscutting.UserContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace DddCore.Crosscutting.UserContext
{
    public class IdentityUserContext : IUserContext<Guid>
    {
        #region Private Methods

        readonly IHttpContextAccessor httpContextAccessor;

        IIdentity Identity
        {
            get
            {
                var identity =
                    httpContextAccessor
                        .HttpContext
                        .User
                        .Identity;

                return identity;
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
                var userIdString = ((ClaimsIdentity)Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                Guid userId;

                if (!Guid.TryParse(userIdString, out userId))
                {
                    userId = Guid.Empty;
                }
                return userId;
            }
        }

        public string UserName => Identity.Name;

        public bool IsAuthenticated => Identity.IsAuthenticated;
    }
}
