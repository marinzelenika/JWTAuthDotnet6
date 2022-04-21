using JWTAuthDotnet6.DTOs;
using JWTAuthDotnet6.Models;

namespace JWTAuthDotnet6.Services
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDto>>> GetAllPostsByUser(int userId);
        Task<ServiceResponse<List<GetPostDto>>> GetAllPosts();
        Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost);
        Task<ServiceResponse<GetPostDto>> GetPostById(int id);
        Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id);
        Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost);
    }
}
