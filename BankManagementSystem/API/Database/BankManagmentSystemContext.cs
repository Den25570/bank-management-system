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

        public BankManagmentSystemContext(DbContextOptions<BankManagmentSystemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.RegistrationCity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // <--

            modelBuilder.Entity<Client>()
                .HasOne(e => e.ResidenceCity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasIndex(u => u.PassportIdNumber)
                .IsUnique();

            //Department
            var cities = new List<string>() {
                "Минск","Гомель","Витебск","Могилёв","Гродно","Брест","Бобруйск","Барановичи","Борисов","Пинск","Орша","Мозырь","Лида","Солигорск ","Новополоцк" 
            };
            var disabilities = new List<string>() {
                "Нет", "I степень", "II степень", "III степень", "IV степень"
            };
            var familyStatuses = new List<string>() {
                "холост (не замужем)", "женат (замужем)", "разведен (разве­дена)", "вдовец (вдова)"
            };
            var citizenships = new List<string>() {
                "Республика Беларусь", "Другое"
            };
            modelBuilder.Entity<City>().HasData(cities.Select((c, i) => new City() { 
                Id = -(i+1),
                Name = c
            }));
            modelBuilder.Entity<Disability>().HasData(disabilities.Select((c, i) => new Disability()
            {
                Id = -(i + 1),
                Name = c
            }));
            modelBuilder.Entity<FamilyStatus>().HasData(familyStatuses.Select((c, i) => new FamilyStatus()
            {
                Id = -(i + 1),
                Name = c
            }));
            modelBuilder.Entity<Citizenship>().HasData(citizenships.Select((c, i) => new Citizenship()
            {
                Id = -(i + 1),
                Name = c
            }));
        }
    }
}