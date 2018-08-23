using System;
using System.Linq;
using System.Security.Cryptography;

namespace iHoaDon.Util
{
    /// <summary>
    /// Extension for byte[]
    /// </summary>
    public static class ByteArrayExtension
    {
        /// <summary>
        /// Hashes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        public static byte[] Hash(this byte[] input, string algorithm)
        {
            if(input == null)
            {
                throw new ArgumentNullException("input");
            }
            if(String.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("algorithm");
            }
            using(var algo = HashAlgorithm.Create(algorithm))
            {
                return algo.ComputeHash(input);
            }
        }

        /// <summary>
        /// Convert the input byte array into base64 string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string AsBase64(this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// Concats the specified contents.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns></returns>
        public static byte[] Concat(params byte[][] contents)
        {
            var totalLength = contents.Sum(c=>c.Length);
            var result = new byte[totalLength];

            var start = 0;
            foreach (var byteArray in contents)
            {
                Buffer.BlockCopy(byteArray, 0, result, start, byteArray.Length);
                start += byteArray.Length;
            }
            return result;
        }
    }
}