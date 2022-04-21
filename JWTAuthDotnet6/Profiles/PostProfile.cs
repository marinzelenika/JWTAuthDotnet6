using AutoMapper;
using JWTAuthDotnet6.DTOs;
using JWTAuthDotnet6.Models;

namespace JWTAuthDotnet6.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, GetPostDto>();
            CreateMap<AddPostDto, Post>();
        }
    }
}
