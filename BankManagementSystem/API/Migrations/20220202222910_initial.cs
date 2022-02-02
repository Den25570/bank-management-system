using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Disabilities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -5, "IV степень" },
                    { -4, "III степень" },
                    { -3, "II степень" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Citizenships");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "FamilyStatuses");
        }
    }
}
