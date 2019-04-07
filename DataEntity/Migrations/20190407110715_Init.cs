using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataEntity.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Patronymic = table.Column<string>(maxLength: 50, nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Education = table.Column<string>(maxLength: 500, nullable: true),
                    Job = table.Column<string>(maxLength: 500, nullable: true),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    Experience = table.Column<byte>(nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    Rating = table.Column<int>(nullable: true, defaultValue: 1),
                    IsVisible = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CityId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutors_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    ContactTypeId = table.Column<long>(nullable: true),
                    TutorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(maxLength: 30, nullable: false),
                    Operator = table.Column<string>(maxLength: 30, nullable: true),
                    TutorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSpecializations",
                columns: table => new
                {
                    TutorId = table.Column<long>(nullable: false),
                    SpecializationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSpecializations", x => new { x.TutorId, x.SpecializationId });
                    table.ForeignKey(
                        name: "FK_TutorSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSpecializations_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSubjects",
                columns: table => new
                {
                    TutorId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubjects", x => new { x.TutorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TutorSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubjects_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Могилёв" },
                    { 2L, "Минск" },
                    { 3L, "Витебск" },
                    { 4L, "Гомель" },
                    { 5L, "Гродно" },
                    { 6L, "Брест" }
                });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2L, "Viber" },
                    { 3L, "Email" },
                    { 1L, "Skype" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Репетитор для школьника" },
                    { 2L, "Репетитор для студента" },
                    { 3L, "Подготовка к ЦТ" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5L, "Химия" },
                    { 1L, "Математика" },
                    { 2L, "Физика" },
                    { 3L, "История" },
                    { 4L, "Русский язык" },
                    { 6L, "Английский язык" }
                });

            migrationBuilder.InsertData(
                table: "Tutors",
                columns: new[] { "Id", "Address", "CityId", "Cost", "CreateDate", "Description", "Education", "Experience", "FirstName", "IsVisible", "Job", "LastName", "Patronymic", "PhotoPath", "Rating" },
                values: new object[,]
                {
                    { 1L, "Address0", 1L, 0m, new DateTime(2019, 4, 7, 14, 7, 4, 129, DateTimeKind.Local), "Description0", "High", (byte)0, "FirstName0", true, "Job0", "LastName0", "Patronymic0", null, 1 },
                    { 10L, "Address9", 4L, 9m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description9", "Medium", (byte)9, "FirstName9", true, "Job9", "LastName9", "Patronymic9", null, 5 },
                    { 16L, "Address15", 4L, 15m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description15", "Medium", (byte)15, "FirstName15", true, "Job15", "LastName15", "Patronymic15", null, 1 },
                    { 22L, "Address21", 4L, 21m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description21", "Medium", (byte)21, "FirstName21", true, "Job21", "LastName21", "Patronymic21", null, 2 },
                    { 28L, "Address27", 4L, 27m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description27", "Medium", (byte)27, "FirstName27", true, "Job27", "LastName27", "Patronymic27", null, 3 },
                    { 34L, "Address33", 4L, 33m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description33", "Medium", (byte)33, "FirstName33", true, "Job33", "LastName33", "Patronymic33", null, 4 },
                    { 40L, "Address39", 4L, 39m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description39", "Medium", (byte)39, "FirstName39", true, "Job39", "LastName39", "Patronymic39", null, 5 },
                    { 46L, "Address45", 4L, 45m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description45", "Medium", (byte)45, "FirstName45", true, "Job45", "LastName45", "Patronymic45", null, 1 },
                    { 5L, "Address4", 5L, 4m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description4", "High", (byte)4, "FirstName4", true, "Job4", "LastName4", "Patronymic4", null, 5 },
                    { 11L, "Address10", 5L, 10m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description10", "High", (byte)10, "FirstName10", true, "Job10", "LastName10", "Patronymic10", null, 1 },
                    { 17L, "Address16", 5L, 16m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description16", "High", (byte)16, "FirstName16", true, "Job16", "LastName16", "Patronymic16", null, 2 },
                    { 23L, "Address22", 5L, 22m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description22", "High", (byte)22, "FirstName22", true, "Job22", "LastName22", "Patronymic22", null, 3 },
                    { 29L, "Address28", 5L, 28m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description28", "High", (byte)28, "FirstName28", true, "Job28", "LastName28", "Patronymic28", null, 4 },
                    { 35L, "Address34", 5L, 34m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description34", "High", (byte)34, "FirstName34", true, "Job34", "LastName34", "Patronymic34", null, 5 },
                    { 41L, "Address40", 5L, 40m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description40", "High", (byte)40, "FirstName40", true, "Job40", "LastName40", "Patronymic40", null, 1 },
                    { 47L, "Address46", 5L, 46m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description46", "High", (byte)46, "FirstName46", true, "Job46", "LastName46", "Patronymic46", null, 2 },
                    { 6L, "Address5", 6L, 5m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description5", "Medium", (byte)5, "FirstName5", true, "Job5", "LastName5", "Patronymic5", null, 1 },
                    { 12L, "Address11", 6L, 11m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description11", "Medium", (byte)11, "FirstName11", true, "Job11", "LastName11", "Patronymic11", null, 2 },
                    { 18L, "Address17", 6L, 17m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description17", "Medium", (byte)17, "FirstName17", true, "Job17", "LastName17", "Patronymic17", null, 3 },
                    { 24L, "Address23", 6L, 23m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description23", "Medium", (byte)23, "FirstName23", true, "Job23", "LastName23", "Patronymic23", null, 4 },
                    { 30L, "Address29", 6L, 29m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description29", "Medium", (byte)29, "FirstName29", true, "Job29", "LastName29", "Patronymic29", null, 5 },
                    { 36L, "Address35", 6L, 35m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description35", "Medium", (byte)35, "FirstName35", true, "Job35", "LastName35", "Patronymic35", null, 1 },
                    { 4L, "Address3", 4L, 3m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description3", "Medium", (byte)3, "FirstName3", true, "Job3", "LastName3", "Patronymic3", null, 4 },
                    { 45L, "Address44", 3L, 44m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description44", "High", (byte)44, "FirstName44", true, "Job44", "LastName44", "Patronymic44", null, 5 },
                    { 39L, "Address38", 3L, 38m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description38", "High", (byte)38, "FirstName38", true, "Job38", "LastName38", "Patronymic38", null, 4 },
                    { 33L, "Address32", 3L, 32m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description32", "High", (byte)32, "FirstName32", true, "Job32", "LastName32", "Patronymic32", null, 3 },
                    { 7L, "Address6", 1L, 6m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description6", "High", (byte)6, "FirstName6", true, "Job6", "LastName6", "Patronymic6", null, 2 },
                    { 13L, "Address12", 1L, 12m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description12", "High", (byte)12, "FirstName12", true, "Job12", "LastName12", "Patronymic12", null, 3 },
                    { 19L, "Address18", 1L, 18m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description18", "High", (byte)18, "FirstName18", true, "Job18", "LastName18", "Patronymic18", null, 4 },
                    { 25L, "Address24", 1L, 24m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description24", "High", (byte)24, "FirstName24", true, "Job24", "LastName24", "Patronymic24", null, 5 },
                    { 31L, "Address30", 1L, 30m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description30", "High", (byte)30, "FirstName30", true, "Job30", "LastName30", "Patronymic30", null, 1 },
                    { 37L, "Address36", 1L, 36m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description36", "High", (byte)36, "FirstName36", true, "Job36", "LastName36", "Patronymic36", null, 2 },
                    { 43L, "Address42", 1L, 42m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description42", "High", (byte)42, "FirstName42", true, "Job42", "LastName42", "Patronymic42", null, 3 },
                    { 49L, "Address48", 1L, 48m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description48", "High", (byte)48, "FirstName48", true, "Job48", "LastName48", "Patronymic48", null, 4 },
                    { 2L, "Address1", 2L, 1m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description1", "Medium", (byte)1, "FirstName1", true, "Job1", "LastName1", "Patronymic1", null, 2 },
                    { 8L, "Address7", 2L, 7m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description7", "Medium", (byte)7, "FirstName7", true, "Job7", "LastName7", "Patronymic7", null, 3 },
                    { 42L, "Address41", 6L, 41m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description41", "Medium", (byte)41, "FirstName41", true, "Job41", "LastName41", "Patronymic41", null, 2 },
                    { 14L, "Address13", 2L, 13m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description13", "Medium", (byte)13, "FirstName13", true, "Job13", "LastName13", "Patronymic13", null, 4 },
                    { 26L, "Address25", 2L, 25m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description25", "Medium", (byte)25, "FirstName25", true, "Job25", "LastName25", "Patronymic25", null, 1 },
                    { 32L, "Address31", 2L, 31m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description31", "Medium", (byte)31, "FirstName31", true, "Job31", "LastName31", "Patronymic31", null, 2 },
                    { 38L, "Address37", 2L, 37m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description37", "Medium", (byte)37, "FirstName37", true, "Job37", "LastName37", "Patronymic37", null, 3 },
                    { 44L, "Address43", 2L, 43m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description43", "Medium", (byte)43, "FirstName43", true, "Job43", "LastName43", "Patronymic43", null, 4 },
                    { 50L, "Address49", 2L, 49m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description49", "Medium", (byte)49, "FirstName49", true, "Job49", "LastName49", "Patronymic49", null, 5 },
                    { 3L, "Address2", 3L, 2m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description2", "High", (byte)2, "FirstName2", true, "Job2", "LastName2", "Patronymic2", null, 3 },
                    { 9L, "Address8", 3L, 8m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description8", "High", (byte)8, "FirstName8", true, "Job8", "LastName8", "Patronymic8", null, 4 },
                    { 15L, "Address14", 3L, 14m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description14", "High", (byte)14, "FirstName14", true, "Job14", "LastName14", "Patronymic14", null, 5 },
                    { 21L, "Address20", 3L, 20m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description20", "High", (byte)20, "FirstName20", true, "Job20", "LastName20", "Patronymic20", null, 1 },
                    { 27L, "Address26", 3L, 26m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description26", "High", (byte)26, "FirstName26", true, "Job26", "LastName26", "Patronymic26", null, 2 },
                    { 20L, "Address19", 2L, 19m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description19", "Medium", (byte)19, "FirstName19", true, "Job19", "LastName19", "Patronymic19", null, 5 },
                    { 48L, "Address47", 6L, 47m, new DateTime(2019, 4, 7, 14, 7, 4, 131, DateTimeKind.Local), "Description47", "Medium", (byte)47, "FirstName47", true, "Job47", "LastName47", "Patronymic47", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "TutorSubjects",
                columns: new[] { "TutorId", "SubjectId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 10L, 4L },
                    { 16L, 4L },
                    { 22L, 4L },
                    { 28L, 4L },
                    { 34L, 4L },
                    { 40L, 4L },
                    { 46L, 4L },
                    { 5L, 5L },
                    { 11L, 5L },
                    { 17L, 5L },
                    { 23L, 5L },
                    { 29L, 5L },
                    { 35L, 5L },
                    { 41L, 5L },
                    { 47L, 5L },
                    { 6L, 6L },
                    { 12L, 6L },
                    { 18L, 6L },
                    { 24L, 6L },
                    { 30L, 6L },
                    { 36L, 6L },
                    { 4L, 4L },
                    { 45L, 3L },
                    { 39L, 3L },
                    { 33L, 3L },
                    { 7L, 1L },
                    { 13L, 1L },
                    { 19L, 1L },
                    { 25L, 1L },
                    { 31L, 1L },
                    { 37L, 1L },
                    { 43L, 1L },
                    { 49L, 1L },
                    { 2L, 2L },
                    { 8L, 2L },
                    { 42L, 6L },
                    { 14L, 2L },
                    { 26L, 2L },
                    { 32L, 2L },
                    { 38L, 2L },
                    { 44L, 2L },
                    { 50L, 2L },
                    { 3L, 3L },
                    { 9L, 3L },
                    { 15L, 3L },
                    { 21L, 3L },
                    { 27L, 3L },
                    { 20L, 2L },
                    { 48L, 6L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TutorId",
                table: "Contacts",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypes_Name",
                table: "ContactTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_TutorId",
                table: "Phones",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                table: "Specializations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_CityId",
                table: "Tutors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSpecializations_SpecializationId",
                table: "TutorSpecializations",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjects_SubjectId",
                table: "TutorSubjects",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "TutorSpecializations");

            migrationBuilder.DropTable(
                name: "TutorSubjects");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
