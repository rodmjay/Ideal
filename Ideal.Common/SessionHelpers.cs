#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 03-21-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Configuration;

namespace Ideal
{
    #region

    

    #endregion

    public static class SessionHelpers
    {
        private static int _sessionTimeout;
        public static int GetSessionTimeoutInMinutes
        {
            get
            {
                if (_sessionTimeout == 0)
                {
                    var conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
                    var section = (SessionStateSection)conf.GetSection("system.web/sessionState");
                    _sessionTimeout = (int)section.Timeout.TotalMinutes;
                }
                return _sessionTimeout;
            }
        }
    }
}
