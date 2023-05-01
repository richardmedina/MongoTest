using Microsoft.AspNetCore.Mvc;
using MongoTest.Common.Domain.Repository;
using MongoTest.Contract.Domain;

namespace MongoTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByFullName([FromQuery] string? fullName)
        {
            var result = string.IsNullOrWhiteSpace(fullName)
                ? await userRepository.GetAllAsync()
                : await userRepository.GetByFullNameAsync(fullName);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await userRepository.GetAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDomain user)
        {
            var result = await userRepository.CreateAsync(user);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await userRepository.DeleteAsync(id);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] UserDomain user)
        {
            await userRepository.UpdateAsync(id, user);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
