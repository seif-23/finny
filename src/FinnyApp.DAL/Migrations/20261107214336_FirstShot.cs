using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinnyApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FirstShot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentTable",
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
                    table.PrimaryKey("PK_ParentTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChildTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Finnies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChildTable_ParentTable_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskAmountToFinish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GoalTable_ParentTable_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RewardTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinniesCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RewardTable_ParentTable_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskTable_ParentTable_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildGoalProgressTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildID = table.Column<int>(type: "int", nullable: false),
                    GoalID = table.Column<int>(type: "int", nullable: false),
                    TasksProgress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildGoalProgressTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChildGoalProgressTable_ChildTable_ChildID",
                        column: x => x.ChildID,
                        principalTable: "ChildTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildGoalProgressTable_GoalTable_GoalID",
                        column: x => x.GoalID,
                        principalTable: "GoalTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChildRewardSelectionTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildID = table.Column<int>(type: "int", nullable: false),
                    RewardID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildRewardSelectionTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChildRewardSelectionTable_ChildTable_ChildID",
                        column: x => x.ChildID,
                        principalTable: "ChildTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildRewardSelectionTable_RewardTable_RewardID",
                        column: x => x.RewardID,
                        principalTable: "RewardTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChildTaskProgressTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    ChildID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildTaskProgressTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChildTaskProgressTable_ChildTable_ChildID",
                        column: x => x.ChildID,
                        principalTable: "ChildTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildTaskProgressTable_TaskTable_TaskID",
                        column: x => x.TaskID,
                        principalTable: "TaskTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildGoalProgressTable_ChildID",
                table: "ChildGoalProgressTable",
                column: "ChildID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildGoalProgressTable_GoalID",
                table: "ChildGoalProgressTable",
                column: "GoalID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildRewardSelectionTable_ChildID",
                table: "ChildRewardSelectionTable",
                column: "ChildID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildRewardSelectionTable_RewardID",
                table: "ChildRewardSelectionTable",
                column: "RewardID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildTable_ParentID",
                table: "ChildTable",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildTaskProgressTable_ChildID",
                table: "ChildTaskProgressTable",
                column: "ChildID");

            migrationBuilder.CreateIndex(
                name: "IX_ChildTaskProgressTable_TaskID",
                table: "ChildTaskProgressTable",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_GoalTable_ParentID",
                table: "GoalTable",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RewardTable_ParentID",
                table: "RewardTable",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTable_ParentID",
                table: "TaskTable",
                column: "ParentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildGoalProgressTable");

            migrationBuilder.DropTable(
                name: "ChildRewardSelectionTable");

            migrationBuilder.DropTable(
                name: "ChildTaskProgressTable");

            migrationBuilder.DropTable(
                name: "GoalTable");

            migrationBuilder.DropTable(
                name: "RewardTable");

            migrationBuilder.DropTable(
                name: "ChildTable");

            migrationBuilder.DropTable(
                name: "TaskTable");

            migrationBuilder.DropTable(
                name: "ParentTable");
        }
    }
}
