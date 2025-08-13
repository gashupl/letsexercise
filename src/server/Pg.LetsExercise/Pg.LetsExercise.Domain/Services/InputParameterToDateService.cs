using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Repositories;
using System;

namespace Pg.LetsExercise.Domain.Services
{
    public class InputParameterToDateService : ServiceBase, IInputParameterToDateService
    {
        public InputParameterToDateService(IRepository repository, ITracingService tracingService) : base(repository, tracingService)
        {
        }

        public DateTime GetDate(string inputParameter)
        {
            if (string.IsNullOrWhiteSpace(inputParameter))
            {
                throw new ArgumentException("Input parameter cannot be null or empty.", nameof(inputParameter));
            }

            DateTime result;

            // Try parsing with common formats
            var formats = new[] { "yyyy-MM-dd", "yyyy/MM/dd", "dd/MM/yyyy", "MM/dd/yyyy" };
            if (DateTime.TryParseExact(inputParameter, formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            // Fallback to general parsing
            if (DateTime.TryParse(inputParameter, out result))
            {
                return result;
            }

            throw new ArgumentException($"Input parameter '{inputParameter}' is not a valid date.", nameof(inputParameter));
        }
    }
}
