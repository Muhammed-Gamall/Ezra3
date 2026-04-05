
namespace Graduation.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor accessor) 
        : IdentityDbContext(options)
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        public DbSet<FarmerProfile> FarmerProfiles { get; set; } 
        public DbSet<FarmerRating> FarmerRatings { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 
        public DbSet<Plant> Plants { get; set; } 
        public DbSet<PlantPhoto> PlantPhotos { get; set; } 
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var CascadeFKs = modelBuilder.Model.GetEntityTypes()
                 .SelectMany(e => e.GetForeignKeys())
                 .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

            foreach (var fk in CascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entries)
            {
                var userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedById).CurrentValue = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedById).CurrentValue = userId;
                    entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


    }

}
