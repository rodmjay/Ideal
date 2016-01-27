using Ideal.Core.Interfaces.Data;
using Ideal.Core.Model;
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
