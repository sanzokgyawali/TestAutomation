using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Consultants
    {
        public Consultants()
        {
            Invoices = new HashSet<Invoices>();
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
