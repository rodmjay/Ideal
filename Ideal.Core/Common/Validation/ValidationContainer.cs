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
using Ideal.Core.Interfaces.Validation;

namespace Ideal.Core.Common.Validation
{
    #region

    

    #endregion

    public partial class ValidationContainer<T> : IValidationContainer<T>
    {
        public IDictionary<string, IList<string>> ValidationErrors { get; private set; }
        public T Entity { get; private set; }

        public bool IsValid
        {
            get { return this.ValidationErrors.Count == 0; }
        }

        public ValidationContainer(IDictionary<string, IList<string>> validationErrors, T entity)
        {
            this.ValidationErrors = validationErrors;
            this.Entity = entity;
        }
    }
}
