using Accounts.Test.TestQuery.Dto.Hourlog.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog
{
    public class ProjectHourMonthlyReport : CommonDto
    {
        public ProjectHourMonthlyReport()
        {
            Result = new List<HourMonthlyReport>();
        }

        public List<HourMonthlyReport> Result { get; set; }
    }
}