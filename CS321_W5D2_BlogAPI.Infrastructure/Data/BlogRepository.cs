using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogRepository(AppDbContext dbContext) 
        {
             _dbContext = dbContext;
        }

        public IEnumerable<Blog> GetAll()
        {
            // TODO: Retrieve all blgs. Include Blog.User.
             return _dbContext.Blogs
               .Include(b => b.Posts)
               .Include(b => b.User)
               .ToList();
        }

        public Blog Get(int id)
        {
            // TODO: Retrieve the blog by id. Include Blog.User.
            throw new NotImplementedException();
        }

        public Blog Add(Blog blog)
        {
            // TODO: Add new blog
             _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return blog;
        }

        public Blog Update(Blog updatedItem)
        {
            // TODO: update blog
             var currentBlog = _dbContext.Blogs.Find(updatedBlog.Id);
            if (currentBlog == null) return null;
            _dbContext.Entry(currentBlog)
                .CurrentValues
                .SetValues(updatedBlog);
           _dbContext.Blogs.Update(currentBlog);
            _dbContext.SaveChanges();
            return currentBlog;
            var existingItem = _dbContext.Find(updatedItem.Id);
            if (existingItem == null) return null;
            _dbContext.Entry(existingItem)
               .CurrentValues
               .SetValues(updatedItem);
            _dbContext.Blogs.Update(existingItem);
            _dbContext.SaveChanges();
            return existingItem;


        }

        public void Remove(int id)
        {
            // TODO: remove blog
            var deleteBlog = Get(id);
              if (deleteBlog != null) 
            _dbContext.Blogs.Remove(blogToDelete);
            _dbContext.SaveChanges();
        }
    }
}
