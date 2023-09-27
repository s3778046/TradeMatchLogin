using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMatchLogin.Models;

public class Address
{
   
    public int AddressID { get; set; }

    public string Number { get; set; }

    public string Street { get; set; }

    public string Suburb { get; set; }

    public int PostCode { get; set; }

    public string State { get; set; }

    [ForeignKey("UserID")]
    public int UserID { get; set; }
}