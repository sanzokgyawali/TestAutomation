using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Terms
    {
        public Terms()
        {
            Companies = new HashSet<Companies>();
            Invoices = new HashSet<Invoices>();
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string Name { get; set; }
        public int DueDays { get; set; }
        public int DiscountDays { get; set; }
        public string ExternalTermId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Companies> Companies { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
