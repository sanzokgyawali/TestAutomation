using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class LineItems
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ServiceDt { get; set; }
        public int? InvoiceId { get; set; }
        public int ExpenseTypeId { get; set; }

        public virtual ExpenseTypes ExpenseType { get; set; }
        public virtual Invoices Invoice { get; set; }
    }
}
