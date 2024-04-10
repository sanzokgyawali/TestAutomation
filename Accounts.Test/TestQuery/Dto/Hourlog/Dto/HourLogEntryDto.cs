using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog.Dto
{
    public class HourLogEntryDto
    {
        public int? ProjectId { get; set; }

        public DateTime Day { get; set; }

        public double? Hours { get; set; }

        public bool IsAssociatedWithTimesheet { get; set; }

        public int TimesheetStatusesId { get; set; }
        public int Id { get; set; }
    }
}