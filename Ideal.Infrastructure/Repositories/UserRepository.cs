using Ideal.Core.Data;
using Ideal.Identity.Data;
using Ideal.Identity.Model;
using Ideal.Infrastructure.Data;

namespace Ideal.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            
        }
    }
}
