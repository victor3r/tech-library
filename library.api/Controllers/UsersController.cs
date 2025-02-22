using library.api.UseCases.Users.Register;
using library.communication.Requests;
using library.communication.Responses;
using library.exception;
using Microsoft.AspNetCore.Mvc;

namespace library.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Create(RequestUserJson request)
        {
            try
            {
                var useCase = new RegisterUserUseCase();

                var response = useCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (LibraryException ex)
            {

                return BadRequest(new ResponseErrorMessagesJson { Errors = ex.GetErrorMessages() });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson { Errors = ["Internal Server Error"] });
            }
        }
    }
}
