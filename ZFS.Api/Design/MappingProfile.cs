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

            CreateMap<Group, GroupViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.GroupCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GroupName)); ;
            CreateMap<GroupViewModel, Group>();


            CreateMap<Menu, MenuViewModel>()
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.MenuCode))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MenuName))
               .ForMember(dest => dest.Authorities, opt => opt.MapFrom(src => src.MenuAuthorities))
               .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.MenuCaption))
            .ForMember(dest => dest.NameSpace, opt => opt.MapFrom(src => src.MenuNameSpace));
            CreateMap<MenuViewModel, Menu>();

        }
    }
}
