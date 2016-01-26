#region credits
// ***********************************************************************
// Assembly	: Ideal
// Author	: Rod Johnson
// Created	: 03-17-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;
using Ideal.Core.Interfaces.Validation;

namespace Ideal
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Processes the specified model state.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool Process(this ModelStateDictionary modelState, IValidationContainer result)
        {
            foreach (var r in result.ValidationErrors)
                foreach (var e in r.Value)
                    modelState.AddModelError(r.Key, e);
            return modelState.IsValid;
        }
    }
}