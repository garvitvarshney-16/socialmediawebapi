using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Data;
using SocialMediaApi.Models;
using SocialMediaApi.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace SocialMediaApi.PostService;


public class PostService : IPostService
{
    private readonly SocialMediaContext _context;


    public PostService(SocialMediaContext context)
    {
        _context = context;
    }

    public async Task CreatePostAsync(PostDto dto, int userId)
    {
        var post = new Post
        {
            UserId = userId,
            Content = dto.Content
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetPostsAsync(int pageNumber, int pageSize)
    {
        return await _context.Posts
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    Task<string> IPostService.GetPostsAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}