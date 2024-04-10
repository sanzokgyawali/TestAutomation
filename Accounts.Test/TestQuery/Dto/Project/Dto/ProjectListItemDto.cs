using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Project.Dto
{
    public class ProjectListItemDto
    {
        public int ConsultantId { get; set; }

        public string ConsultantName { get; set; }

        public string CompanyName { get; set; }

        public int CompanyId { get; set; }

        public DateTime StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        public DateTime InvoiceCycleStartDt { get; set; }

        public string InvoiceCycleName { get; set; }

        public string TermName { get; set; }

        public int InvoiceCycleId { get; set; }

        public double Rate { get; set; }

        public double PastTimesheetDays { get; set; }

        public double TotalHoursBilled { get; set; }

        public decimal TotalAmountBilled { get; set; }

        public int Id { get; set; }
    }
}