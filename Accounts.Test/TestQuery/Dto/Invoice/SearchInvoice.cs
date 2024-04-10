using Accounts.Test.TestQuery.Dto.Invoice.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Invoice
{
    public class SearchInvoice : CommonDto
    {
        public SearchInvoiceResult result { get; set; }
    }
}