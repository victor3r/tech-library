using library.api.Domain.Entities;
using library.api.Infraestructure;
using library.communication.Requests;
using library.communication.Responses;
using library.exception;

namespace library.api.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestUserJson request)
        {
            Validate(request);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var dbContext = new LibraryDbContext();

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name
            };
        }

        private void Validate(RequestUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
