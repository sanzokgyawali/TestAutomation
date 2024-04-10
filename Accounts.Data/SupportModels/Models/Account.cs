using Microsoft.AspNetCore.Identity;
using SupportingApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportingApplication.Data.Models
{
    public class Account : CommonEntity
    {
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BlobUrl { get; set; }
        public int AccountTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneTypeId { get; set; }
        public string IdentityUserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual PhoneType PhoneType { get; set; }
    }
}
