using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHelper.UserService.Data.Provider.MsSql.Ef.Migrations;

  /// <inheritdoc />
  public partial class Initial : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {


          migrationBuilder.CreateTable(
              name: "Users",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  IsActive = table.Column<bool>(type: "bit", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                  PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                      .Annotation("SqlServer:IsTemporal", true)
                      .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
                      .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                      .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                      .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Users", x => x.Id);
              })
              .Annotation("SqlServer:IsTemporal", true)
              .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
              .Annotation("SqlServer:TemporalHistoryTableSchema", null)
              .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
              .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

          migrationBuilder.CreateTable(
              name: "PendingUsers",
              columns: table => new
              {
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CommunicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PendingUsers", x => x.UserId);
                  table.ForeignKey(
                      name: "FK_PendingUsers_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UsersAdditions",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UsersAdditions", x => x.Id);
                  
                  table.ForeignKey(
                      name: "FK_UsersAdditions_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UsersAvatars",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  IsCurrentAvatar = table.Column<bool>(type: "bit", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UsersAvatars", x => x.Id);
                  table.ForeignKey(
                      name: "FK_UsersAvatars_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UsersCommunications",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Type = table.Column<int>(type: "int", nullable: false),
                  Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                  ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                  ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UsersCommunications", x => x.Id);
                  table.ForeignKey(
                      name: "FK_UsersCommunications_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UsersCredentials",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                  ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                  ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UsersCredentials", x => x.Id);
                  table.ForeignKey(
                      name: "FK_UsersCredentials_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });



          migrationBuilder.CreateIndex(
              name: "IX_UsersAdditions_UserId",
              table: "UsersAdditions",
              column: "UserId",
              unique: true);

          migrationBuilder.CreateIndex(
              name: "IX_UsersAvatars_UserId",
              table: "UsersAvatars",
              column: "UserId");

          migrationBuilder.CreateIndex(
              name: "IX_UsersCommunications_UserId",
              table: "UsersCommunications",
              column: "UserId");

          migrationBuilder.CreateIndex(
              name: "IX_UsersCredentials_UserId",
              table: "UsersCredentials",
              column: "UserId",
              unique: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "PendingUsers");

          migrationBuilder.DropTable(
              name: "UsersAdditions");

          migrationBuilder.DropTable(
              name: "UsersAvatars");

          migrationBuilder.DropTable(
              name: "UsersCommunications");

          migrationBuilder.DropTable(
              name: "UsersCredentials");


          migrationBuilder.DropTable(
              name: "Users")
              .Annotation("SqlServer:IsTemporal", true)
              .Annotation("SqlServer:TemporalHistoryTableName", "UsersHistory")
              .Annotation("SqlServer:TemporalHistoryTableSchema", null)
              .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
              .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
      }
  }
