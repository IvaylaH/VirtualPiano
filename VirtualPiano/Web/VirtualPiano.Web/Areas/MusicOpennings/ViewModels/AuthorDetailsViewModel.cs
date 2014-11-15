namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AuthorDetailsViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<AuthorsAdViewModel> Ads { get; set; }

         public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage
        {
            get
            {
                if (this.CurrentPage >= this.PagesCount)
                {
                    return 1;
                }

                return this.CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.CurrentPage <= 1)
                {
                    return this.PagesCount;
                }

                return this.CurrentPage - 1;
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, AuthorDetailsViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(author => author.FirstName + " " + author.LastName))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(author => author.CreatedOn))
                .ForMember(dest => dest.Ads, opt => opt.MapFrom(author => author.Ads.AsQueryable()
                    .OrderBy(ad => ad.Title)
                    .Select(ad => new AuthorsAdViewModel()
                    {
                        Id = ad.Id,
                        Title = ad.Title
                    })));
        }
    }
}