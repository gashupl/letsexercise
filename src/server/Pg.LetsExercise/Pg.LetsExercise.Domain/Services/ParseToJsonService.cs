using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using Pg.LetsExercise.Domain.Repositories;
using System;

namespace Pg.LetsExercise.Domain.Services
{
    public class ParseToJsonService : ServiceBase, IParseToJsonService
    {
        public ParseToJsonService(IRepository repository, ITracingService tracingService) : base(repository, tracingService)
        {
        }

        public string Parse(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data);
        }
    }
}
