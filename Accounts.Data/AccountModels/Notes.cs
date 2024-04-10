using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class Notes
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public int? TimesheetId { get; set; }

        public virtual Timesheets Timesheet { get; set; }
    }
}
