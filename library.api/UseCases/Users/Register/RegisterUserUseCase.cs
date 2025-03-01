using FluentValidation.Results;
using library.api.Domain.Entities;
using library.api.Infraestructure.DataAccess;
using library.api.Infraestructure.Security.Cryptography;
using library.api.Infraestructure.Security.Tokens.Access;
using library.communication.Requests;
using library.communication.Responses;
using library.exception;

namespace library.api.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestUserJson request)
        {
            var dbContext = new LibraryDbContext();

            Validate(request, dbContext);

            var cryptography = new BCryptAlgorithm();

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = cryptography.HashPassword(request.Password),
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                AccessToken = tokenGenerator.Generate(user)
            };
        }

        private static void Validate(RequestUserJson request, LibraryDbContext dbContext)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var existUserWithEmail = dbContext.Users
                .Any(user => user.Email.Equals(request.Email));

            if (existUserWithEmail)
            {
                result.Errors.Add(
                    new ValidationFailure("Email", "Email already registered"));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
