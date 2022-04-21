using JWTAuthDotnet6.DTOs;
using JWTAuthDotnet6.Models;
using JWTAuthDotnet6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTAuthDotnet6.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetAllPostsByUser")]
        public async Task<IActionResult> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _postService.GetAllPostsByUser(id));
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinglePost(int id)
        {
            return Ok(await _postService.GetPostById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetPostDto>> response = await _postService.DeletePost(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> response = await _postService.UpdatePost(updatedPost);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
