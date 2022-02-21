using API.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class BankManagmentSystemContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<AccountClass> AccountClasses { get; set; }
        public DbSet<AccountSubclass> AccountSubclasses { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountActivity> AccountActivities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositType> DepositTypes { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }

        public BankManagmentSystemContext(DbContextOptions<BankManagmentSystemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModifyEntities(modelBuilder);
            Seed(modelBuilder);
        }

        private void ModifyEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.RegistrationCity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // <--

            modelBuilder.Entity<Client>()
                .HasOne(e => e.ResidenceCity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deposit>()
                .HasOne(c => c.MainAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // <--

            modelBuilder.Entity<Deposit>()
                .HasOne(e => e.PercentAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Credit>()
                .HasOne(c => c.MainAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // <--

            modelBuilder.Entity<Credit>()
                .HasOne(e => e.PercentAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasIndex(u => u.PassportIdNumber)
                .IsUnique();
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedCitizenships();
            modelBuilder.SeedFamilyStatuses();
            modelBuilder.SeedCities();
            modelBuilder.SeedDisabilities();
            modelBuilder.SeedAccountsPlan();
            modelBuilder.SeedAccountActivities();
            modelBuilder.SeedAccounts();
            modelBuilder.SeedDepositTypes();
            modelBuilder.SeedCreditTypes();
            modelBuilder.SeedCurrencies();
        }
    }
}