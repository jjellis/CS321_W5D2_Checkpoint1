using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;
        }

        public Post Add(Post newPost)
        {
            // TODO: Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            //     You may have to retrieve the blog in order to check user id
            // TODO: assign the current date to DatePublished
             var blogId = newPost.BlogId;
            var blog = _blogRepository.Get(blogId);
            var userId = blog.UserId;

            var currentUser = _userService.CurrentUserId;

            if (currentUser != userId)
            {
                throw new Exception("This blog does not belong to you. Your post is not added.");
            }
            newPost.DatePublished = DateTime.Now;
            return _postRepository.Add(newPost);
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            var post = this.Get(id);
            // TODO: prevent user from deleting from a blog that isn't theirs
            
            var userId = blog.UserId;

            var currentUser = _userService.CurrentUserId;

            if (currentUser != userId)
            {
                throw new Exception("This blog does not belong to you. This post is not removed.");
            }
            _postRepository.Remove(id);
        }

        public Post Update(Post updatedPost)
        {
            // TODO: prevent user from updating a blog that isn't theirs
            var userId = blog.UserId;

            var currentUser = _userService.CurrentUserId;

            if (currentUser != userId)
            {
                throw new Exception("This blog does not belong to you. This post is not updated.");
            }
            return _postRepository.Update(updatedPost);
        }

    }
}
