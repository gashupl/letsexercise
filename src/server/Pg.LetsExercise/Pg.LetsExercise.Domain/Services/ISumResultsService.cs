using Pg.LetsExercise.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Domain.Services
{
    public interface ISumResultsService
    {
        List<MonthlyResult> GetMonthlyResults(DateTime startDate, DateTime endDate, Guid ownerId); 
    }
}
