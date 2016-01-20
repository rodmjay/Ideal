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

namespace Ideal.Core.Interfaces.Data
{
    #region

    

    #endregion

    public interface IDatabaseFactory : IDisposable
    {
        IDataContext Get();
    }
}