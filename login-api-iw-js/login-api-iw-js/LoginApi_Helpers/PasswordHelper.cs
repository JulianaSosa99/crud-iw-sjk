using System.Security.Cryptography;
using System.Text;

namespace login_api_iw_js.LoginApi_Helpers
{
    public class PasswordHelper
    {
        // Generar un hash de la contraseña utilizando SHA256
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("La contraseña no puede ser nula o vacía.");
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password); // Convertir a bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);    // Generar el hash
                return Convert.ToBase64String(hashBytes);                 // Convertir el hash a Base64
            }
        }

        // Verificar si la contraseña ingresada coincide con el hash almacenado
        public static bool verifyPassword(string passwordIngresada, string storedHash)
        {
            if (string.IsNullOrEmpty(passwordIngresada) || string.IsNullOrEmpty(storedHash))
            {
                throw new ArgumentNullException("La contraseña o el hash no pueden ser nulos.");
            }

            // Hasheamos la contraseña ingresada y comparamos con el hash almacenado
            string hashedPassword = HashPassword(passwordIngresada);
            return hashedPassword == storedHash;
        }
    }
}
