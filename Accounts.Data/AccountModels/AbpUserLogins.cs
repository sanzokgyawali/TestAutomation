﻿using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class AbpUserLogins
    {
        public long Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public int? TenantId { get; set; }
        public long UserId { get; set; }

        public virtual AbpUsers User { get; set; }
    }
}
