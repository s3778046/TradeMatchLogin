using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradeMatchLogin.DTOs;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;
using TradeMatchLogin.Validators.DTOValidators;
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
        private readonly RoleRepository _roleRepo;
        private readonly IConfiguration _configuration;
       
        // Constructor
        public AuthController(UserRepository userRepo, LoginRepository loginRepo,
               RoleRepository roleRepo, AddressRepository addressRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _loginRepo = loginRepo;
            _roleRepo = roleRepo;
            _addressRepo = addressRepo;
            _configuration = configuration;
        }

        // Register route
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            // Create a Fluent validator and pass request through it
            RegisterDTOValidator validator = new();
            ValidationResult result = validator.Validate(registerDTO);

            // Validate Request by checking the result is valid 
            if (result.IsValid)
            {

                // Get user from loginRepo by username.
                var usernameExists = await _loginRepo.GetByUserNameAsync(registerDTO.UserName);

                // If UsernameExists return a bad request with message.
                if (usernameExists != null)
                {
                    return BadRequestWithMessage("UserName already exists");
                }

                // Add registerDTO data to relevent user models.
                var user = AddUser(registerDTO);
                var login = AddLogin(registerDTO, user.UserID);
                AddRole(registerDTO, user.UserID);
                AddAddress(registerDTO, user.UserID);

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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            LoginDTOValidator validator = new();
            ValidationResult result = validator.Validate(loginDTO);

            // Validate Request by checking the validation result is valid 
            if (result.IsValid)
            {
                // Get login data from loginRepo by username.
                var login = await _loginRepo.GetByUserNameAsync(loginDTO.UserName);

                // If login is null, return a bad request with message.
                if (login == null)
                {
                    return BadRequestWithMessage("Invalid credentials");
                }

                // If request password is not equal to password in the login object, return a bad request with message.
                if (!login.Password.Equals(loginDTO.Password))
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
        private User AddUser(RegisterDTO registerDTO)
        {
            // Create a new user
            var user = new User()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Phone = registerDTO.Phone,
                Email = registerDTO.Email,
                ABN = registerDTO.ABN,
                BusinessName = registerDTO.BusinessName,
                Status = "Registered"
            };

            _userRepo.Add(user);

            return user;
        }

        // Create a new Login object from the UserRegistrationDTO, add it to the repo and return it.
        private Login AddLogin(RegisterDTO registerDTO, int userID)
        {
            // Create a new login
            var login = new Login()
            {
                UserName = registerDTO.UserName,
                Password = registerDTO.Password,
                UserID = userID
            };

            _loginRepo.Add(login);

            return login;
        }

        // Create a new Address object from the UserRegistrationDTO and add it to the repo.
        private void AddAddress(RegisterDTO registerDTO, int userID)
        {
            // Create a new address
            var address = new Address()
            {
                Number = registerDTO.Number,
                Street = registerDTO.Street,
                Suburb = registerDTO.Suburb,
                PostCode = registerDTO.PostCode,
                State = registerDTO.State,
                UserID = userID
            };

            _addressRepo.Add(address);
        }

        // Create a new Role object from the UserRegistrationDTO and add it to the repo.
        private void AddRole(RegisterDTO registerDTO, int userID)
        {
            // Create a new role
            var role = new Role()
            {
                RoleType = registerDTO.RoleType,
                UserID = userID
            };

            _roleRepo.Add(role);
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
