using AutoMapper;
using Domain.Models;
using LibApp.WebApp.ViewModels;

namespace LibApp.WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => MapAuthors(src.Authors)))
                .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(author => author.Id)))
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name));

            CreateMap<BookViewModel, Book>();
        }

        private IEnumerable<(int AuthorId, string AuthorName)> MapAuthors(IEnumerable<Author> authors)
        {
            return authors.Select(author => (AuthorId: author.Id, AuthorName: author.Name));
        }
    }
}