﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToPlanName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Plans_Name",
                table: "Plans",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_Name",
                table: "Plans");
        }
    }
}
