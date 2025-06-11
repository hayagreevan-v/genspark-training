using AutoMapper;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;

namespace DocumentSharingSystem.Misc
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserAddRequestDTO, UserAddServiceDTO>();
            CreateMap<UserUpdateRequestDTO, UserAddServiceDTO>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserAddServiceDTO, User>()
                .ForMember(u => u.Password, opt => opt.Ignore())
                .ForMember(u => u.LastUpdatedByUserId, act => act.MapFrom(src => src.CreatedByUserId))
                .ForMember(u => u.CreatedAt, act => act.MapFrom(src => DateTime.UtcNow))
                .ForMember(u => u.LastUpdatedAt, act => act.MapFrom(src => DateTime.UtcNow))
                .ForMember(u => u.Id, act => act.MapFrom(src => Guid.NewGuid()));

            CreateMap<Document, DocumentReponseDTO>();
        }
    }
}