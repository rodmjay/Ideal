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

using System.Collections.Generic;

namespace Ideal.Core.Common.Lists
{
    #region

    

    #endregion

    public static partial class Lists
    {
        public static readonly IDictionary<string, string> EventStatusDictionary = new Dictionary<string, string> 
     
        {
            {"Info", "Info"},
            {"Error", "Error"},
            {"Warning", "Warning"},
            {"Success", "Success"},
            {"Debug", "Debug"},
            {"Fatal", "Fatal"}
        };
    }
}
