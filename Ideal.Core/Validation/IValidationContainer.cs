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

using System.Collections.Generic;

namespace Ideal.Core.Validation
{
    #region

    

    #endregion

    public interface IValidationContainer<out T> : IValidationContainer
    {        
        T Entity { get;  }
    }

    public interface IValidationContainer
    {
        IDictionary<string, IList<string>> ValidationErrors { get; }
        bool IsValid { get; }
    }
}