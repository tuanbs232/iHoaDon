using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace iHoaDon.Business
{
    /// <summary>
    /// Some extension methods for entities
    /// </summary>
    public static class EntityUtils
    {
        /// <summary>
        /// Merge dictionary.
        /// Like Jquery's $.extend(source, target)
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void Append(this IDictionary<string, object> target, IDictionary<string, object> source)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (source == null)
            {
                return;
            }

            foreach (var key in source.Keys)
            {
                var val = source[key];
                if (val == null)
                {
                    continue;
                }

                if (target.ContainsKey(key))
                {
                    target[key] = val;
                }
                else
                {
                    target.Add(key, val);
                }
            }
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            var result = new StringBuilder();
            const string src = "1234567890";
            var seed = GetRandomSeed();
            var rnd = new Random(seed);
            length.Times(() => result.Append(src[rnd.Next(src.Length - 1)]));
            return result.ToString();
        }

        /// <summary>
        /// Gets the random seed.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            var rng = new RNGCryptoServiceProvider();
            var randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            return (randomBytes[0] & 0x7f) << 24 |
                   randomBytes[1] << 16 |
                   randomBytes[2] << 8 |
                   randomBytes[3];
        }

        /// <summary>
        /// Generates some the random bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] GenerateRandomBytes(int length)
        {
            var result = new byte[length];
            RandomNumberGenerator.Create().GetBytes(result);
            return result;
        }

        /// <summary>
        /// Hash the input password
        /// </summary>
        /// <param name="pwd">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public static byte[] GetInputPasswordHash(string pwd, byte[] salt)
        {
            var inputPwdBytes = Encoding.UTF8.GetBytes(pwd);
            var inputPwdHasher = new Rfc2898DeriveBytes(inputPwdBytes, salt, Constants.PasswordDerivationIteration);
            return inputPwdHasher.GetBytes(Constants.PasswordBytesLength);
        }

        ///<summary>
        /// Generate notify number
        ///</summary>
        ///<param name="date">The date</param>
        ///<returns></returns>
        public static string GenerateNotifyNumber(DateTime date)
        {
            return string.Format("{0:MMddHHmmssfff/yyyy}/TB-TVAN", date);
        }
    }
}
