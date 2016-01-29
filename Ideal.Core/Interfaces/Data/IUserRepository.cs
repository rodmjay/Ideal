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

using Ideal.Core.Model.Membership;

namespace Ideal.Core.Interfaces.Data
{
    public partial interface IUserRepository : IRepository<User>
    {		
		// Add extra datainterface methods in a partial interface
	}
}
