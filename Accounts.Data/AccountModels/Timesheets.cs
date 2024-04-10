using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Timesheets
    {
        public Timesheets()
        {
            Attachments = new HashSet<Attachments>();
            Expenses = new HashSet<Expenses>();
            HourLogEntries = new HashSet<HourLogEntries>();
            Notes = new HashSet<Notes>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public long? ApprovedByUserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? InvoiceId { get; set; }
        public long? InvoiceGeneratedByUserId { get; set; }
        public DateTime? InvoiceGeneratedDate { get; set; }
        public DateTime EndDt { get; set; }
        public DateTime StartDt { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public double TotalHrs { get; set; }

        public virtual AbpUsers ApprovedByUser { get; set; }
        public virtual AbpUsers CreatorUser { get; set; }
        public virtual Invoices Invoice { get; set; }
        public virtual AbpUsers InvoiceGeneratedByUser { get; set; }
        public virtual Projects Project { get; set; }
        public virtual TimesheetStatuses Status { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<HourLogEntries> HourLogEntries { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
    }
}