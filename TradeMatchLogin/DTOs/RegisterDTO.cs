
namespace TradeMatchLogin.DTOs
{
    public class RegisterDTO
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

        //
        // Login Data
        //
        public string UserName { get; set; }

        public string Password { get; set; }

        //
        // Role Data 
        //
        public string RoleType { get; set; }

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
