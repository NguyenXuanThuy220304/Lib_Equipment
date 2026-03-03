using System;
using System.Security.Cryptography;
using System.Text;

namespace Lib_Equipment.Helpers
{
    public class SecurityHelper
    {
        /// <summary>
        /// Hàm băm chuỗi mật khẩu sang định dạng SHA-256 (64 ký tự)
        /// </summary>
        public static string HashSHA256(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            using (SHA256 sha256 = SHA256.Create())
            {
                // 1. Chuyển chuỗi thành mảng byte
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // 2. Chuyển mảng byte thành chuỗi Hexa
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // "x2" định dạng chuỗi theo hệ cơ số 16 (hexadecimal) chữ thường
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}