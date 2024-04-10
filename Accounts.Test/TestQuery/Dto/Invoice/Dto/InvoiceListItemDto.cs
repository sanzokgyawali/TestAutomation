using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Invoice.Dto
{
    public class InvoiceListItemDto
    {
        public string QBOInvoiceId { get; set; }
        public string CompanyName { get; set; }
        public string ConsultantName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Total { get; set; }
    }
}