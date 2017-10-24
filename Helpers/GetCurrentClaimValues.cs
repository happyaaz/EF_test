using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace LostAndFound.Helpers
{
    public static class GetCurrentClaimValues
    {
        public static string GetCurrentUserRole ()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userRole = identity.FindFirstValue(ClaimTypes.Role);
            return userRole;
        }


        public static int GetCurrentUserId()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userId = identity.FindFirstValue("userId");
            return Convert.ToInt32 (userId);
        }

        public static string GetCurrentUserClient()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string client = identity.FindFirst("clientName").Value;
            return client;
        }
    }
}