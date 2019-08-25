using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFS.Core.Entity;
using ZFS.EFCore.Resources.ViewModel;

namespace ZFS.Api.Design
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //数据库实体转换实际引用实体, 如果出现不同, 手动设置转换
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.LastTime, opt => opt.MapFrom(src => src.LastLoginTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.LastLogouTime));
            CreateMap<UserViewModel, User>();
            CreateMap<UserAddViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
            CreateMap<User, UserUpdateViewModel>();


            CreateMap<Dictionaries, DictionariesViewModel>()
               .ForMember(dest => dest.LastTime, opt => opt.MapFrom(src => src.LastUpdate))
               .ForMember(dest => dest.LastTimeBy, opt => opt.MapFrom(src => src.LastUpdateBy))
              .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreationDate));
        }
    }
}
