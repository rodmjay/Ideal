#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideal.Core.Model
{
    #region

    

    #endregion

    public abstract class DomainObject : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Override this method to implement custom validation in your entities
            
            // This is only for making it compile... and returning null will give an exception.
            if (false)
                yield return new ValidationResult("Well, this should not happend...");
        }
    }
}