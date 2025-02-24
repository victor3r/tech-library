using library.api.UseCases.Login.Login;
using library.communication.Requests;
using library.communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace library.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
        public IActionResult Login(RequestLoginJson request)
        {
            var useCase = new LoginUseCase();

            var response = useCase.Execute(request);

            return Ok(response);
        }
    }
}
