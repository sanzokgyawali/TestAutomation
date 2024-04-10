using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class InvoiceCycles
    {
        public InvoiceCycles()
        {
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
