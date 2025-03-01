using library.api.Domain.Entities;
using library.api.Infraestructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;

namespace library.api.Services.LoggedUser
{
    public class LoggedUserService(HttpContext httpContext)
    {

        private readonly HttpContext _httpContext = httpContext;

        public User GetLoggedUser(LibraryDbContext dbContext)
        {
            var authorization = _httpContext.Request.Headers.Authorization.ToString();

            var token = authorization["Bearer".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            var userId = Guid.Parse(identifier);

            return dbContext.Users
                .First(user => user.Id == userId);
        }
    }
}
