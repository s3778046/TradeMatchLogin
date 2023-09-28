using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;

namespace TradeMatchLogin.Utils
{
    public class RepositoryUtils
    {

        // Create a new User object from the UserRegistrationDTO, add it to the repo and return it.
        public static async Task<User> AddUser(RegisterDto registerDto, UserRepository _userRepo)
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

            await _userRepo.AddAsync(user);

            return user;
        }

        // Create a new Login object from the UserRegistrationDTO, add it to the repo and return it.
        public static async Task<Login> AddLogin(RegisterDto registerDto, Guid userID, LoginRepository _loginRepo, string hash)
        {
            
            // Create a new login
            var login = new Login()
            {
                // Create a new instance of SimpleHash.
                UserName = registerDto.UserName,
                PasswordHash = hash,
                UserID = userID
            };

            await _loginRepo.AddAsync(login);

            return login;
        }

        // Create a new Address object from the UserRegistrationDTO and add it to the repo.
        public static async Task<Address> AddAddress(RegisterDto registerDto, Guid userID, AddressRepository _addressRepo)
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

            await _addressRepo.AddAsync(address);

            return address;
        }
    }
}
