using AutoMapper;

namespace Catalog.Api.Mappings
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToResponseMappingProfile());
                ps.AddProfile(new RequestToCommandMappingProfile());
            });
        }
    }
}
