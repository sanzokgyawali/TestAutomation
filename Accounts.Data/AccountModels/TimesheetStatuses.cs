using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class TimesheetStatuses
    {
        public TimesheetStatuses()
        {
            Timesheets = new HashSet<Timesheets>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<Timesheets> Timesheets { get; set; }
    }
}
