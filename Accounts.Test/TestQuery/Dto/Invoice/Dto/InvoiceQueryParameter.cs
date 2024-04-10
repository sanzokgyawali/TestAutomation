using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Invoice.Dto
{
    public class InvoiceQueryParameter
    {
        public int? CompanyId { get; set; }

        public int? ConsultantId { get; set; }

        public int? ProjectId { get; set; }

        public string ConsultantName { get; set; }

        public string CompanyName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}