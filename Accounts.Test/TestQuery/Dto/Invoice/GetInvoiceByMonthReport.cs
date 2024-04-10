using Accounts.Test.TestQuery.Dto.Invoice.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Invoice
{
    public class GetInvoiceByMonthReport : CommonDto
    {
        public GetInvoiceByMonthReport()
        {
            result = new List<InvoiceMonthReportDto>();
        }

        public List<InvoiceMonthReportDto> result { get; set; }
    }
}