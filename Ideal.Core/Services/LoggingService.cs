using Ideal.Core.Interfaces.Data;
using Ideal.Core.Model;

namespace Ideal.Core.Services
{
    public class LoggingService : BaseService<Log>
    {
        private IRepository<Log> _repository;

        public LoggingService(IUnitOfWork unitOfWork, IRepository<Log> repository) : base(unitOfWork)
        {
            this.Repository = _repository = repository;
        }
    }
}
