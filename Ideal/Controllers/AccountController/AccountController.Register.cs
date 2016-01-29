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

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
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
        /// Registration form.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous, OnlyAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        /// <summary>
        /// Registration form.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost, AllowAnonymous, OnlyAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.CreateAccount(model.Username, model.Password, model.Email, model.FirstName, model.LastName, model.PhoneNumber, model.Address);
                    if (ModelState.Process(user))
                    {
                        //new MembershipEvent(MembershipEventCode.UserCreated, user.Entity).Raise();

                        if (_membershipSettings.RequireAccountVerification)
                        {
                            return View("RegisterSuccess", model);
                        }
                        return View("RegisterConfirm", true);
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Confirms a new registration
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous, OnlyAnonymous]
        public ActionResult Confirm(string id)
        {
            var result = _userService.VerifyAccount(id);
            return View("RegisterConfirm", result.IsValid);
        }

        /// <summary>
        /// Cancels an existing registration
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous, OnlyAnonymous]
        public ActionResult Cancel(string id)
        {
            var result = _userService.CancelNewAccount(id);
            return View("RegisterCancel", result);
        }
    }
}