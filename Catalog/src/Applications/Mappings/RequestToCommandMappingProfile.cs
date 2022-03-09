using AutoMapper;
using Request = Core.Contracts.Requests;

namespace Catalog.Api.Application.Mappings
{
 
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<Request.Product, Domain.Product>().ReverseMap();
        }
    }
}
