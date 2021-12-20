using Microsoft.EntityFrameworkCore.Migrations;

namespace MOTILab3.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnemyWeapon",
                columns: table => new
                {
                    WeaponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeaponPhysicalDamage = table.Column<int>(type: "int", nullable: false),
                    WeaponMagicalDamage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyWeapon", x => x.WeaponId);
                });

            migrationBuilder.CreateTable(
                name: "PlayersArmour",
                columns: table => new
                {
                    ArmourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArmourPhysicalResistance = table.Column<int>(type: "int", nullable: false),
                    ArmourMagicalResistance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersArmour", x => x.ArmourId);
                });

            migrationBuilder.CreateTable(
                name: "ArmourResistanceResult",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmourId = table.Column<int>(type: "int", nullable: false),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    ResistedDamage = table.Column<int>(type: "int", nullable: false),
                    PlayersArmourArmourId = table.Column<int>(type: "int", nullable: true),
                    EnemyWeaponWeaponId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmourResistanceResult", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_ArmourResistanceResult_EnemyWeapon_EnemyWeaponWeaponId",
                        column: x => x.EnemyWeaponWeaponId,
                        principalTable: "EnemyWeapon",
                        principalColumn: "WeaponId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArmourResistanceResult_PlayersArmour_PlayersArmourArmourId",
                        column: x => x.PlayersArmourArmourId,
                        principalTable: "PlayersArmour",
                        principalColumn: "ArmourId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArmourResistanceResult_EnemyWeaponWeaponId",
                table: "ArmourResistanceResult",
                column: "EnemyWeaponWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_ArmourResistanceResult_PlayersArmourArmourId",
                table: "ArmourResistanceResult",
                column: "PlayersArmourArmourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmourResistanceResult");

            migrationBuilder.DropTable(
                name: "EnemyWeapon");

            migrationBuilder.DropTable(
                name: "PlayersArmour");
        }
    }
}
