using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Timesheet.Dto
{
    public class TimesheetQueryParameters
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int? ConsultantId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public int[] StatusId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}