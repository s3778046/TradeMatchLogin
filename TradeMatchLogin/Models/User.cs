using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace TradeMatchLogin.Models;

public class User
{
 
    public int UserID { get; set; }

    public string FirstName { get; set; }
   
    public string LastName { get; set; }
 
    public string Phone { get; set; }

    public string Email { get; set; }

    public string ABN { get; set; }

    public string BusinessName { get; set; }

    public string Status { get; set; }

    public string Role { get; set; }
}