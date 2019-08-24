using System;
using CS321_W5D2_BlogAPI.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CS321_W5D2_BlogAPI.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CS321_W5D2_BlogAPI.Controllers
{
    // TODO: secure controller actions that change data
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        private readonly IPostService _postService;

        // TODO: inject PostService
        public PostsController(IPostService postService)
        {
            postService = postService;
        }

        // TODO: get posts for blog
        // TODO: allow anyone to get, even if not logged in
        // GET /api/blogs/{blogId}/posts
        [HttpGet("/api/blogs/{blogId}/posts")]
        [AllowAnonymous]
        public IActionResult Get(int blogId)
        {
            // TODO: replace the code below with the correct implementation
            
           var posts = _postService.GetBlogPosts(blogId);
            var postModels = posts.Select(p => p.ToApiModel());
            return Ok(postModels);
        }

        // TODO: get post by id
        // TODO: allow anyone to get, even if not logged in
        // GET api/blogs/{blogId}/posts/{postId}
        [HttpGet("/api/blogs/{blogId}/posts/{postId}")]
        [AllowAnonymous]
        public IActionResult Get(int blogId, int postId)
        {
            // TODO: replace the code below with the correct implementation
            var posts = _postService.GetBlogPosts(blogId);
            var post = posts.FirstOrDefault(p => p.Id == postId);
            var postModel = post.ToApiModel();

            return Ok(postModel);
           
        }

        // TODO: add a new post to blog
        // POST /api/blogs/{blogId}/post
        [HttpPost("/api/blogs/{blogId}/posts")]
        [AllowAnonymous]
        public IActionResult Post(int blogId, [FromBody]PostModel postModel)
        {
            // TODO: replace the code below with the correct implementation
            try
            {
                var post = postModel.ToDomainModel();
                _postService.Add(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddPost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT /api/blogs/{blogId}/posts/{postId}
        [HttpPut("/api/blogs/{blogId}/posts/{postId}")]
        [Authorize]
        public IActionResult Put(int blogId, int postId, [FromBody]PostModel postModel)
        {
            try
            {
                var updatedPost = _postService.Update(postModel.ToDomainModel());
                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdatePost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // TODO: delete post by id
        // DELETE /api/blogs/{blogId}/posts/{postId}
        [HttpDelete("/api/blogs/{blogId}/posts/{postId}")]
        [Authorize]
        public IActionResult Delete(int blogId, int postId)
        {
            // TODO: replace the code below with the correct implementation
             try
            {
                _postService.Remove(postId);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeletePost", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
   
