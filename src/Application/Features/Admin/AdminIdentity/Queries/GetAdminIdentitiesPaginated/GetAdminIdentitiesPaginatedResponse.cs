﻿using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

public record GetAdminIdentitiesPaginatedResponse : IMapFrom<ApplicationUser>
{
    public string? Id { get; set; }

    public string? NombreDeUsuario { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<ApplicationUser, GetAdminIdentitiesPaginatedResponse>()
            .ForMember(
                dest => dest.NombreDeUsuario,
                opt => opt.MapFrom(src => $"{src.UserName}, {src.UserName}"));
    }
}