using library.api.Infraestructure.DataAccess;
using library.communication.Requests;
using library.communication.Responses;
using Microsoft.EntityFrameworkCore;

namespace library.api.UseCases.Books.Filter;

public class FilterBookUseCase
{
    private const int PAGE_SIZE = 10;

    public ResponseBooksJson Execute(RequestFilterBooksJson request)
    {
        var dbContext = new LibraryDbContext();

        if (request.PageNumber < 1)
        {
            request.PageNumber = 1;
        }

        var skip = (request.PageNumber - 1) * PAGE_SIZE;

        var query = dbContext.Books.AsQueryable();
        var totalCount = dbContext.Books.Count();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query
                .Where(book => EF.Functions
                .Like(book.Title, $"%{request.Title}%"));

            totalCount = dbContext.Books
                .Count(book => EF.Functions
                .Like(book.Title, $"%{request.Title}%"));
        }

        var books = query.OrderBy(book => book.Title)
           .ThenBy(book => book.Author)
           .Skip(skip)
           .Take(PAGE_SIZE)
           .ToList();

        return new ResponseBooksJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalCount,
            },
            Books = [.. books.Select(book => new ResponseBookJson
            {
                Title = book.Title,
                Author = book.Author,
                Id = book.Id,
                Amount= book.Amount
            })],
        };
    }
}
