using library.api.Services.LoggedUser;
using library.api.UseCases.Checkouts;
using library.communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutsController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
        public IActionResult BookCheckout(Guid bookId)
        {
            var loggedUserService = new LoggedUserService(HttpContext);

            var useCase = new RegisterBookCheckoutUseCase(loggedUserService);

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
