using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Expenses
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string Comment { get; set; }
        public DateTime ReportDt { get; set; }
        public int? TimesheetId { get; set; }
        public int? ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }

        public virtual ExpenseTypes ExpenseType { get; set; }
        public virtual Timesheets Timesheet { get; set; }
    }
}
