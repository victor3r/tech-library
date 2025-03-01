using library.api.Domain.Entities;
using library.api.Infraestructure.DataAccess;
using library.api.Services.LoggedUser;
using library.exception;

namespace library.api.UseCases.Checkouts
{
    public class RegisterBookCheckoutUseCase(LoggedUserService loggedUserService)
    {
        private readonly LoggedUserService _loggedUserService = loggedUserService;
        private const int MAX_LOAN_DAYS = 7;

        public void Execute(Guid bookId)
        {
            var dbContext = new LibraryDbContext();

            Validate(dbContext, bookId);

            var user = _loggedUserService.GetLoggedUser(dbContext);

            dbContext.Checkouts.Add(new Checkout
            {
                UserId = user.Id,
                BookId = bookId,
                ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
            });

            dbContext.SaveChanges();
        }

        private static void Validate(LibraryDbContext dbContext, Guid bookId)
        {
            var book = dbContext.Books
                .FirstOrDefault(book => book.Id == bookId) ??
                throw new NotFoundException("Book not found");

            var amountBookNotReturned = dbContext.Checkouts
                .Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);

            if (amountBookNotReturned == book.Amount)
            {
                throw new ConflictException("Book unavailable");
            }
        }
    }
}
