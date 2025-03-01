using library.api.Infraestructure.DataAccess;
using library.api.Infraestructure.Security.Cryptography;
using library.api.Infraestructure.Security.Tokens.Access;
using library.communication.Requests;
using library.communication.Responses;
using library.exception;

namespace library.api.UseCases.Login.Login
{
    public class LoginUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            var dbContext = new LibraryDbContext();

            var user = dbContext.Users
                .FirstOrDefault(user => user.Email.Equals(request.Email)) ??
                throw new InvalidLoginException();

            var cryptography = new BCryptAlgorithm();

            var passwordIdValid = cryptography.Verify(request.Password, user.Password);

            if (!passwordIdValid)
            {
                throw new InvalidLoginException();
            }

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                AccessToken = tokenGenerator.Generate(user)
            };
        }
    }
}
