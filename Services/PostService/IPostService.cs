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

public interface IPostService
{
    Task CreatePostAsync(PostDto dto, int userId);
    Task<string> GetPostsAsync(int pageNumber, int pageSize);
}