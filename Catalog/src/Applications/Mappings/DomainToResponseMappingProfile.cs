namespace Catalog.Api.Application.Mappings
{
    using Request = Core.Contracts.Requests;
    using AutoMapper;

    public class DomainToResponseMappingProfile : Profile
    {

        public DomainToResponseMappingProfile()
        {
            CreateMap<Domain.Product, Request.Product>().ReverseMap();

        }
    }
}
