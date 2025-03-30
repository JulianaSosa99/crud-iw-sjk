namespace login_api_iw_js.LoginApi_Helpers
{
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Security.Cryptography;
    using System.Text;

    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }

           
        }
        public static bool verifyPassword(string passwordIngresada, string storedHash)
        {
            return HashPassword(passwordIngresada) == storedHash;   
        }
    }
}
