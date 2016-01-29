#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-20-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.ComponentModel.DataAnnotations;

namespace Ideal.Core.Model.Logging
{
    #region

    

    #endregion

    [DisplayColumn("Log")]
    public class Log : DomainObject
    {
        public string Message { get; set; }

        public string MachineName { get; set; }

        public string Details { get; set; }

        public string EventType { get; set; }

        public decimal EventSequence { get; set; }

        public decimal EventOccurrence { get; set; }

        public string RequestUrl { get; set; }

        public int EventCode { get; set; }

        public string ExceptionType { get; set; }

        public int EventDetailCode { get; set; }

        public string ApplicationPath { get; set; }

        public string ApplicationVirtualPath { get; set; }

        public string Tenant { get; set; }

        public string Username { get; set; }
    }
}
