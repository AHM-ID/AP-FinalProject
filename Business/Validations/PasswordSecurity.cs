﻿using System.Security.Cryptography;
using System.Text;

namespace Business.Validations
{
    public static class PasswordSecurity
    {
        public static string HashPassword(string value)
        {
            StringBuilder Hashpass = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (byte b in result)
                    Hashpass.Append(b.ToString("x2"));
            }
            return Hashpass.ToString();
        }
    }
}
