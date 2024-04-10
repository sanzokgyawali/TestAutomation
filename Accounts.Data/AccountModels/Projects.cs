using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Projects
    {
        public Projects()
        {
            Attachments = new HashSet<Attachments>();
            HourLogEntries = new HashSet<HourLogEntries>();
            Invoices = new HashSet<Invoices>();
            Timesheets = new HashSet<Timesheets>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public int TermId { get; set; }
        public int CompanyId { get; set; }
        public int ConsultantId { get; set; }
        public double Rate { get; set; }
        public decimal? DiscountValue { get; set; }
        public int InvoiceCycleId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public int? DiscountType { get; set; }
        public DateTime? InvoiceCycleStartDt { get; set; }

        public virtual Companies Company { get; set; }
        public virtual Consultants Consultant { get; set; }
        public virtual InvoiceCycles InvoiceCycle { get; set; }
        public virtual Terms Term { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<HourLogEntries> HourLogEntries { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<Timesheets> Timesheets { get; set; }
    }
}
