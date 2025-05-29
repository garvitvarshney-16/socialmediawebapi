using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApi.Data;
using SocialMediaApi.Models;
using SocialMediaApi.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace SocialMediaApi.CommentService;


public class CommentService : ICommentService
{
    private readonly SocialMediaContext _context;

    public CommentService(SocialMediaContext context)
    {
        _context = context;
    }

    public async Task CreateCommentAsync(CommentDto dto, int postId, int userId)
    {
        var post = await _context.Posts.FindAsync(postId)
            ?? throw new Exception("Post not found.");

        var comment = new Comment
        {
            PostId = postId,
            UserId = userId,
            Content = dto.Content
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsAsync(int postId, int pageNumber, int pageSize)
    {
        return await _context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    Task<string> ICommentService.GetCommentsAsync(int postId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}