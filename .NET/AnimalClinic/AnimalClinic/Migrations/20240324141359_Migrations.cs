using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinic.Migrations;

/// <inheritdoc />
public partial class Migrations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Client",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Client", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Animal",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Kind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClientId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Animal", x => x.Id);
                table.ForeignKey(
                    name: "FK_Animal_Client_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Client",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Visit",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Office = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Date = table.Column<DateOnly>(type: "date", nullable: true),
                Time = table.Column<TimeOnly>(type: "time", nullable: true),
                ClientId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Visit", x => x.Id);
                table.ForeignKey(
                    name: "FK_Visit_Client_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Client",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Animal_ClientId",
            table: "Animal",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Visit_ClientId",
            table: "Visit",
            column: "ClientId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Animal");

        migrationBuilder.DropTable(
            name: "Visit");

        migrationBuilder.DropTable(
            name: "Client");
    }
}
