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
        public static readonly IDictionary<string, string> MonthDictionary = new Dictionary<string, string> 
        {
            {"", ""},
            {"January", "01"},
            {"February", "02"},
            {"March", "03"},
            {"April", "04"},
            {"May", "05"},
            {"June", "06"},
            {"July", "07"},
            {"August", "08"},
            {"September", "09"},
            {"October", "10"},
            {"November", "11"},
            {"December", "12"}
        };
    }
}
