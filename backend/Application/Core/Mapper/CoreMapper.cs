using AutoMapper;
using Domain.Files.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Core.Mapper;

public class CoreMapper : Profile
{
    public CoreMapper()
    {
        CreateMap<IFormFile, FileEntry>()
            .ForMember(x => x.Size, opt => opt
                .MapFrom(s => s.Length))
            .ForMember(x => x.Extension, opt => opt
                .MapFrom(s => Path.GetExtension(s.FileName)))
            .ForMember(x => x.Name, opt => opt
                .MapFrom(s => s.FileName));
    }
}