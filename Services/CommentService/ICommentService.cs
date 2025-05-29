using SocialMediaApi.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace SocialMediaApi.CommentService;


public interface ICommentService
{
    Task CreateCommentAsync(CommentDto dto, int postId, int userId);
    Task<string> GetCommentsAsync(int postId, int pageNumber, int pageSize);
}