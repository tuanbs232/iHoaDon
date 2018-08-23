using System.IO;

namespace iHoaDon.Util
{
    /// <summary>
    /// Functions for working with directories
    /// </summary>
    public static class DirectoryUtil
    {
        /// <summary>
        /// Ensures the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string Ensure(string path)
        {
            return ToAbsoluteAndEnsure(path, null);
        }

        /// <summary>
        /// Resolve and create the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fromBasePath">From base path.</param>
        /// <returns></returns>
        public static string ToAbsoluteAndEnsure(string path, string fromBasePath)
        {
            var absolutePath = PathUtil.ToAbsolute(path, fromBasePath);
            if (absolutePath != null && !Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            return absolutePath;
        }
    }
}