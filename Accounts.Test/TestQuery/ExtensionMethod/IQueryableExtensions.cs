using Accounts.Data.AccountModels;
using Accounts.Test.TestQuery.Helper;
using System.Linq;

namespace Accounts.Test.TestQuery.ExtensionMethod
{
    public static class IQueryableExtensions
    {
        public static IQueryable<MonthlySummary> GetMonthlyReport(this IQueryable<HourLogEntries> hourLogEntries)
        {
            return from mhl in hourLogEntries
                   group mhl by new { mhl.Day.Month, mhl.Day.Year } into mg
                   select new MonthlySummary
                   {
                       Month = mg.Key.Month,
                       Year = mg.Key.Year,
                       Value = mg.Sum(y => y.Hours.HasValue ? y.Hours.Value : 0)
                   };
        }
    }
}