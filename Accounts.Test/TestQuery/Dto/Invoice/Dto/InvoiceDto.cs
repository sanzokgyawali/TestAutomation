using Accounts.Test.TestQuery.Dto.Attachments.Dto;
using Accounts.Test.TestQuery.Dto.LineItems;
using Accounts.Test.TestQuery.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Invoice.Dto
{
    public class InvoiceDto
    {
        public InvoiceDto()
        {
            LineItems = new List<LineItemDto>();
            Attachments = new List<AttachmentDto>();
        }

        public int? TimesheetId { get; set; }
        public string Description { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public double TotalHours { get; set; }

        public double Rate { get; set; }

        public decimal ServiceTotal { get; set; }

        public decimal SubTotal { get; set; }

        public DiscountTypeEnums? DiscountType { get; set; }

        public double? DiscountValue { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total { get; set; }

        public string ConsultantName { get; set; }

        public string CompanyName { get; set; }

        public string TermName { get; set; }

        public string CompanyEmail { get; set; }

        public string QBOInvoiceId { get; set; }

        public List<LineItemDto> LineItems { get; set; }

        public List<AttachmentDto> Attachments { get; set; }

        public int Id { get; set; }
    }
}