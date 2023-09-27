using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMatchLogin.Models;

public class Login
{
    // LoginId
    public int LoginID { get; set; }

    // Username
    [Required, StringLength(30)]
    public string UserName { get; set; }

    // PasswordHash
    [Required, StringLength(94)]
    public string Password { get; set; }

    // UserID
    [ForeignKey("UserID"), Required]
    public int UserID { get; set; }

    // Navigation Properies
   //public virtual User User { get; set; }

}
