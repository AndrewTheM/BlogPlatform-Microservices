using AutoMapper;
using BlogPlatform.Accounts.Application.Common.DTO;
using BlogPlatform.Accounts.Application.Features.Accounts.Commands.EditAccount;
using BlogPlatform.Accounts.Domain.Entities;
using BlogPlatform.Accounts.Domain.ValueObjects;

namespace BlogPlatform.Accounts.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Name.MiddleName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Location.State))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Location.Country));

        CreateMap<EditAccountCommand, Account>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.FirstName, src.MiddleName, src.LastName)))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new Location(src.Country, src.State, src.City)));
    }
}
