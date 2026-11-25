using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinnyApp.DAL.Database.Migrations
{
    /// <inheritdoc />
    public partial class FirstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GoalTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskAmountToFinish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GoalTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RewardTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinniesCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RewardTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubUserTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Finnies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubUserTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubUserTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suGoalProgressTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubUserID = table.Column<int>(type: "int", nullable: false),
                    GoalID = table.Column<int>(type: "int", nullable: false),
                    TasksProgress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suGoalProgressTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_suGoalProgressTable_GoalTable_GoalID",
                        column: x => x.GoalID,
                        principalTable: "GoalTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suGoalProgressTable_SubUserTable_SubUserID",
                        column: x => x.SubUserID,
                        principalTable: "SubUserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suRewardSelectionTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubUserID = table.Column<int>(type: "int", nullable: false),
                    RewardID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suRewardSelectionTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_suRewardSelectionTable_RewardTable_RewardID",
                        column: x => x.RewardID,
                        principalTable: "RewardTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suRewardSelectionTable_SubUserTable_SubUserID",
                        column: x => x.SubUserID,
                        principalTable: "SubUserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suTaskProgressTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    SubUserID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suTaskProgressTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_suTaskProgressTable_SubUserTable_SubUserID",
                        column: x => x.SubUserID,
                        principalTable: "SubUserTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suTaskProgressTable_TaskTable_TaskID",
                        column: x => x.TaskID,
                        principalTable: "TaskTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalTable_UserID",
                table: "GoalTable",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RewardTable_UserID",
                table: "RewardTable",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubUserTable_UserID",
                table: "SubUserTable",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_suGoalProgressTable_GoalID",
                table: "suGoalProgressTable",
                column: "GoalID");

            migrationBuilder.CreateIndex(
                name: "IX_suGoalProgressTable_SubUserID",
                table: "suGoalProgressTable",
                column: "SubUserID");

            migrationBuilder.CreateIndex(
                name: "IX_suRewardSelectionTable_RewardID",
                table: "suRewardSelectionTable",
                column: "RewardID");

            migrationBuilder.CreateIndex(
                name: "IX_suRewardSelectionTable_SubUserID",
                table: "suRewardSelectionTable",
                column: "SubUserID");

            migrationBuilder.CreateIndex(
                name: "IX_suTaskProgressTable_SubUserID",
                table: "suTaskProgressTable",
                column: "SubUserID");

            migrationBuilder.CreateIndex(
                name: "IX_suTaskProgressTable_TaskID",
                table: "suTaskProgressTable",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTable_UserID",
                table: "TaskTable",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suGoalProgressTable");

            migrationBuilder.DropTable(
                name: "suRewardSelectionTable");

            migrationBuilder.DropTable(
                name: "suTaskProgressTable");

            migrationBuilder.DropTable(
                name: "GoalTable");

            migrationBuilder.DropTable(
                name: "RewardTable");

            migrationBuilder.DropTable(
                name: "SubUserTable");

            migrationBuilder.DropTable(
                name: "TaskTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
