using AutoMapper;
using api_rest_cs.DTOs;
using api_rest_cs.Models;

namespace api_rest_cs.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();

        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.Books,
                opt => opt.MapFrom(src => src.Books)
            );
    }

}
