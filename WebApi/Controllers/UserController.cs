using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IJwtService _jwtService;

        public UserController(IUserService service, IJwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _service.GetByUsername(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _service.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<ActionResult> Get(string username)
        {
            var user = await _service.GetByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _service.Add(value);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _service.Update(value);
            return Ok(user);
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> Remove(string username)
        {
            var existingUser = await _service.GetByUsername(username);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _service.Remove(existingUser.Id);
            return NoContent();
        }
    }
}