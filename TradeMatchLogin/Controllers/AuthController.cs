using Microsoft.AspNetCore.Mvc;
using SimpleHashing.Net;
using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;
using TradeMatchLogin.Utils;
using TradeMatchLogin.Validators.DtoValidators;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace TradeMatchLogin.Controllers
{

    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        private readonly LoginRepository _loginRepo;
        private readonly AddressRepository _addressRepo;
        private readonly JwtGenerator _jwtGenerator;
        private static readonly ISimpleHash simpleHash = new SimpleHash();

        // Constructor
        public AuthController(UserRepository userRepo, LoginRepository loginRepo,
               AddressRepository addressRepo, JwtGenerator jwtGenerator)
        {
            _userRepo = userRepo;
            _loginRepo = loginRepo;
            _addressRepo = addressRepo;
            _jwtGenerator = jwtGenerator;
        }

        // Register route
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            // Create a Fluent validator and pass request through it
            RegisterDtoValidator validator = new();
            ValidationResult result = validator.Validate(registerDto);

            // Validate Request by checking the result is valid 
            if (result.IsValid)
            {

                // Get user from loginRepo by username.
                var usernameExists = await _loginRepo.GetByUserNameAsync(registerDto.UserName);

                // If UsernameExists return a bad request with message.
                if (usernameExists != null)
                {
                    return BadRequestWithMessage("UserName already exists");
                }

                // Hash the password input
                var hash = simpleHash.Compute(registerDto.Password);

                // Add registerDTO data to relevent user models.
                var user = RepositoryUtils.AddUser(registerDto, _userRepo);
                var login = RepositoryUtils.AddLogin(registerDto, user.UserID, _loginRepo, hash);
                RepositoryUtils.AddAddress(registerDto, user.UserID, _addressRepo);

                // Generate a json web token
                var token = _jwtGenerator.GenerateJsonWebToken(user);

                // Return OK with the token
                return OkWithToken(token);
            }

            return BadRequestWithMessage(result.ToString());
        }

        // Login route
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {
            LoginDtoValidator validator = new();
            ValidationResult result = validator.Validate(loginDTO);

            // Validate Request by checking the validation result is valid 
            if (result.IsValid)
            {
                // Get login data from loginRepo by username.
                var login = await _loginRepo.GetByUserNameAsync(loginDTO.UserName);

                // If login is null or passwords do not match, return a bad request with message.
                if (login == null || string.IsNullOrEmpty(loginDTO.Password) || !simpleHash.Verify(loginDTO.Password, login.PasswordHash))
                {
                    return BadRequestWithMessage("Invalid credentials");
                }

                var user = _userRepo.Get(login.UserID);

                // Generate a json web token
                var token = _jwtGenerator.GenerateJsonWebToken(user);

                // Return OK with the token
                return OkWithToken(token);
            }
            // Return a bad request with message.
            return BadRequestWithMessage("Invalid credentials");
        }

        // Return a Bad request object with a message.
        private BadRequestObjectResult BadRequestWithMessage(string message)
        {
            return BadRequest(new AuthResult()
            {
                Error = message,
                Result = false
            });
        }

        // Return an OK request object with a message.
        private OkObjectResult OkWithToken(string token)
        {
            return Ok(new AuthResult()
            {
                Result = true,
                Token = token
            });
        }
    }
}
