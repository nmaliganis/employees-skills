using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace employees.skills.api.Controllers.API.Base
{
    /// <summary>
    /// BaseController
    /// </summary>
    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// GetEmailFromClaims
        /// </summary>
        /// <returns></returns>
        protected bool IsSuFromClaims()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var isSu = (claimsPrincipal?.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
                .FirstOrDefault(x => x.Value == "SU")
                ?.Value;
            return !String.IsNullOrEmpty(isSu);
        }

        /// <summary>
        /// GetEmailFromClaims
        /// </summary>
        /// <returns></returns>
        protected string GetEmailFromClaims()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                .Value;
            return email;
        }

        /// <summary>
        /// GetMemberFromClaims
        /// </summary>
        /// <returns></returns>
        protected Guid GetMemberFromClaims() {
            var claimsPrincipal = User as ClaimsPrincipal;
            var member = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")
                .Value;
            return member != null ? Guid.Parse(member) : Guid.Empty;
        }

        /// <summary>
        /// GetInstitutionFromClaims
        /// </summary>
        /// <returns></returns>
        protected Guid GetInstitutionFromClaims() {
            var claimsPrincipal = User as ClaimsPrincipal;
            var institution = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn")
                .Value;
            return institution != null ? Guid.Parse(institution) : Guid.Empty;
        }
    }
}