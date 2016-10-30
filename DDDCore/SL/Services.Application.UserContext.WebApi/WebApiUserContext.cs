using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Contracts.Services.Application.UserContext;

namespace Services.Application.UserContext.WebApi
{
    public class WebApiUserContext : IUserContext
    {
        IIdentity identity;

        IIdentity Identity => identity ?? (identity = HttpContext.Current.User.Identity);

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
