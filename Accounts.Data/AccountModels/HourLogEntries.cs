using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class HourLogEntries
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public double? Hours { get; set; }
        public DateTime Day { get; set; }
        public int ProjectId { get; set; }
        public int? TimesheetId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Timesheets Timesheet { get; set; }
    }
}
