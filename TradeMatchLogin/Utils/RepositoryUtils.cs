using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;

namespace TradeMatchLogin.Utils
{
    public class RepositoryUtils
    {

        // Create a new User object from the UserRegistrationDTO, add it to the repo and return it.
        public static User AddUser(RegisterDto registerDto, UserRepository _userRepo)
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
        public static Login AddLogin(RegisterDto registerDto, int userID, LoginRepository _loginRepo, string hash)
        {
            
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
        public static void AddAddress(RegisterDto registerDto, int userID, AddressRepository _addressRepo)
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
    }
}
