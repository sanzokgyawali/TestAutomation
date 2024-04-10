using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class AbpTenants
    {
        public int Id { get; set; }
        public string ConnectionString { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? EditionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string Name { get; set; }
        public string TenancyName { get; set; }

        public virtual AbpUsers CreatorUser { get; set; }
        public virtual AbpUsers DeleterUser { get; set; }
        public virtual AbpEditions Edition { get; set; }
        public virtual AbpUsers LastModifierUser { get; set; }
    }
}
