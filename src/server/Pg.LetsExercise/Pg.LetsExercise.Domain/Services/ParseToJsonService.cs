using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
