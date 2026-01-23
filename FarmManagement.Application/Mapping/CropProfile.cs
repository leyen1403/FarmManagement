using AutoMapper;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Domain.Entities.Crops;

namespace FarmManagement.Application.Mapping
{
    public class CropProfile : Profile
    {
        public CropProfile()
        {
            // CropType mappings
            CreateMap<CropType, CropTypeDto>();

            CreateMap<CreateCropTypeDto, CropType>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crops, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropTypeDto, CropType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.Ignore()) // Code không được update
                .ForMember(dest => dest.Crops, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CropStatus mappings
            CreateMap<CropStatus, CropStatusDto>();

            CreateMap<CreateCropStatusDto, CropStatus>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crops, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropStatusDto, CropStatus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crops, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // Crop mappings
            CreateMap<Crop, CropDto>()
                .ForMember(dest => dest.CropTypeName, opt => opt.MapFrom(src => src.CropType != null ? src.CropType.Name : null))
                .ForMember(dest => dest.CropStatusName, opt => opt.MapFrom(src => src.CropStatus != null ? src.CropStatus.Name : null))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null))
                .ForMember(dest => dest.CurrentPrice, opt => opt.Ignore());

            CreateMap<CreateCropDto, Crop>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ActualHarvestDate, opt => opt.Ignore())
                .ForMember(dest => dest.CropType, opt => opt.Ignore())
                .ForMember(dest => dest.CropStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareLogs, opt => opt.Ignore())
                .ForMember(dest => dest.CropHarvests, opt => opt.Ignore())
                .ForMember(dest => dest.CropCosts, opt => opt.Ignore())
                .ForMember(dest => dest.CropPrices, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropDto, Crop>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CropType, opt => opt.Ignore())
                .ForMember(dest => dest.CropStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareLogs, opt => opt.Ignore())
                .ForMember(dest => dest.CropHarvests, opt => opt.Ignore())
                .ForMember(dest => dest.CropCosts, opt => opt.Ignore())
                .ForMember(dest => dest.CropPrices, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CropPrice mappings
            CreateMap<CropPrice, CropPriceDto>()
                .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.Crop != null ? src.Crop.Name : null));

            CreateMap<CreateCropPriceDto, CropPrice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropPriceDto, CropPrice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CropHarvest mappings
            CreateMap<CropHarvest, CropHarvestDto>()
                .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.Crop != null ? src.Crop.Name : null));

            CreateMap<CreateCropHarvestDto, CropHarvest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropHarvestDto, CropHarvest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CropCost mappings
            CreateMap<CropCost, CropCostDto>()
                .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.Crop != null ? src.Crop.Name : null))
                .ForMember(dest => dest.CostTypeName, opt => opt.MapFrom(src => src.CostType != null ? src.CostType.Name : null));

            CreateMap<CreateCropCostDto, CropCost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CostType, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropCostDto, CropCost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CostType, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CropCareLog mappings
            CreateMap<CropCareLog, CropCareLogDto>()
                .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.Crop != null ? src.Crop.Name : null))
                .ForMember(dest => dest.CropCareTypeName, opt => opt.MapFrom(src => src.CropCareType != null ? src.CropCareType.Name : null));

            CreateMap<CreateCropCareLogDto, CropCareLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareType, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareLogs, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCropCareLogDto, CropCareLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Crop, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareType, opt => opt.Ignore())
                .ForMember(dest => dest.CropCareLogs, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // CostType mappings
            CreateMap<CostType, CostTypeDto>();

            // CropCareType mappings
            CreateMap<CropCareType, CropCareTypeDto>();
        }
    }
}
