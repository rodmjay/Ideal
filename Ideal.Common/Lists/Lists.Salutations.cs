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

namespace Ideal.Lists
{
    #region

    

    #endregion

    public static partial class Lists
    {
        public static readonly IDictionary<string, string> SalutationDirectory = new Dictionary<string, string> 
        {
            {"", ""},
            {"Dr.", "DR"},
            {"Hr.", "HR"},
            {"Mr.", "MR"},
            {"Mrs.", "MRS"},
            {"Ms.", "MS"},
            {"Sir.", "SIR"},
        };
    }
}
