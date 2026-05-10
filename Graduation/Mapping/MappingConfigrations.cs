

using Graduation.Contracts.Bundle;

namespace Graduation.Mapping
{
    public class MappingConfigrations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<BundleRequest, Bundle>()
            //     .Map(dest => dest.Items, src => src.Items.Select(item =>
            //     new BundleItem { PlantId = item.PlantId , Quantity = item.Quantity }));
        }
    }
}
