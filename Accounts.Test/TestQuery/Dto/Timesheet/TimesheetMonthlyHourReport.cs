using Accounts.Test.TestQuery.Helper;
using System.Collections.Generic;

namespace Accounts.Test.TestQuery.Dto.Timesheet
{
    public class TimesheetMonthlyHourReport : CommonDto
    {
        public TimesheetMonthlyHourReport()
        {
            Result = new List<MonthlySummary>();
        }

        public List<MonthlySummary> Result { get; set; }
    }
}