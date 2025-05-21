using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsExercise.Domain.Services
{
    public interface IInputParameterToDateService
    {
        DateTime GetDate(string inputParamerer); 
    }
}
