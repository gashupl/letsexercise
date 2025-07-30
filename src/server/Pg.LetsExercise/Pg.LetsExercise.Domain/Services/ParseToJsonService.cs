using Newtonsoft.Json;
using System;

namespace Pg.LetsExercise.Domain.Services
{
    public class ParseToJsonService : IParseToJsonService
    {
        public string Parse(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data);
        }
    }
}
