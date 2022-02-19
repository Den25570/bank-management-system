using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizenships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepositTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountSubclasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountClassId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubclasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSubclasses_AccountClasses_AccountClassId",
                        column: x => x.AccountClassId,
                        principalTable: "AccountClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    PassportIdNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    PassportSeries = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PassportIssuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidenceCityId = table.Column<int>(type: "int", nullable: false),
                    ResidenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberStationary = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PhoneNumberMobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyStatusId = table.Column<int>(type: "int", nullable: false),
                    CitizenshipId = table.Column<int>(type: "int", nullable: false),
                    RegistrationCityId = table.Column<int>(type: "int", nullable: false),
                    RegistrationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisabilityId = table.Column<int>(type: "int", nullable: false),
                    IsRetired = table.Column<bool>(type: "bit", nullable: false),
                    MounthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_RegistrationCityId",
                        column: x => x.RegistrationCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_ResidenceCityId",
                        column: x => x.ResidenceCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Citizenships_CitizenshipId",
                        column: x => x.CitizenshipId,
                        principalTable: "Citizenships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_FamilyStatuses_FamilyStatusId",
                        column: x => x.FamilyStatusId,
                        principalTable: "FamilyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountSubclassId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Charateristic = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTypes_AccountSubclasses_AccountSubclassId",
                        column: x => x.AccountSubclassId,
                        principalTable: "AccountSubclasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndividualNumber = table.Column<int>(type: "int", nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    AccountActivityId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountActivities_AccountActivityId",
                        column: x => x.AccountActivityId,
                        principalTable: "AccountActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositTypeId = table.Column<int>(type: "int", nullable: false),
                    DepositNumber = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ContractTerm = table.Column<int>(type: "int", nullable: false),
                    DepositAmount = table.Column<int>(type: "int", nullable: false),
                    DepositPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MainAccountId = table.Column<int>(type: "int", nullable: false),
                    PercentAccountId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    LastPercentEvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysPassed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_MainAccountId",
                        column: x => x.MainAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_PercentAccountId",
                        column: x => x.PercentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposits_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposits_DepositTypes_DepositTypeId",
                        column: x => x.DepositTypeId,
                        principalTable: "DepositTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountActivities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "актив." },
                    { 2, "пассив." },
                    { 3, "актив.-пассив." }
                });

            migrationBuilder.InsertData(
                table: "AccountClasses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 10, "ДЕНЕЖНЫЕ СРЕДСТВА" },
                    { 12, "СРЕДСТВА В НАЦИОНАЛЬНОМ БАНКЕ И ЦЕНТРАЛЬНЫХ(НАЦИОНАЛЬНЫХ) БАНКАХ ИНОСТРАННЫХ ГОСУДАРСТВ" },
                    { 13, "ДРАГОЦЕННЫЕ МЕТАЛЛЫ И ДРАГОЦЕННЫЕ КАМНИ" },
                    { 15, "СРЕДСТВА В ДРУГИХ БАНКАХ, СПЕЦИАЛИЗИРОВАННЫХ ФИНАНСОВЫХ ОРГАНИЗАЦИЯХ" },
                    { 16, "СРЕДСТВА НАЦИОНАЛЬНОГО БАНКА" },
                    { 17, "СРЕДСТВА ДРУГИХ БАНКОВ, СПЕЦИАЛИЗИРОВАННЫХ ФИНАНСОВЫХ ОРГАНИЗАЦИЙ" },
                    { 19, "РЕЗЕРВЫ НА ПОКРЫТИЕ ВОЗМОЖНЫХ УБЫТКОВ И ПО НЕПОЛУЧЕННЫМ ПРОЦЕНТНЫМ ДОХОДАМ ПО ОПЕРАЦИЯМ С БАНКАМИ" },
                    { 21, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С КОММЕРЧЕСКИМИ ОРГАНИЗАЦИЯМИ" },
                    { 23, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С ИНДИВИДУАЛЬНЫМИ ПРЕДПРИНИМАТЕЛЯМИ" },
                    { 24, "КРЕДИТЫ И ИНЫЕ АКТИВНЫЕ ОПЕРАЦИИ С ФИЗИЧЕСКИМИ ЛИЦАМИ" },
                    { 30, "СРЕДСТВА НА ТЕКУЩИХ (РАСЧЕТНЫХ) БАНКОВСКИХ СЧЕТАХ КЛИЕНТОВ" },
                    { 34, "ВКЛАДЫ (ДЕПОЗИТЫ) КЛИЕНТОВ" },
                    { 73, "СОБСТВЕННЫЙ КАПИТАЛ" },
                    { 80, "ПРОЦЕНТНЫЕ ДОХОДЫ" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -15, "Новополоцк" },
                    { -14, "Солигорск " },
                    { -13, "Лида" },
                    { -12, "Мозырь" },
                    { -11, "Орша" },
                    { -10, "Пинск" },
                    { -9, "Борисов" },
                    { -8, "Барановичи" },
                    { -7, "Бобруйск" },
                    { -6, "Брест" },
                    { -5, "Гродно" },
                    { -4, "Могилёв" },
                    { -3, "Витебск" },
                    { -2, "Гомель" },
                    { -1, "Минск" }
                });

            migrationBuilder.InsertData(
                table: "Citizenships",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -2, "Другое" },
                    { -1, "Республика Беларусь" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "USD" },
                    { 2, "BYN" },
                    { 3, "RUB" }
                });

            migrationBuilder.InsertData(
                table: "DepositTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Отзывный" },
                    { 2, "Безотзывный" }
                });

            migrationBuilder.InsertData(
                table: "Disabilities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -5, "IV степень" },
                    { -4, "III степень" },
                    { -3, "II степень" }
                });

            migrationBuilder.InsertData(
                table: "Disabilities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -2, "I степень" },
                    { -1, "Нет" }
                });

            migrationBuilder.InsertData(
                table: "FamilyStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -4, "вдовец (вдова)" },
                    { -3, "разведен (разве­дена)" },
                    { -2, "женат (замужем)" },
                    { -1, "холост (не замужем)" }
                });

            migrationBuilder.InsertData(
                table: "AccountSubclasses",
                columns: new[] { "Id", "AccountClassId", "Description" },
                values: new object[,]
                {
                    { 101, 10, "Денежные средства в кассе" },
                    { 120, 12, "Корреспондентские счета в Национальном банке и центральных(национальных) банках иностранных государств" },
                    { 210, 21, "Займы коммерческим организациям" },
                    { 230, 23, "Займы индивидуальным предпринимателям" },
                    { 240, 24, "Займы физическим лицам" },
                    { 301, 30, "Текущие (расчетные) банковские счета клиентов" },
                    { 340, 34, "Вклады (депозиты) до востребования" },
                    { 341, 10, "Срочные вклады (депозиты)" },
                    { 347, 34, "Начисленные процентные расходы по вкладам(депозитам) клиентов" },
                    { 730, 73, "Уставный фонд" },
                    { 732, 73, "Фонды" },
                    { 800, 80, "Процентные доходы по средствам в Национальном банке и центральных (национальных) банках иностранных государств" },
                    { 801, 80, "Процентные доходы по средствам в других банках, специализированных финансовых организациях" },
                    { 802, 80, "Процентные доходы по кредитам и иным активным операциям с небанковскими финансовыми организациями" }
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "AccountSubclassId", "Charateristic", "Description" },
                values: new object[,]
                {
                    { 1010, 101, "А", "Денежные средства в кассе" },
                    { 1201, 120, "А", "Корреспондентский счет в Национальном банке для внутриреспубликанских расчетов" },
                    { 2100, 210, "А", "Займы коммерческим организациям" },
                    { 2300, 230, "А", "Займы индивидуальным предпринимателям" },
                    { 2400, 240, "А", "Займы физическим лицам на потребительские цели" },
                    { 3011, 301, "П", "Текущие (расчетные) банковские счета небанковских финансовых организаций" },
                    { 3012, 301, "П", "Текущие (расчетные) банковские счета коммерческих организаций" },
                    { 3013, 301, "П", "Текущие (расчетные) банковские счета индивидуальных предпринимателей" },
                    { 3014, 301, "П", "Текущие (расчетные) банковские счета физических лиц " },
                    { 3015, 301, "П", "Текущие (расчетные) банковские счета некоммерческих организаций" },
                    { 3401, 340, "П", "Вклады(депозиты) до востребования небанковских финансовых организаций" },
                    { 3402, 340, "П", "Вклады(депозиты) до востребования коммерческих организаций" },
                    { 3403, 340, "П", "Вклады(депозиты) до востребования индивидуальных предпринимателей" },
                    { 3404, 340, "П", "Вклады(депозиты) до востребования физических лиц" },
                    { 3405, 340, "П", "Вклады(депозиты) до востребования некоммерческих организаций" },
                    { 3411, 341, "П", "Срочные вклады (депозиты) небанковских финансовых организаций" },
                    { 3412, 341, "П", "Срочные вклады (депозиты) коммерческих организаций" },
                    { 3413, 341, "П", "Срочные вклады (депозиты) индивидуальных предпринимателей" },
                    { 3414, 341, "П", "Срочные вклады (депозиты) физических лиц" },
                    { 3415, 341, "П", "Срочные вклады (депозиты) некоммерческих организаций" },
                    { 3470, 347, "П", "Начисленные процентные расходы по вкладам(депозитам) до востребования" },
                    { 3471, 347, "П", "Начисленные процентные расходы по срочным вкладам(депозитам)" },
                    { 3472, 347, "П", "Начисленные процентные расходы по условным вкладам(депозитам)" },
                    { 7327, 732, "П", "Фонд развития" },
                    { 8003, 800, "П", "Процентные доходы по вкладам (депозитам) до востребования, размещенным в Национальном банке и центральных (национальных) банках иностранных государств" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountActivityId", "AccountTypeId", "Balance", "Credit", "CurrencyId", "Debit", "IndividualNumber", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, 1, 1010, 0m, 0m, 1, 0m, 1, "Касса банка USD", null },
                    { 2, 2, 7327, 10000m, 0m, 1, 0m, 1, "Счет фонда развития банка USD", null },
                    { 3, 1, 1010, 0m, 0m, 2, 0m, 2, "Касса банка BYN", null },
                    { 4, 2, 7327, 10000000000m, 0m, 2, 0m, 2, "Счет фонда развития банка BYN", null },
                    { 5, 1, 1010, 0m, 0m, 3, 0m, 3, "Касса банка RUB", null },
                    { 6, 2, 7327, 100000m, 0m, 3, 0m, 3, "Счет фонда развития банка RUB", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountActivityId",
                table: "Accounts",
                column: "AccountActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubclasses_AccountClassId",
                table: "AccountSubclasses",
                column: "AccountClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_AccountSubclassId",
                table: "AccountTypes",
                column: "AccountSubclassId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CitizenshipId",
                table: "Clients",
                column: "CitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DisabilityId",
                table: "Clients",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FamilyStatusId",
                table: "Clients",
                column: "FamilyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportIdNumber",
                table: "Clients",
                column: "PassportIdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RegistrationCityId",
                table: "Clients",
                column: "RegistrationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ResidenceCityId",
                table: "Clients",
                column: "ResidenceCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ClientId",
                table: "Deposits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_CurrencyId",
                table: "Deposits",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_DepositTypeId",
                table: "Deposits",
                column: "DepositTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_MainAccountId",
                table: "Deposits",
                column: "MainAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_PercentAccountId",
                table: "Deposits",
                column: "PercentAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "DepositTypes");

            migrationBuilder.DropTable(
                name: "AccountActivities");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "AccountSubclasses");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Citizenships");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "FamilyStatuses");

            migrationBuilder.DropTable(
                name: "AccountClasses");
        }
    }
}
