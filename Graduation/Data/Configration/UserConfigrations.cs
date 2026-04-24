using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation.Data.Configration
{
    public class UserConfigrations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens).ToTable("RefreshTokens")
                .WithOwner().HasForeignKey("UserId");

            builder.Property(x => x.FName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.LName).HasMaxLength(30).IsRequired();
        }
    }
}
