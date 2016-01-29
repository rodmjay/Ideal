#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;

namespace Ideal.Core.Interfaces.Settings
{
    #region

    

    #endregion

    public interface IMembershipSettings
    {
        bool MultiTenant { get; set; }

        string DefaultTenant { get; set; }

        bool EmailIsUsername { get; set; }

        bool UsernamesUniqueAcrossTenants { get; set; }

        bool RequireAccountVerification { get; set; }

        bool AllowLoginAfterAccountCreation { get; set; }

        int AccountLockoutFailedLoginAttempts { get; set; }

        TimeSpan AccountLockoutDuration { get; set; }

        int AccountLockoutMinutes { get; set; }

        bool AllowAccountDeletion { get; set; }

        int MinimumPasswordLength { get; set; }

        int PasswordResetFrequency { get; set; }

        int PasswordHashingIterationCount { get; set; }

        bool AllowEmailChangeWhenEmailIsUsername { get; set; }
    }
}