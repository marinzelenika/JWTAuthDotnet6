using AutoMapper;
using JWTAuthDotnet6.Auth;
using JWTAuthDotnet6.DTOs;
using JWTAuthDotnet6.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JWTAuthDotnet6.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IMapper mapper, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPostsByUser(int userId)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Post> dbPosts = await _context.Posts.Where(p => p.UserId == userId.ToString()).ToListAsync();
            serviceResponse.Data = (dbPosts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Post> dbPosts = await _context.Posts.ToListAsync();
            serviceResponse.Data = (dbPosts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            Post post = _mapper.Map<Post>(newPost);
            post.createdAt = DateTime.Now;
            post.updatedAt = DateTime.Now;
            //post.UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            Post dbPost = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            serviceResponse.Data = _mapper.Map<GetPostDto>(dbPost);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            try
            {
                Post post = await _context.Posts.FirstAsync(p => p.PostId == id);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = (_context.Posts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            try
            {
                Post post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == updatedPost.PostId);

                post.Title = updatedPost.Title;
                post.Content = updatedPost.Content;
                post.createdAt = updatedPost.createdAt;
                post.updatedAt = DateTime.Now;



                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetPostDto>(post);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
