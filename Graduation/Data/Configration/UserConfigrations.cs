using Graduation.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation.Data.Configration
{
    public class UserConfigrations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens).ToTable("RefreshTokens")
                .WithOwner().HasForeignKey("UserId");

            builder.Property(x => x.FullName).HasMaxLength(30).IsRequired();


            builder.HasData(new ApplicationUser
            {
                Id = DefaultUsers.AdminId,
                FullName = "Admin User",
                UserName = DefaultUsers.AdminEmail,
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAECT5qqExszEq4/R10c5+9427qkSJA5NH2Ei8cAyBmngHLCSAFaMdsDrjM8AXTXvrYg==",
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp
            });
        }
    }
}
