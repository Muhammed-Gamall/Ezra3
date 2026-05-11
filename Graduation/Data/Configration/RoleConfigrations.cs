using Graduation.Consts;

namespace Graduation.Data.Configration
{
    public class RoleConfigrations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            builder.HasData([new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
               Name = DefaultRoles.Admin,
               NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminConcurrencyStamp
            },
                
            new ApplicationRole
            {
                Id = DefaultRoles.FarmerRoleId,
               Name = DefaultRoles.Farmer,
               NormalizedName = DefaultRoles.Farmer.ToUpper(),
                ConcurrencyStamp = DefaultRoles.FarmerConcurrencyStamp
            },

            new ApplicationRole{
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberConcurrencyStamp,
                IsDefault = true
            }]);
        }
    }
}
