#region credits
// ***********************************************************************
// Assembly	: Ideal
// Author	: Rod Johnson
// Created	: 03-15-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Threading.Tasks;
using System.Web.Mvc;
using Ideal.Core.Common.Membership;
using Ideal.Extensions;
using Ideal.HttpClients;
using Ideal.Models;
using Ideal.Security.Authorization;

namespace Ideal.Controllers
{
    /// <summary>
    /// Class AccountController
    /// </summary>
    public partial class AccountController
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous, OnlyAnonymous]
        public async Task<ActionResult> Identity()
        {
	        var client = IdealHttpClient.GetClient();

	        var identity = await client.GetAsync("identity").ConfigureAwait(false);

	        if (identity.IsSuccessStatusCode)
	        {
		        var identityString = await identity.Content.ReadAsStringAsync().ConfigureAwait(false);
		        return View(identityString);
	        }
	        else return View("Error");

        }

    }
}