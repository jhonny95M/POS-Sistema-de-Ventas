using AutoMapper;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Mappers
{
    public sealed class CategoryMappingsProfile:Profile
    {
        public CategoryMappingsProfile()
        {
            CreateMap<Category,CategoryResponseDto>()
                .ForMember(c=>c.StateCategory,c=>c.MapFrom(y=>y.State.Equals(StateTypes.Activo)?"Activo":"Inactivo"))
                .ReverseMap();
            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category,CategorySelectResponseDto>()
                .ReverseMap();
        }
    }
}
