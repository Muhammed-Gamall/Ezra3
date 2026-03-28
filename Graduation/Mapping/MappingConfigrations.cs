

namespace Graduation.Mapping
{
    public class MappingConfigrations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<PlantRequest, Plant>()
            //     .Map(dest => dest.Images, src => src.Image.Select(p => new PlantPhoto { Photo = p }));
        }
    }
}
