using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Helper
{
    public class TimesheetSummary
    {
        public DateTime StartDt { get; set; }

        public DateTime EndDt { get; set; }

        public double? TotalHrs { get; set; }
    }
}