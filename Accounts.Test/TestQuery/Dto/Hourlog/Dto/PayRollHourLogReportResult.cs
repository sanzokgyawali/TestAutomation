using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog.Dto
{
    public class PayRollHourLogReportResult
    {
        public PayRollHourLogReportResult()
        {
            DailyHourLogs = new List<DailyHourLog>();
        }

        public int ProjectId { get; set; }
        public string ConsultantName { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public List<DailyHourLog> DailyHourLogs { get; set; }
    }
}