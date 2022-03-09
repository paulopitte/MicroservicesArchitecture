using AutoMapper;

namespace Catalog.Api.Application.Mappings
{
    using Request = Core.Contracts.Requests;

    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<Request.Product, Domain.Product>().ReverseMap();
        }
    }
}
