using Ideal.Core.Data;
using Ideal.Core.Model.Logging;

namespace Ideal.Core.Services.Logging
{
    public class LoggingService : DataService<Log>
    {
        private IRepository<Log> _repository;

        public LoggingService(IUnitOfWork unitOfWork, IRepository<Log> repository) : base(unitOfWork)
        {
            Repository = _repository = repository;
        }
    }
}
