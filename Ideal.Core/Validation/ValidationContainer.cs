#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Collections.Generic;

namespace Ideal.Core.Validation
{
    #region

    

    #endregion

    public partial class ValidationContainer<T> : IValidationContainer<T>
    {
        public IDictionary<string, IList<string>> ValidationErrors { get; }
        public T Entity { get; }

        public bool IsValid => ValidationErrors.Count == 0;

        public ValidationContainer(IDictionary<string, IList<string>> validationErrors, T entity)
        {
            ValidationErrors = validationErrors;
            Entity = entity;
        }
    }
}
