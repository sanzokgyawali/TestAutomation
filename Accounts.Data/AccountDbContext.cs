using Accounts.Data.AccountModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Accounts.Data
{
    public partial class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbpAuditLogs> AbpAuditLogs { get; set; }
        public virtual DbSet<AbpBackgroundJobs> AbpBackgroundJobs { get; set; }
        public virtual DbSet<AbpEditions> AbpEditions { get; set; }
        public virtual DbSet<AbpEntityChangeSets> AbpEntityChangeSets { get; set; }
        public virtual DbSet<AbpEntityChanges> AbpEntityChanges { get; set; }
        public virtual DbSet<AbpEntityPropertyChanges> AbpEntityPropertyChanges { get; set; }
        public virtual DbSet<AbpFeatures> AbpFeatures { get; set; }
        public virtual DbSet<AbpLanguageTexts> AbpLanguageTexts { get; set; }
        public virtual DbSet<AbpLanguages> AbpLanguages { get; set; }
        public virtual DbSet<AbpNotificationSubscriptions> AbpNotificationSubscriptions { get; set; }
        public virtual DbSet<AbpNotifications> AbpNotifications { get; set; }
        public virtual DbSet<AbpOrganizationUnitRoles> AbpOrganizationUnitRoles { get; set; }
        public virtual DbSet<AbpOrganizationUnits> AbpOrganizationUnits { get; set; }
        public virtual DbSet<AbpPermissions> AbpPermissions { get; set; }
        public virtual DbSet<AbpRoleClaims> AbpRoleClaims { get; set; }
        public virtual DbSet<AbpRoles> AbpRoles { get; set; }
        public virtual DbSet<AbpSettings> AbpSettings { get; set; }
        public virtual DbSet<AbpTenantNotifications> AbpTenantNotifications { get; set; }
        public virtual DbSet<AbpTenants> AbpTenants { get; set; }
        public virtual DbSet<AbpUserAccounts> AbpUserAccounts { get; set; }
        public virtual DbSet<AbpUserClaims> AbpUserClaims { get; set; }
        public virtual DbSet<AbpUserLoginAttempts> AbpUserLoginAttempts { get; set; }
        public virtual DbSet<AbpUserLogins> AbpUserLogins { get; set; }
        public virtual DbSet<AbpUserNotifications> AbpUserNotifications { get; set; }
        public virtual DbSet<AbpUserOrganizationUnits> AbpUserOrganizationUnits { get; set; }
        public virtual DbSet<AbpUserRoles> AbpUserRoles { get; set; }
        public virtual DbSet<AbpUserTokens> AbpUserTokens { get; set; }
        public virtual DbSet<AbpUsers> AbpUsers { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Consultants> Consultants { get; set; }
        public virtual DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<HourLogEntries> HourLogEntries { get; set; }
        public virtual DbSet<InvoiceCycles> InvoiceCycles { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<LineItems> LineItems { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Terms> Terms { get; set; }
        public virtual DbSet<TimesheetPeriods> TimesheetPeriods { get; set; }
        public virtual DbSet<TimesheetStatuses> TimesheetStatuses { get; set; }
        public virtual DbSet<Timesheets> Timesheets { get; set; }

        private ILoggerFactory AccountFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(AccountFactory());
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbpAuditLogs>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.ExecutionDuration });

                entity.HasIndex(e => new { e.TenantId, e.ExecutionTime });

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.Property(e => e.BrowserInfo).HasMaxLength(512);

                entity.Property(e => e.ClientIpAddress).HasMaxLength(64);

                entity.Property(e => e.ClientName).HasMaxLength(128);

                entity.Property(e => e.CustomData).HasMaxLength(2000);

                entity.Property(e => e.Exception).HasMaxLength(2000);

                entity.Property(e => e.MethodName).HasMaxLength(256);

                entity.Property(e => e.Parameters).HasMaxLength(1024);

                entity.Property(e => e.ServiceName).HasMaxLength(256);
            });

            modelBuilder.Entity<AbpBackgroundJobs>(entity =>
            {
                entity.HasIndex(e => new { e.IsAbandoned, e.NextTryTime });

                entity.Property(e => e.JobArgs).IsRequired();

                entity.Property(e => e.JobType)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<AbpEditions>(entity =>
            {
                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<AbpEntityChangeSets>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.CreationTime });

                entity.HasIndex(e => new { e.TenantId, e.Reason });

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.Property(e => e.BrowserInfo).HasMaxLength(512);

                entity.Property(e => e.ClientIpAddress).HasMaxLength(64);

                entity.Property(e => e.ClientName).HasMaxLength(128);

                entity.Property(e => e.Reason).HasMaxLength(256);
            });

            modelBuilder.Entity<AbpEntityChanges>(entity =>
            {
                entity.HasIndex(e => e.EntityChangeSetId);

                entity.HasIndex(e => new { e.EntityTypeFullName, e.EntityId });

                entity.Property(e => e.EntityId).HasMaxLength(48);

                entity.Property(e => e.EntityTypeFullName).HasMaxLength(192);

                entity.HasOne(d => d.EntityChangeSet)
                    .WithMany(p => p.AbpEntityChanges)
                    .HasForeignKey(d => d.EntityChangeSetId);
            });

            modelBuilder.Entity<AbpEntityPropertyChanges>(entity =>
            {
                entity.HasIndex(e => e.EntityChangeId);

                entity.Property(e => e.NewValue).HasMaxLength(512);

                entity.Property(e => e.OriginalValue).HasMaxLength(512);

                entity.Property(e => e.PropertyName).HasMaxLength(96);

                entity.Property(e => e.PropertyTypeFullName).HasMaxLength(192);

                entity.HasOne(d => d.EntityChange)
                    .WithMany(p => p.AbpEntityPropertyChanges)
                    .HasForeignKey(d => d.EntityChangeId);
            });

            modelBuilder.Entity<AbpFeatures>(entity =>
            {
                entity.HasIndex(e => new { e.EditionId, e.Name });

                entity.HasIndex(e => new { e.TenantId, e.Name });

                entity.Property(e => e.Discriminator).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.AbpFeatures)
                    .HasForeignKey(d => d.EditionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AbpLanguageTexts>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.Source, e.LanguageName, e.Key });

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<AbpLanguages>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.Name });

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Icon).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<AbpNotificationSubscriptions>(entity =>
            {
                entity.HasIndex(e => new { e.NotificationName, e.EntityTypeName, e.EntityId, e.UserId });

                entity.HasIndex(e => new { e.TenantId, e.NotificationName, e.EntityTypeName, e.EntityId, e.UserId });

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EntityId).HasMaxLength(96);

                entity.Property(e => e.EntityTypeAssemblyQualifiedName).HasMaxLength(512);

                entity.Property(e => e.EntityTypeName).HasMaxLength(250);

                entity.Property(e => e.NotificationName).HasMaxLength(96);
            });

            modelBuilder.Entity<AbpNotifications>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DataTypeName).HasMaxLength(512);

                entity.Property(e => e.EntityId).HasMaxLength(96);

                entity.Property(e => e.EntityTypeAssemblyQualifiedName).HasMaxLength(512);

                entity.Property(e => e.EntityTypeName).HasMaxLength(250);

                entity.Property(e => e.NotificationName)
                    .IsRequired()
                    .HasMaxLength(96);
            });

            modelBuilder.Entity<AbpOrganizationUnitRoles>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.OrganizationUnitId });

                entity.HasIndex(e => new { e.TenantId, e.RoleId });
            });

            modelBuilder.Entity<AbpOrganizationUnits>(entity =>
            {
                entity.HasIndex(e => e.ParentId);

                entity.HasIndex(e => new { e.TenantId, e.Code });

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(95);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId);
            });

            modelBuilder.Entity<AbpPermissions>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.Name });

                entity.Property(e => e.Discriminator).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AbpPermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AbpRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => new { e.TenantId, e.ClaimType });

                entity.Property(e => e.ClaimType).HasMaxLength(256);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AbpRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AbpRoles>(entity =>
            {
                entity.HasIndex(e => e.CreatorUserId);

                entity.HasIndex(e => e.DeleterUserId);

                entity.HasIndex(e => e.LastModifierUserId);

                entity.HasIndex(e => new { e.TenantId, e.NormalizedName });

                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(128);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.HasOne(d => d.CreatorUser)
                    .WithMany(p => p.AbpRolesCreatorUser)
                    .HasForeignKey(d => d.CreatorUserId);

                entity.HasOne(d => d.DeleterUser)
                    .WithMany(p => p.AbpRolesDeleterUser)
                    .HasForeignKey(d => d.DeleterUserId);

                entity.HasOne(d => d.LastModifierUser)
                    .WithMany(p => p.AbpRolesLastModifierUser)
                    .HasForeignKey(d => d.LastModifierUserId);
            });

            modelBuilder.Entity<AbpSettings>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.Name, e.UserId })
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpSettings)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AbpTenantNotifications>(entity =>
            {
                entity.HasIndex(e => e.TenantId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DataTypeName).HasMaxLength(512);

                entity.Property(e => e.EntityId).HasMaxLength(96);

                entity.Property(e => e.EntityTypeAssemblyQualifiedName).HasMaxLength(512);

                entity.Property(e => e.EntityTypeName).HasMaxLength(250);

                entity.Property(e => e.NotificationName)
                    .IsRequired()
                    .HasMaxLength(96);
            });

            modelBuilder.Entity<AbpTenants>(entity =>
            {
                entity.HasIndex(e => e.CreatorUserId);

                entity.HasIndex(e => e.DeleterUserId);

                entity.HasIndex(e => e.EditionId);

                entity.HasIndex(e => e.LastModifierUserId);

                entity.HasIndex(e => e.TenancyName);

                entity.Property(e => e.ConnectionString).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.TenancyName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.CreatorUser)
                    .WithMany(p => p.AbpTenantsCreatorUser)
                    .HasForeignKey(d => d.CreatorUserId);

                entity.HasOne(d => d.DeleterUser)
                    .WithMany(p => p.AbpTenantsDeleterUser)
                    .HasForeignKey(d => d.DeleterUserId);

                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.AbpTenants)
                    .HasForeignKey(d => d.EditionId);

                entity.HasOne(d => d.LastModifierUser)
                    .WithMany(p => p.AbpTenantsLastModifierUser)
                    .HasForeignKey(d => d.LastModifierUserId);
            });

            modelBuilder.Entity<AbpUserAccounts>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress);

                entity.HasIndex(e => e.UserName);

                entity.HasIndex(e => new { e.TenantId, e.EmailAddress });

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.HasIndex(e => new { e.TenantId, e.UserName });

                entity.Property(e => e.EmailAddress).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AbpUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.ClaimType });

                entity.Property(e => e.ClaimType).HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AbpUserLoginAttempts>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.TenantId });

                entity.HasIndex(e => new { e.TenancyName, e.UserNameOrEmailAddress, e.Result });

                entity.Property(e => e.BrowserInfo).HasMaxLength(512);

                entity.Property(e => e.ClientIpAddress).HasMaxLength(64);

                entity.Property(e => e.ClientName).HasMaxLength(128);

                entity.Property(e => e.TenancyName).HasMaxLength(64);

                entity.Property(e => e.UserNameOrEmailAddress).HasMaxLength(255);
            });

            modelBuilder.Entity<AbpUserLogins>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.HasIndex(e => new { e.TenantId, e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderKey)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AbpUserNotifications>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.State, e.CreationTime });

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AbpUserOrganizationUnits>(entity =>
            {
                entity.HasIndex(e => new { e.TenantId, e.OrganizationUnitId });

                entity.HasIndex(e => new { e.TenantId, e.UserId });
            });

            modelBuilder.Entity<AbpUserRoles>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.RoleId });

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AbpUserTokens>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.TenantId, e.UserId });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.Value).HasMaxLength(512);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AbpUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AbpUsers>(entity =>
            {
                entity.HasIndex(e => e.CreatorUserId);

                entity.HasIndex(e => e.DeleterUserId);

                entity.HasIndex(e => e.LastModifierUserId);

                entity.HasIndex(e => new { e.TenantId, e.NormalizedEmailAddress });

                entity.HasIndex(e => new { e.TenantId, e.NormalizedUserName });

                entity.Property(e => e.AuthenticationSource).HasMaxLength(64);

                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(128);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EmailConfirmationCode).HasMaxLength(328);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NormalizedEmailAddress)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordResetCode).HasMaxLength(328);

                entity.Property(e => e.PhoneNumber).HasMaxLength(32);

                entity.Property(e => e.SecurityStamp).HasMaxLength(128);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.CreatorUser)
                    .WithMany(p => p.InverseCreatorUser)
                    .HasForeignKey(d => d.CreatorUserId);

                entity.HasOne(d => d.DeleterUser)
                    .WithMany(p => p.InverseDeleterUser)
                    .HasForeignKey(d => d.DeleterUserId);

                entity.HasOne(d => d.LastModifierUser)
                    .WithMany(p => p.InverseLastModifierUser)
                    .HasForeignKey(d => d.LastModifierUserId);
            });

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.HasIndex(e => e.InvoiceId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.TimesheetId);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.InvoiceId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ProjectId);

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TimesheetId);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasIndex(e => e.TermId);

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.TermId);
            });

            modelBuilder.Entity<Consultants>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.HasIndex(e => e.ExpenseTypeId);

                entity.HasIndex(e => e.TimesheetId);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ExpenseType)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.ExpenseTypeId);

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.TimesheetId);
            });

            modelBuilder.Entity<HourLogEntries>(entity =>
            {
                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.TimesheetId);

                entity.HasIndex(e => new { e.ProjectId, e.Day, e.IsDeleted })
                    .IsUnique();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.HourLogEntries)
                    .HasForeignKey(d => d.ProjectId);

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.HourLogEntries)
                    .HasForeignKey(d => d.TimesheetId);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasIndex(e => e.CompanyId);

                entity.HasIndex(e => e.ConsultantId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.TermId);

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProjectId).HasDefaultValueSql("((2))");

                entity.Property(e => e.QboinvoiceId).HasColumnName("QBOInvoiceId");

                entity.Property(e => e.ServiceTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ConsultantId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.TermId);
            });

            modelBuilder.Entity<LineItems>(entity =>
            {
                entity.HasIndex(e => e.ExpenseTypeId);

                entity.HasIndex(e => e.InvoiceId);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ExpenseType)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.ExpenseTypeId);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.InvoiceId);
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasIndex(e => e.TimesheetId);

                entity.Property(e => e.NoteTitle).HasMaxLength(200);

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.TimesheetId);
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasIndex(e => e.CompanyId);

                entity.HasIndex(e => e.ConsultantId);

                entity.HasIndex(e => e.InvoiceCycleId);

                entity.HasIndex(e => e.TermId);

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ConsultantId);

                entity.HasOne(d => d.InvoiceCycle)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.InvoiceCycleId);

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.TermId);
            });

            modelBuilder.Entity<Timesheets>(entity =>
            {
                entity.HasIndex(e => e.ApprovedByUserId);

                entity.HasIndex(e => e.CreatorUserId);

                entity.HasIndex(e => e.InvoiceGeneratedByUserId);

                entity.HasIndex(e => e.InvoiceId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.StatusId);

                entity.Property(e => e.EndDt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.StartDt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.ApprovedByUser)
                    .WithMany(p => p.TimesheetsApprovedByUser)
                    .HasForeignKey(d => d.ApprovedByUserId);

                entity.HasOne(d => d.CreatorUser)
                    .WithMany(p => p.TimesheetsCreatorUser)
                    .HasForeignKey(d => d.CreatorUserId);

                entity.HasOne(d => d.InvoiceGeneratedByUser)
                    .WithMany(p => p.TimesheetsInvoiceGeneratedByUser)
                    .HasForeignKey(d => d.InvoiceGeneratedByUserId);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.InvoiceId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.ProjectId);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.StatusId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}