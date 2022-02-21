using Microsoft.EntityFrameworkCore;

namespace API.Database.Extensions
{
    public static class SeedingExtensions
    {
        private static List<string> accountActivities = new List<string>() { "актив.", "пассив.", "актив.-пассив." };
        private static List<string> currencies = new List<string>() { "USD", "BYN", "RUB" };
        private static List<decimal> currenciesBalances = new List<decimal>() {10e3M, 10e9M, 10e4M };

        public static void SeedAccountActivities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountActivity>().HasData(accountActivities.Select((a, i) => new AccountActivity()
            {
                Id = i + 1,
                Name = a
            }));
        }

        public static void SeedCurrencies(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(currencies.Select((c, i) => new Currency()
            {
                Id = i + 1,
                Name = c
            }));
        }

        public static void SeedAccounts(this ModelBuilder modelBuilder)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                modelBuilder.Entity<Account>().HasData(
                    new Account() {
                        Id = i*2+1,
                        Name = $"Касса банка {currencies[i]}",
                        Debit = 0,
                        Credit = 0,
                        Balance = 0,
                        IndividualNumber = i+1,
                        AccountTypeId = 1010,  
                        AccountActivityId = 1,
                        OwnerId = null,
                        CurrencyId = i+1,
                    },
                    new Account()
                    {
                        Id = i*2+2,
                        Name = $"Счет фонда развития банка {currencies[i]}",
                        Debit = 0,
                        Credit = 0,
                        Balance = currenciesBalances[i],
                        IndividualNumber = i+1,
                        AccountTypeId = 7327,
                        AccountActivityId = 2,
                        OwnerId = null,
                        CurrencyId = i + 1,
                    }
                );
            }
        }

        public static void SeedDepositTypes(this ModelBuilder modelBuilder)
        {
            var types = new List<string>() { "Отзывный", "Безотзывный" };
            modelBuilder.Entity<DepositType>().HasData(types.Select((d, i) => new DepositType()
            {
                Id = i + 1,
                Name = d
            }));
        }

        public static void SeedCreditTypes(this ModelBuilder modelBuilder)
        {
            var types = new List<string>() { "Аннуитетный", "Дифференцированный" };
            modelBuilder.Entity<CreditType>().HasData(types.Select((d, i) => new CreditType()
            {
                Id = i + 1,
                Name = d
            }));
        }

        public static void SeedAccountsPlan(this ModelBuilder modelBuilder)
        {
            var accountClasses = new Dictionary<int, string>() {
                { 10, "ДЕНЕЖНЫЕ СРЕДСТВА" },
                { 21, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С КОММЕРЧЕСКИМИ ОРГАНИЗАЦИЯМИ" },
                { 23, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С ИНДИВИДУАЛЬНЫМИ ПРЕДПРИНИМАТЕЛЯМИ" },
                { 24, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С ФИЗИЧЕСКИМИ ЛИЦАМИ" },
                { 12, "СРЕДСТВА В НАЦИОНАЛЬНОМ БАНКЕ И ЦЕНТРАЛЬНЫХ(НАЦИОНАЛЬНЫХ) БАНКАХ ИНОСТРАННЫХ ГОСУДАРСТВ" },
                { 13, "ДРАГОЦЕННЫЕ МЕТАЛЛЫ И ДРАГОЦЕННЫЕ КАМНИ" },
                { 15, "СРЕДСТВА В ДРУГИХ БАНКАХ, СПЕЦИАЛИЗИРОВАННЫХ ФИНАНСОВЫХ ОРГАНИЗАЦИЯХ" },
                { 16, "СРЕДСТВА НАЦИОНАЛЬНОГО БАНКА" },
                { 17, "СРЕДСТВА ДРУГИХ БАНКОВ, СПЕЦИАЛИЗИРОВАННЫХ ФИНАНСОВЫХ ОРГАНИЗАЦИЙ" },
                { 19, "РЕЗЕРВЫ НА ПОКРЫТИЕ ВОЗМОЖНЫХ УБЫТКОВ И ПО НЕПОЛУЧЕННЫМ ПРОЦЕНТНЫМ ДОХОДАМ ПО ОПЕРАЦИЯМ С БАНКАМИ" },
                { 30, "СРЕДСТВА НА ТЕКУЩИХ (РАСЧЕТНЫХ) БАНКОВСКИХ СЧЕТАХ КЛИЕНТОВ" },
                { 34, "ВКЛАДЫ (ДЕПОЗИТЫ) КЛИЕНТОВ" },
                { 73, "СОБСТВЕННЫЙ КАПИТАЛ" },
                { 80, "ПРОЦЕНТНЫЕ ДОХОДЫ" },
            };

            var accoutSubClasses = new Dictionary<int, Tuple<int, string>>()
            {
                { 101, new Tuple<int, string>(10, "Денежные средства в кассе") },
                { 120, new Tuple<int, string>(12, "Корреспондентские счета в Национальном банке и центральных(национальных) банках иностранных государств") },
                { 210, new Tuple<int, string>(21, "Займы коммерческим организациям") },
                { 230, new Tuple<int, string>(23, "Займы индивидуальным предпринимателям") },
                { 240, new Tuple<int, string>(24, "Займы физическим лицам") },
                { 301, new Tuple<int, string>(30, "Текущие (расчетные) банковские счета клиентов") },     
                { 340, new Tuple<int, string>(34, "Вклады (депозиты) до востребования") },
                { 341, new Tuple<int, string>(10, "Срочные вклады (депозиты)") },
                { 347, new Tuple<int, string>(34, "Начисленные процентные расходы по вкладам(депозитам) клиентов") },
                { 730, new Tuple<int, string>(73, "Уставный фонд") },
                { 732, new Tuple<int, string>(73, "Фонды") },
                { 800, new Tuple<int, string>(80, "Процентные доходы по средствам в Национальном банке и центральных (национальных) банках иностранных государств") },
                { 801, new Tuple<int, string>(80, "Процентные доходы по средствам в других банках, специализированных финансовых организациях") },
                { 802, new Tuple<int, string>(80, "Процентные доходы по кредитам и иным активным операциям с небанковскими финансовыми организациями") },
                
                // Credits
                { 241, new Tuple<int, string>(24, "Краткосрочные кредиты физическим лицам")},
                { 242, new Tuple<int, string>(24, "Долгосрочные кредиты физическим лицам")},
                { 247, new Tuple<int, string>(24, "Начисленные процентные доходы по кредитам и иным активным операциям с физическими лицами") },
            };

            var accoutTypes = new Dictionary<int, Tuple<int, string, char>>()
            {
                // Сredits
                { 2412, new Tuple<int, string, char>(241, "Краткосрочные кредиты физическим лицам на потребительские нужды", 'А')},
                { 2413, new Tuple<int, string, char>(241, "Краткосрочные кредиты физическим лицам на приобретение жилья", 'А')},
                { 2414, new Tuple<int, string, char>(241, "Краткосрочные кредиты физическим лицам на строительство(реконструкцию) жилья", 'А')},
                { 2415, new Tuple<int, string, char>(241, "Иные краткосрочные кредиты физическим лицам на финансирование недвижимости", 'А')},
                { 2421, new Tuple<int, string, char>(242, "Долгосрочные кредиты физическим лицам на приобретение жилья", 'А')},
                { 2422, new Tuple<int, string, char>(242, "Долгосрочные кредиты физическим лицам на строительство(реконструкцию) жилья", 'А')},
                { 2423, new Tuple<int, string, char>(242, "Долгосрочные льготные кредиты физическим лицам на приобретение жилья", 'А')},
                { 2424, new Tuple<int, string, char>(242, "Долгосрочные льготные кредиты физическим лицам на строительство (реконструкцию) жилья", 'А')},
                { 2426, new Tuple<int, string, char>(242, "Долгосрочные льготные кредиты физическим лицам на потребительские нужды", 'А')},
                { 2427, new Tuple<int, string, char>(242, "Долгосрочные кредиты физическим лицам на потребительские нужды", 'А')},
                { 2428, new Tuple<int, string, char>(242, "Иные долгосрочные льготные кредиты физическим лицам на финансирование недвижимости", 'А')},
                { 2429, new Tuple<int, string, char>(242, "Иные долгосрочные кредиты физическим лицам на финансирование недвижимости", 'A')},
                //Credits Percents
                { 2471, new Tuple<int, string, char>(247, "Начисленные процентные доходы по краткосрочным кредитам физическим лицам на потребительские нужды", 'А')},
                { 2472, new Tuple<int, string, char>(247, "Начисленные процентные доходы по краткосрочным кредитам физическим лицам на финансирование недвижимости", 'А')},
                { 2475, new Tuple<int, string, char>(247, "Начисленные процентные доходы по долгосрочным кредитам физическим лицам на потребительские нужды", 'А')},
                { 2476, new Tuple<int, string, char>(247, "Начисленные процентные доходы по долгосрочным кредитам физическим лицам на приобретение жилья", 'А')},
                { 2477, new Tuple<int, string, char>(247, "Начисленные процентные доходы по долгосрочным кредитам физическим лицам на строительство (реконструкцию) жилья", 'А')},
                { 2478, new Tuple<int, string, char>(247, "Начисленные процентные доходы по иным долгосрочным кредитам физическим лицам на финансирование недвижимости", 'А')},
                { 2479, new Tuple<int, string, char>(247, "Начисленные процентные доходы по иным активным операциям с физическими лицами", 'A') },

                { 1010, new Tuple<int, string, char>(101, "Денежные средства в кассе", 'А')},
                { 1201, new Tuple<int, string, char>(120, "Корреспондентский счет в Национальном банке для внутриреспубликанских расчетов", 'А')},
                { 2100, new Tuple<int, string, char>(210, "Займы коммерческим организациям", 'А')},
                { 2300, new Tuple<int, string, char>(230, "Займы индивидуальным предпринимателям", 'А')},
                { 2400, new Tuple<int, string, char>(240, "Займы физическим лицам на потребительские цели", 'А')},
                { 3401, new Tuple<int, string, char>(340, "Вклады(депозиты) до востребования небанковских финансовых организаций", 'П')},
                { 3402, new Tuple<int, string, char>(340, "Вклады(депозиты) до востребования коммерческих организаций", 'П')},
                { 3403, new Tuple<int, string, char>(340, "Вклады(депозиты) до востребования индивидуальных предпринимателей", 'П')},
                { 3404, new Tuple<int, string, char>(340, "Вклады(депозиты) до востребования физических лиц", 'П')},
                { 3405, new Tuple<int, string, char>(340, "Вклады(депозиты) до востребования некоммерческих организаций", 'П')},
                { 3411, new Tuple<int, string ,char>(341, "Срочные вклады (депозиты) небанковских финансовых организаций", 'П')},
                { 3412, new Tuple<int, string ,char>(341, "Срочные вклады (депозиты) коммерческих организаций", 'П')},
                { 3413, new Tuple<int, string ,char>(341, "Срочные вклады (депозиты) индивидуальных предпринимателей", 'П')},
                { 3414, new Tuple<int, string ,char>(341, "Срочные вклады (депозиты) физических лиц", 'П')},
                { 3415, new Tuple<int, string ,char>(341, "Срочные вклады (депозиты) некоммерческих организаций", 'П')},
                { 3470, new Tuple<int, string, char>(347, "Начисленные процентные расходы по вкладам(депозитам) до востребования", 'П')},
                { 3471, new Tuple<int, string, char>(347, "Начисленные процентные расходы по срочным вкладам(депозитам)", 'П')},
                { 3472, new Tuple<int, string, char>(347, "Начисленные процентные расходы по условным вкладам(депозитам)", 'П')},
                { 3011, new Tuple<int, string, char>(301, "Текущие (расчетные) банковские счета небанковских финансовых организаций", 'П')},
                { 3012, new Tuple<int, string, char>(301, "Текущие (расчетные) банковские счета коммерческих организаций", 'П')},
                { 3013, new Tuple<int, string, char>(301, "Текущие (расчетные) банковские счета индивидуальных предпринимателей", 'П')},
                { 3014, new Tuple<int, string, char>(301, "Текущие (расчетные) банковские счета физических лиц ", 'П')},
                { 3015, new Tuple<int, string, char>(301, "Текущие (расчетные) банковские счета некоммерческих организаций", 'П')},
                { 7327, new Tuple<int, string, char>(732, "Фонд развития", 'П')},
                { 8003, new Tuple<int, string, char>(800, "Процентные доходы по вкладам (депозитам) до востребования, размещенным в Национальном банке и центральных (национальных) банках иностранных государств", 'П')},
            };

            modelBuilder.Entity<AccountClass>().HasData(accountClasses.Select((a, i) => new AccountClass()
            {
                Id = a.Key,
                Description = a.Value
            }));

            modelBuilder.Entity<AccountSubclass>().HasData(accoutSubClasses.Select((a, i) => new AccountSubclass()
            {
                Id = a.Key,
                AccountClassId = a.Value.Item1,
                Description = a.Value.Item2
            }));

            modelBuilder.Entity<AccountType>().HasData(accoutTypes.Select((a, i) => new AccountType()
            {
                Id = a.Key,
                AccountSubclassId = a.Value.Item1,
                Description = a.Value.Item2,
                Charateristic = a.Value.Item3
            }));
        }

        public static void SeedCitizenships(this ModelBuilder modelBuilder)
        {
            var citizenships = new List<string>() {
                "Республика Беларусь", "Другое"
            };
            modelBuilder.Entity<Citizenship>().HasData(citizenships.Select((c, i) => new Citizenship()
            {
                Id = -(i + 1),
                Name = c
            }));
        }

        public static void SeedCities(this ModelBuilder modelBuilder)
        {
            var cities = new List<string>() {
                "Минск","Гомель","Витебск","Могилёв","Гродно","Брест","Бобруйск","Барановичи","Борисов","Пинск","Орша","Мозырь","Лида","Солигорск ","Новополоцк"
            };
            modelBuilder.Entity<City>().HasData(cities.Select((c, i) => new City()
            {
                Id = -(i + 1),
                Name = c
            }));
        }

        public static void SeedDisabilities(this ModelBuilder modelBuilder)
        {
            var disabilities = new List<string>() {
                "Нет", "I степень", "II степень", "III степень", "IV степень"
            };
            modelBuilder.Entity<Disability>().HasData(disabilities.Select((c, i) => new City()
            {
                Id = -(i + 1),
                Name = c
            }));
        }

        public static void SeedFamilyStatuses(this ModelBuilder modelBuilder)
        {
            var familyStatuses = new List<string>() {
                "холост (не замужем)", "женат (замужем)", "разведен (разве­дена)", "вдовец (вдова)"
            };
            modelBuilder.Entity<FamilyStatus>().HasData(familyStatuses.Select((c, i) => new FamilyStatus()
            {
                Id = -(i + 1),
                Name = c
            }));
        }

    }
}
