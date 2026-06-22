using AutoMapper;
using Contactos.Application.Features.DTOs;
using Contactos.Application.Features.Posts.Commands.CreatePost;
using Contactos.Application.Features.Users.Commands.CreateUsersCommand;
using Contactos.Application.Features.Users.Commands.UpdateUsersCommand;
using Contactos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // queries

            //CreateMap<User, UserDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email != null ? src.Email.Direction : null))
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null));

            CreateMap<Phone, PhoneDto>();
            CreateMap<Email, EmailDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Geo, GeoDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<User, UserByidDto>();

            CreateMap<Post, PostDto>();

            // commands

            CreateMap<CreateUserCommand, User>()
                .ConstructUsing((a, b) => new User(
                    a.Name,
                    a.UserName,
                    b.Mapper.Map<Address>(a.Address),
                    a.Email != null ? new Email(a.Email) : null,
                    a.Phone != null ? new Phone(a.Phone) : null,
                    a.WebSite,
                    b.Mapper.Map<Company>(a.Company)
                    ));
            CreateMap<GeoDto, Geo>().ConstructUsing(q => new Geo(q.Lat!, q.Lng!));
            CreateMap<CompanyDto, Company>()
                .ConstructUsing(a => new Company(a.Name,a.CatchPhrase, a.Bs));
            CreateMap<AddressDto, Address>()
                .ConstructUsing((pe, pa) => new Address(pe.Street!, pe.Suite!, pe.City!, pe.ZipCode!, pa.Mapper.Map<Geo>(pe.Geo)));
            
            CreateMap<UpdateUserCommand, User>()
                .ConstructUsing((a, b) => new User(
                    a.Name,
                    a.UserName,
                    b.Mapper.Map<Address>(a.Address),
                    b.Mapper.Map<Email>(a.Email),
                    b.Mapper.Map<Phone>(a.Phone),
                    a.WebSite,
                    b.Mapper.Map<Company>(a.Company)
                    ));

            CreateMap<CreatePostCommand, Post>();
        }

        
    }
}
