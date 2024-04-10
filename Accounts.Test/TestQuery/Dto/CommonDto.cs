using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto
{
    public class CommonDto
    {
        public string targetUrl { get; set; }
        public bool success => true;
        public string error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp => true;
    }
}