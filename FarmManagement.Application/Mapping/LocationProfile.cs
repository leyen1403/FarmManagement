// ***********************************************************************
// File: LocationProfile.cs
// Description: Provides mapping configurations for Location, LocationType, and LocationStatus entities.
// ***********************************************************************

using AutoMapper;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Domain.Entities.Locations;

namespace FarmManagement.Application.Mapping;

/// <summary>
/// Defines mapping profiles for Location, LocationType, and LocationStatus entities.
/// </summary>
public class LocationProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationProfile"/> class.
    /// Configures mappings between domain entities and DTOs.
    /// </summary>
    public LocationProfile()
    {
        // ------------------------
        // Location
        // ------------------------
        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.LocationTypeName, opt => opt.MapFrom(src => src.LocationType != null ? src.LocationType.Name : string.Empty))
            .ForMember(dest => dest.LocationStatusName, opt => opt.MapFrom(src => src.LocationStatus != null ? src.LocationStatus.Name : string.Empty));

        CreateMap<LocationDto, Location>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ForMember(x => x.DeletedDate, opt => opt.Ignore())
            .ForMember(x => x.IsDeleted, opt => opt.Ignore());

        // ------------------------
        // LocationType
        // ------------------------
        CreateMap<LocationType, LocationTypeDto>();

        CreateMap<LocationTypeDto, LocationType>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ForMember(x => x.DeletedDate, opt => opt.Ignore())
            .ForMember(x => x.IsDeleted, opt => opt.Ignore());

        // ------------------------
        // LocationStatus
        // ------------------------
        CreateMap<LocationStatus, LocationStatusDto>();

        CreateMap<LocationStatusDto, LocationStatus>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ForMember(x => x.DeletedDate, opt => opt.Ignore())
            .ForMember(x => x.IsDeleted, opt => opt.Ignore());
    }
}
