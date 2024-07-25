namespace Steam.Models.Account
{
    public class RegisterViewModel
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
    }
}
