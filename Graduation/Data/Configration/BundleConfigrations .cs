namespace Graduation.Data.Configration
{
    public class BundleConfigrations : IEntityTypeConfiguration<Bundle>
    {
        public void Configure(EntityTypeBuilder<Bundle> builder)
        {
                builder.HasMany(f => f.Items)
                    .WithOne(r => r.Bundle)
                    .HasForeignKey(r => r.BundleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
