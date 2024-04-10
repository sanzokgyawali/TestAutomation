using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Invoices
    {
        public Invoices()
        {
            Attachments = new HashSet<Attachments>();
            LineItems = new HashSet<LineItems>();
            Timesheets = new HashSet<Timesheets>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public int TermId { get; set; }
        public string Memo { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double TotalHours { get; set; }
        public double Rate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string QboinvoiceId { get; set; }
        public int CompanyId { get; set; }
        public int ConsultantId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public int ProjectId { get; set; }
        public int? DiscountType { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ServiceTotal { get; set; }

        public virtual Companies Company { get; set; }
        public virtual Consultants Consultant { get; set; }
        public virtual Projects Project { get; set; }
        public virtual Terms Term { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
        public virtual ICollection<Timesheets> Timesheets { get; set; }
    }
}
