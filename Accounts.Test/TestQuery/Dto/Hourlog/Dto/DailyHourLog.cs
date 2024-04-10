using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog.Dto
{
    public class DailyHourLog
    {
        public double? Hours { get; set; }
        public DateTime Day { get; set; }
        public string Status { get; set; }
    }
}