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

namespace Ideal.Core.Model
{
    #region

    

    #endregion

    public partial class User
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Comment { get; set; }


        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}