using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksService.Migrations
{
    /// <inheritdoc />
    public partial class FixInitialTimeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InitialTime",
                table: "Tasks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialTime",
                table: "Tasks");
        }
    }
}
