﻿using System;
using System.Collections.Generic;

namespace Accounts.Data.AccountModels
{
    public partial class AbpLanguageTexts
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string Key { get; set; }
        public string LanguageName { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string Source { get; set; }
        public int? TenantId { get; set; }
        public string Value { get; set; }
    }
}
