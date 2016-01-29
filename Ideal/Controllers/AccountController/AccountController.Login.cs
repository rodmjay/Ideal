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

using System.Web.Mvc;
using Ideal.Identity.Model;
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
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous, OnlyAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = this._userService.Authenticate(model.UserName, model.Password);
                if (ModelState.Process(result))
                {
                    var user = result.Entity;

                    _authenticationService.SignIn(user, model.RememberMe);

                    if (_userService.IsPasswordExpired(model.UserName))
                    {
                        return RedirectToAction("ChangePassword", "Account");
                    }

                    //new MembershipEvent(MembershipEventCode.UserLogin, user).Raise();

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Username or Password");
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            throw new System.NotImplementedException();
        }
    }
}