using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace iHoaDon.Util
{
    /// <summary>
    /// Extension for streams
    /// </summary>
    public static class StreamExtension
    {
        /// <summary>
        /// Hashes the stream.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        public static byte[] Hash(this Stream input, string algorithm)
        {
            if(input == null)
            {
                throw new ArgumentNullException("input");
            }
            if(string.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("algorithm");
            }
            using (var algo = HashAlgorithm.Create(algorithm))
            {
                return algo.ComputeHash(input);
            }
        }

        /// <summary>
        /// Writes to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="data">The data.</param>
        public static void Write(this Stream stream, ref byte[] data)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Copies from stream A to stream B (for .NET3.5, .NET4 has this function built in).
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void CopyTo(this Stream from, Stream to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            var buffer = new byte[4096];
            while (true)
            {
                var read = from.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    return;
                }
                to.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// Reads the stream as a string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string ReadAsString(this Stream stream, Encoding encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return encoding.GetString(memStream.ToArray());
            }
        }
    }
}