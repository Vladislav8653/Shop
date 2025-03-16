using AutoMapper;
using UserManagement.Application.DTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRequestDto, User>();
    }
}
