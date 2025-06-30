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
            CreateMap<UserUpdateRequestDTO, UserAddServiceDTO>()
                .ForMember(u => u.Password, opt => opt.Ignore());

            CreateMap<User, UserResponseDTO>()
                .ForMember(u => u.CreatedByUserName, act => act.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Name : ""))
                .ForMember(u => u.LastUpdatedByUserName, act => act.MapFrom(src => src.LastUpdatedByUser != null ? src.LastUpdatedByUser.Name : ""))
                .ForMember(u => u.CreatedByUserEmail, act => act.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Email : ""))
                .ForMember(u => u.LastUpdatedByUserEmail, act => act.MapFrom(src => src.LastUpdatedByUser != null ? src.LastUpdatedByUser.Email : ""))
                .ForMember(u => u.TeamName, act => act.MapFrom(src => src.Team != null ? src.Team.Name : "")); 

            CreateMap<UserAddServiceDTO, User>()
                .ForMember(u => u.Password, opt => opt.Ignore())
                .ForMember(u => u.CreatedByUserId, act => act.MapFrom(src => src.LastUpdatedByUserId))
                .ForMember(u => u.LastUpdatedByUserId, act => act.MapFrom(src => src.LastUpdatedByUserId))
                .ForMember(u => u.CreatedAt, act => act.MapFrom(src => DateTime.UtcNow))
                .ForMember(u => u.LastUpdatedAt, act => act.MapFrom(src => DateTime.UtcNow))
                .ForMember(u => u.Id, act => act.MapFrom(src => Guid.NewGuid()));

            CreateMap<Document, DocumentReponseDTO>()
                .ForMember(d => d.CreatedByUserName, act => act.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Name : ""))
                .ForMember(d => d.LastUpdatedByUserName, act => act.MapFrom(src => src.LastUpdatedByUser != null ? src.LastUpdatedByUser.Name : ""))
                .ForMember(d => d.CreatedByUserEmail, act => act.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Email : ""))
                .ForMember(d => d.LastUpdatedByUserEmail, act => act.MapFrom(src => src.LastUpdatedByUser != null ? src.LastUpdatedByUser.Email : ""))
                .ForMember(d => d.TeamName, act => act.MapFrom(src => src.Team != null ? src.Team.Name : ""));

        }
    }
}