using Pg.LetsExercise.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Domain.Services
{
    public interface IMonthlyResultService
    {
        List<MonthlyResult> Get(DateTime startDate, DateTime endDate);
    }
}
