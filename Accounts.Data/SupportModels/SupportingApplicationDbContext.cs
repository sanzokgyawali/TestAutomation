//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupportingApplication.Data.Models;

namespace Supporting.Data
{
    public class SupportingApplicationDbContext : DbContext
    {
        public SupportingApplicationDbContext(DbContextOptions<SupportingApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<SupportRequest> SupportRequests { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<UrgencyStatus> UrgencyStatuses { get; set; }
        public virtual DbSet<TechnologyType> TechnologyTypes { get; set; }
    }
}
