﻿using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class AbpNotifications
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string Data { get; set; }
        public string DataTypeName { get; set; }
        public string EntityId { get; set; }
        public string EntityTypeAssemblyQualifiedName { get; set; }
        public string EntityTypeName { get; set; }
        public string ExcludedUserIds { get; set; }
        public string NotificationName { get; set; }
        public byte Severity { get; set; }
        public string TenantIds { get; set; }
        public string UserIds { get; set; }
    }
}
