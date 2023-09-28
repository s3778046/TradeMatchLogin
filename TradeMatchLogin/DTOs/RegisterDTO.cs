
namespace TradeMatchLogin.Dtos
{
    public class RegisterDto
    {
        //
        // User Data
        //
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ABN { get; set; }

        public string BusinessName { get; set; }

        public string Role { get; set; }

        //
        // Login Data
        //
        public string UserName { get; set; }

        public string Password { get; set; }


        //
        //Address Data
        //
        public string Number { get; set; }

        public string Street { get; set; }

        public string Suburb { get; set; }

        public int PostCode { get; set; }

        public string State { get; set; }
    }
}
