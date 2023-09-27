namespace TradeMatchLogin.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public string Error { get; set; }
    }
}
