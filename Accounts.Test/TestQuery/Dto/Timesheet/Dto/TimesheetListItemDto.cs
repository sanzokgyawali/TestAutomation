using Accounts.Test.TestQuery.Helper;
using System;

namespace Accounts.Test.TestQuery.Dto.Timesheet.Dto
{
    public class TimesheetListItemDto
    {
        public int? Id { get; set; }

        public ProjectDto Project { get; set; }

        public int StatusId { get; set; }

        public DateTime StartDt { get; set; }

        public DateTime EndDt { get; set; }

        public int ProjectId { get; set; }

        public double TotalHrs { get; set; }

        public DateTime CreatedDt { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string ApprovedByUserName { get; set; }

        public string QBOInvoiceId { get; set; }
    }
}