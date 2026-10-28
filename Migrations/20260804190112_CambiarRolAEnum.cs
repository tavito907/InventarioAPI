using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiarRolAEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "Usuarios"
                SET "Rol" =
                    CASE
                        WHEN "Rol" = 'Administrador' THEN '0'
                        WHEN "Rol" = 'Empleado' THEN '1'
                    END;
             """);

            migrationBuilder.Sql("""
                ALTER TABLE "Usuarios"
                ALTER COLUMN "Rol" TYPE integer
                USING "Rol"::integer;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Usuarios"
                ALTER COLUMN "Rol" TYPE text
                USING CASE
                    WHEN "Rol" = 0 THEN 'Adminstrador'
                    WHEN "Rol" = 1 THEN 'Empleado'
                END;
             """);
        }
    }
}
