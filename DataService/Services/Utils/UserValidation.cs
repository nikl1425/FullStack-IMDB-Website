using System;
using System.Security.Cryptography;

namespace DataService.Services.Utils
{
    public class UserValidation
    {
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            if(String.IsNullOrEmpty(enteredPassword))
                throw new ArgumentNullException(nameof(enteredPassword));
            
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}