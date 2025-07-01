using System;

namespace Pg.LetsExercise.Domain.Services
{
    public interface IInputParameterToDateService
    {
        DateTime GetDate(string inputParamerer); 
    }
}
