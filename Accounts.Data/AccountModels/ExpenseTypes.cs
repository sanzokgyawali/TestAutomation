using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class ExpenseTypes
    {
        public ExpenseTypes()
        {
            Expenses = new HashSet<Expenses>();
            LineItems = new HashSet<LineItems>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
    }
}
