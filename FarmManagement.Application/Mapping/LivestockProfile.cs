using AutoMapper;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.Mapping;

public class LivestockProfile : Profile
{
    public LivestockProfile()
    {
        // Entity -> DTO
        CreateMap<Livestock, LivestockDto>()
        .ForMember(dest => dest.LivestockTypeName, opt => opt.MapFrom(src => src.LivestockType != null ? src.LivestockType.Name : null))
        .ForMember(dest => dest.LivestockStatusName, opt => opt.MapFrom(src => src.LivestockStatus != null ? src.LivestockStatus.Name : null))
        .ForMember(dest => dest.LocationName, opt => opt.Ignore()); // Location navigation not present on entity

        // Create DTO -> Entity
        CreateMap<CreateLivestockDto, Livestock>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockType, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockStatus, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockCareLogs, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockHealthLogs, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockSales, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
        .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
        .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Update DTO -> Entity
        CreateMap<UpdateLivestockDto, Livestock>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockType, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockStatus, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockCareLogs, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockHealthLogs, opt => opt.Ignore())
        .ForMember(dest => dest.LivestockSales, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
        .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
        .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
