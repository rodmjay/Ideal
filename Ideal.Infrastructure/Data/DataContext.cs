#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Data.Entity;
using Ideal.Core.Model;

namespace Ideal.Infrastructure.Data
{
    #region

    #endregion
    public partial class DataContext : BaseContext<DataContext>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Form> Forms { get; set; }
    }
}
