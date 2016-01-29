#region credits
// ***********************************************************************
// Assembly	: Ideal
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;
using Ideal.Core.Eventing;
using Ideal.Identity.Settings;
using Ideal.Membership.Services;
using Ideal.Membership.Settings;
using Ideal.Security.Authentication;

namespace Ideal.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IMembershipService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMessageBus _messageBus;
        private readonly IAccountSettings _membershipSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="membershipSettings"></param>
        public AccountController(IMembershipService userService, IAuthenticationService authenticationService, IMessageBus messageBus, IAccountSettings membershipSettings)
        {
            _membershipSettings = membershipSettings;
            _messageBus = messageBus;
            _userService = userService;
            _authenticationService = authenticationService;
        }
    }
}