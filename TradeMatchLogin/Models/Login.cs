using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMatchLogin.Models;

public class Login
{
    
    
    [Key]
    public Guid LoginID { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    // UserID
    [ForeignKey("UserID")]
    public Guid UserID { get; set; }
}
