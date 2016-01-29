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

namespace Ideal.Core.Model.Membership
{
    #region

    

    #endregion

    public sealed partial class User
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }


        public string FullName => FirstName + " " + LastName;
    }
}