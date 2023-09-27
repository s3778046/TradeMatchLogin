using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMatchLogin.Models;

public class Role
{
 
    public int RoleID { get; set; }

    public string RoleType{ get; set; }

    // UserID
    [ForeignKey("UserID")]
    public int UserID { get; set; }

}
