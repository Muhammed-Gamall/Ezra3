namespace Graduation.Data.Configration
{
    public class FarmerConfigrations : IEntityTypeConfiguration<FarmerProfile>
    {
        public void Configure(EntityTypeBuilder<FarmerProfile> builder)
        {
            builder.HasKey(k => k.UserId);


             builder.HasOne(f => f.User)
                .WithOne(u => u.Farmer)
                .HasForeignKey<FarmerProfile>(f => f.UserId);


                builder.HasMany(f => f.Ratings)
                    .WithOne(r => r.Farmer)
                    .HasForeignKey(r => r.FarmerId);
        }
    }
}
