using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;

namespace Pg.LetsExercise.Domain.Services
{
    public abstract class ServiceBase
    {
        protected readonly IRepository repository;
        protected readonly ITracingService tracingService;

        public ServiceBase(IRepository repository, ITracingService tracingService)
        {
            this.repository = repository;
            this.tracingService = tracingService;
        }
    }
}
