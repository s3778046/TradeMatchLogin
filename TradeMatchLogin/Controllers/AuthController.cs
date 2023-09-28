using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleHashing.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;
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
        private readonly IConfiguration _configuration;
        private static readonly ISimpleHash simpleHash = new SimpleHash();

        // Constructor
        public AuthController(UserRepository userRepo, LoginRepository loginRepo,
               AddressRepository addressRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _loginRepo = loginRepo;
            _addressRepo = addressRepo;
            _configuration = configuration;
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

                // Add registerDTO data to relevent user models.
                var user = AddUser(registerDto);
                var login = AddLogin(registerDto, user.UserID);
                AddAddress(registerDto, user.UserID);

                // Generate a json web token
                var token = GenerateJsonWebToken(login);

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

                // Generate a json web token
                var token = GenerateJsonWebToken(login);

                // Return OK with the token
                return OkWithToken(token);
            }
            // Return a bad request with message.
            return BadRequestWithMessage("Invalid credentials");
        }


        // Function to generate a json web token.
        private string GenerateJsonWebToken(Login login)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Create a token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    //new Claim("ID",  user.UserID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        // Create a new User object from the UserRegistrationDTO, add it to the repo and return it.
        private User AddUser(RegisterDto registerDto)
        {
            // Create a new user
            var user = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Phone = registerDto.Phone,
                Email = registerDto.Email,
                ABN = registerDto.ABN,
                BusinessName = registerDto.BusinessName,
                Status = "Registered",
                Role = registerDto.Role,
            };

            _userRepo.Add(user);

            return user;
        }

        // Create a new Login object from the UserRegistrationDTO, add it to the repo and return it.
        private Login AddLogin(RegisterDto registerDto, int userID)
        {
            // Hash the password input
            var hash = simpleHash.Compute(registerDto.Password);

                // Create a new login
            var login = new Login()
            {
                // Create a new instance of SimpleHash.
                UserName = registerDto.UserName,
                PasswordHash = hash,
                UserID = userID
            };

            _loginRepo.Add(login);

            return login;
        }

        // Create a new Address object from the UserRegistrationDTO and add it to the repo.
        private void AddAddress(RegisterDto registerDto, int userID)
        {
            // Create a new address
            var address = new Address()
            {
                Number = registerDto.Number,
                Street = registerDto.Street,
                Suburb = registerDto.Suburb,
                PostCode = registerDto.PostCode,
                State = registerDto.State,
                UserID = userID
            };

            _addressRepo.Add(address);
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

        // Return a OK request object with a message.
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
