using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Project.MVC.Data.Migrations
{
    /// <inheritdoc />
    /// <inheritdoc />
    public partial class AdminCredentials : Migration
    {
        const string ADMIN_USER_GUID = "45d7339f-df1b-43a3-9224-ed3c920d0f93";
        const string ADMIN_ROLE_GUID = "4cfcd0ff-d359-41bd-a62e-feee508499b9";
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var passwordhash = hasher.HashPassword(null, "Admin123!");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO AspNetUsers(" +
                "Id, " +
                "UserName, " +
                "NormalizedUserName, " +
                "Email, " +
                "NormalizedEmail, " +
                "EmailConfirmed, " +
                "PasswordHash, " +
                "SecurityStamp, " +
                "PhoneNumber, " +
                "PhoneNumberConfirmed, " +
                "TwoFactorEnabled, " +
                "LockoutEnabled, " +
                "AccessFailedCount) " +
                "VALUES (" +
                $"'{ADMIN_USER_GUID}'," +
                "'admin@admin.com'," +
                "'ADMIN@ADMIN.COM'," +
                "'admin@admin.com'," +
                "'ADMIN@ADMIN.COM'," +
                "0," +
                $"'{passwordhash}'," +
                "'Admin'," +
                "'+385991234567'," +
                "1," +
                "0," +
                "0," +
                "0" +
                ");");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}', '{ADMIN_ROLE_GUID}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId='{ADMIN_USER_GUID}' AND RoleId='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id='{ADMIN_USER_GUID}'");
        }
    }
}
