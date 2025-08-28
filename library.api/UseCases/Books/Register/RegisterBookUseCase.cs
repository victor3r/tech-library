using library.api.Domain.Entities;
using library.api.Infraestructure.DataAccess;
using library.communication.Requests;
using library.communication.Responses;

namespace library.api.UseCases.Books.Register;

public class RegisterBookUseCase
{
    public ResponseRegisteredBookJson Execute(RequestBookJson request)
    {
        var dbContext = new LibraryDbContext();

        var existingBook = dbContext.Books
            .FirstOrDefault(book => book.Title.Equals(request.Title));

        if (existingBook is not null)
        {
            existingBook.Amount++;
            dbContext.SaveChanges();

            return new ResponseRegisteredBookJson
            {
                Title = existingBook.Title
            };
        }

        var newBook = new Book
        {
            Author = request.Author,
            Title = request.Title,
            Amount = 1
        };

        dbContext.Books.Add(newBook);
        dbContext.SaveChanges();

        return new ResponseRegisteredBookJson
        {
            Title = newBook.Title
        };
    }
}
