using AutoMapper;
using PublicForum2.Application.ViewModels;
using PublicForum2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TopicViewModel, Topic>();
            CreateMap<UserViewModel, User>();
        }
    }
}
