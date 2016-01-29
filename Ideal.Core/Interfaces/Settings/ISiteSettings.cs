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
    public interface ISiteSettings
    {
        Guid InstanceId { get; set; }

        string Version { get; set; }

        string EmailAddress { get; set; }

        string CompanyName { get; set; }

        string WebsiteName { get; set; }
    }
}