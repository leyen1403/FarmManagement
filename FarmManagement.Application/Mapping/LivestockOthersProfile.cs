using AutoMapper;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.Mapping;

public class LivestockOthersProfile : Profile
{
    public LivestockOthersProfile()
    {
        CreateMap<LivestockType, LivestockTypeDto>();
        CreateMap<CreateLivestockTypeDto, LivestockType>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestocks, o => o.Ignore());
        CreateMap<UpdateLivestockTypeDto, LivestockType>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestocks, o => o.Ignore());

        CreateMap<LivestockStatus, LivestockStatusDto>();
        CreateMap<CreateLivestockStatusDto, LivestockStatus>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestocks, o => o.Ignore());
        CreateMap<UpdateLivestockStatusDto, LivestockStatus>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestocks, o => o.Ignore());

        CreateMap<SaleType, SaleTypeDto>();
        CreateMap<LivestockSale, LivestockSaleDto>()
        .ForMember(d => d.SaleTypeName, o => o.MapFrom(s => s.SaleType != null ? s.SaleType.Name : null));
        CreateMap<CreateLivestockSaleDto, LivestockSale>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestock, o => o.Ignore())
        .ForMember(d => d.SaleType, o => o.Ignore());

        CreateMap<LivestockCareType, LivestockCareTypeDto>();
        CreateMap<LivestockCareLog, LivestockCareLogDto>()
        .ForMember(d => d.LivestockCareTypeName, o => o.MapFrom(s => s.LivestockCareType != null ? s.LivestockCareType.Name : null));
        CreateMap<CreateLivestockCareLogDto, LivestockCareLog>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestock, o => o.Ignore())
        .ForMember(d => d.LivestockCareType, o => o.Ignore());

        CreateMap<LivestockHealthStatus, LivestockHealthStatusDto>();
        CreateMap<LivestockHealthLog, LivestockHealthLogDto>()
        .ForMember(d => d.HealthStatusName, o => o.MapFrom(s => s.HealthStatus != null ? s.HealthStatus.Name : null));
        CreateMap<CreateLivestockHealthLogDto, LivestockHealthLog>()
        .ForMember(d => d.Id, o => o.Ignore())
        .ForMember(d => d.Livestock, o => o.Ignore())
        .ForMember(d => d.HealthStatus, o => o.Ignore());
    }
}
