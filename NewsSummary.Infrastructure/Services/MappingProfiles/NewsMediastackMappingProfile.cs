using AutoMapper;
using NewsSummary.Core.Models.News.Mediastack;
using NewsSummary.Core.Models.News;

namespace NewsSummary.Infrastructure.Services.MappingProfiles;

public class NewsMediastackMappingProfile : Profile
{
    public NewsMediastackMappingProfile()
    {

        CreateMap<NewsRequestDTO, NewsResponseDTO>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Data.FirstOrDefault().Country ?? string.Empty))
            .ForMember(dest => dest.News, opt => opt.MapFrom(src => src.Data)); 

        CreateMap<Article, CommonNewsEntry>()
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublishedAt.ToString("yyyy-MM-dd")));
    }
}