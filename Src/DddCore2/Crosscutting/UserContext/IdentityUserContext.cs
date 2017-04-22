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
                    HttpContextAccessor
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
                if (!IsAuthenticated) throw new ArgumentException("User is not authenticated.");

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

        public IHttpContextAccessor HttpContextAccessor => HttpContextAccessor2;

        public IHttpContextAccessor HttpContextAccessor1 => HttpContextAccessor2;

        public IHttpContextAccessor HttpContextAccessor2 => httpContextAccessor;
    }
}
