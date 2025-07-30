using Microsoft.Xrm.Sdk;
using Pg.LetsExercise.Domain.Dto;
using Pg.LetsExercise.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pg.LetsExercise.Domain.Services
{
    public class SumResultsService : ISumResultsService
    {
        private readonly IRepository _repository;
        private readonly ITracingService _tracingService;

        public SumResultsService(IRepository repository, ITracingService tracingService)
        {
            _repository = repository;
            _tracingService = tracingService;
        }

        public List<MonthlyResult> GetMonthlyResults(DateTime startDate, DateTime endDate, Guid ownerId)
        {
            _tracingService.Trace($"GetMonthlyResults executed with startDate: {startDate} and endDate: {endDate}.");

            var results = new List<MonthlyResult>(); 
            var current = new DateTime(startDate.Year, startDate.Month, 1);
            var end = new DateTime(endDate.Year, endDate.Month, 1);
            while (current <= endDate)
            {
                int year = current.Year;
                int month = current.Month;

                var records = _repository.GetSelectedMonthRecords(year, month, ownerId, Model.pg_ExerciseSet.PushUps);
                var result = new MonthlyResult()
                {
                    MonthCodeName = $"{year}-{month:D2}",
                    MonthFriendlyName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                    Result = records.Sum(r => r.pg_scorenumber ?? 0)
                }; 
                results.Add(result);

                _tracingService.Trace($"Result {result.Result} with code name: {result.MonthCodeName}, " +
                    $"friendly name: {result.MonthFriendlyName} added to returned collection");

                current = current.AddMonths(1);
            }

            _tracingService.Trace($"GetMonthlyResults execution completed.");
            return results; 
        }
    }
}
