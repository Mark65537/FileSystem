using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;


namespace FileSystem
{
    public class FileSystem
    {
        /// <summary>
        /// Extracts the contents of a zip file to a folder.
        /// </summary>
        /// <param name="pathFrom">The path to the zip file.</param>
        /// <param name="pathTo">The path to the folder where the contents of the zip file will be extracted to.</param
        public static void ExtractToFolder(string pathFrom, string pathTo)
        {
            if (!Directory.Exists(pathTo))
            {
                ZipFile.ExtractToDirectory(pathFrom, pathTo);
            }
        }
        public static bool IsImage(string filePath)
        {
            try
            {
                using (var image = Image.FromFile(filePath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="FromDir">From dir.</param>
        /// <param name="ToDir">To dir.</param>
        public static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }

        //find check it file or folder.
        // Source: https://stackoverflow.com/a/19596821 .
        /// <summary>
        /// Determines whether the specified path is a directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <c>true</c> if the specified path is a directory; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        /// <remarks>
        /// <para>
        /// If the path exists, then the result is determined by whether the path is a directory or a file.
        public static bool IsPathDirectory(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            path = path.Trim();

            if (Directory.Exists(path))
                return true;

            if (File.Exists(path))
                return false;

            // neither file nor directory exists. guess intention

            // if has trailing slash then it's a directory
            if (new[] { "\\", "/" }.Any(x => path.EndsWith(x)))
                return true; // ends with slash

            // if has extension then its a file; directory otherwise
            return string.IsNullOrWhiteSpace(Path.GetExtension(path));
        }
    }
}
