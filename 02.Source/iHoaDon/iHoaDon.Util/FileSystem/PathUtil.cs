using System;
using System.IO;

namespace iHoaDon.Util
{
    /// <summary>
    /// Methods for dealing with path strings
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// Gets a unique file using GUID.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        public static string GetGuidFileName(string path, string extension, string prefix = "", string suffix = "")
        {
            var sanPath = DirectoryUtil.Ensure(path);
            var ext = String.IsNullOrEmpty(extension) 
                        ? String.Empty 
                        : extension.StartsWith(".") 
                            ? extension 
                            : "." + extension;
            string result;
            do
            {
                var uniqueName = prefix + Guid.NewGuid().ToString("N") + suffix + ext;
                result = Path.Combine(sanPath, uniqueName);
            } while (File.Exists(result));
            return result;
        }

        /// <summary>
        /// Generate a unique file name using the current datetime
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <returns></returns>
        public static string GetTimeStampFileName(string path, string prefix, string suffix, string dateTimeFormat)
        {
            var sanPath = DirectoryUtil.Ensure(path);
            string result;
            do
            {
                var name = String.Format("{0}{1}{2}", prefix, DateTime.Now.ToString(dateTimeFormat), suffix);
                result = Path.Combine(sanPath, name);
            } while (File.Exists(result));
            return result;
        }

        /// <summary>
        /// Gets a unique file using Path.GetRandomFileName().
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        public static string GetTempFileName(string path, string extension)
        {
            string fileName;
            do
            {
                fileName = Path.Combine(path, Path.GetRandomFileName() + extension);
            }
            while (File.Exists(fileName));
            return fileName;
        }

        /// <summary>
        /// Increments the name of the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public static string IncrementFileName(string name, int i)
        {
            var bareName = Path.GetFileNameWithoutExtension(name);
            var extension = Path.GetExtension(name);
            return bareName + i + extension;
        }

        /// <summary>
        /// Resolve absolute path.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns></returns>
        public static string ToAbsolute(string inputPath, string basePath)
        {
            return Path.Combine
                (
                    String.IsNullOrEmpty(basePath)
                        ? AppDomain.CurrentDomain.BaseDirectory
                        : basePath,
                    inputPath ?? String.Empty
                );
        }

        /// <summary>
        /// Gets the extension without dot.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetExtensionWithoutDot(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (name.IndexOf('.') > 0)
            {
                return Path.GetExtension(name).Remove(0, 1);
            }
            return name;
        }

        /// <summary>
        /// Ensures that the fileName ends with the specified extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        public static string EnsureExtension(string fileName, string extension)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentNullException("extension");
            }
            var ext = extension.StartsWith(".") ? extension : "." + extension;
            if (!fileName.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))
            {
                return fileName + ext;
            }
            return fileName;
        }
    }
}