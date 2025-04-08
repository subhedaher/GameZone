using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Categories (Name) VALUES
            ('Sports'),
            ('Actions'),
            ('Adventure'),
            ('Racing'),
            ('Fighting'),
            ('Film')");

            migrationBuilder.Sql(@"
            INSERT INTO Devices (Name , Icon) VALUES
            ('PlayStation' , 'bi bi-playstation'),
            ('xbox' , 'bi bi-xbox'),
            ('Nintendo Switch' , 'bi bi-nintendo-switch'),
            ('PC' , 'bi bi-pc-display')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DELETE FROM Categories 
            WHERE Name IN ('Sports', 'Actions', 'Adventure', 'Racing', 'Fighting', 'Film')");

            migrationBuilder.Sql(@"
            DELETE FROM Devices 
            WHERE Name IN ('PlayStation', 'xbox', 'Nintendo Switch', 'PC')");
        }
    }
}
