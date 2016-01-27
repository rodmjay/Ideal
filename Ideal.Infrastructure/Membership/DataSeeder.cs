#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Data.Entity.Migrations;
using System.Security.Claims;
using Ideal.Core.Model;
using Ideal.Infrastructure.Data;

namespace Ideal.Infrastructure.Membership
{
    public partial class MembershipDataSeeder
    {
        public void Seed(DataContext context)
        {
            var user = new User()
            {
                Id = 1,
                Email = "webmaster@scaffr.com",
                Username = "admin",
                FirstName = "Rod",
                LastName = "Johnson",
                LastLogin = DateTime.UtcNow,
                Gender = Gender.Male,
                Address = "Admin address",
                PhoneNumber = "555-555-5555",
                IsLoginAllowed = true,
                IsAccountClosed = false,
                IsAccountVerified = true,
                Created = DateTime.UtcNow,
                Tenant = "default",
                // password is "admin"
                HashedPassword = "FA00.ACHEhktjwC+lLMLKq0PZXYsnr9HreWXtgMY55xMDY4ctWYeyzGPxt2vGLEtOEX2SKA==",
                PasswordChanged = DateTime.UtcNow,
                FailedLoginCount = 0,
                Updated = DateTime.UtcNow
            };

            user.Claims.Add(new UserClaim()
            {
                Type = ClaimTypes.Role,
                Value = "Admin"
            });

            user.Claims.Add(new UserClaim()
            {
                Type = ClaimTypes.Role,
                Value = "Super Admin"
            });

            context.Users.AddOrUpdate(user);
            context.SaveChanges();
        }
    }
}