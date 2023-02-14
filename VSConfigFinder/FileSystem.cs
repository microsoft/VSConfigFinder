namespace VSConfigFinder
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text.Json;

    public class FileSystem : IFileSystem
    {
        /// <inheritdoc/>
        public string[] GetFileSystemEntries(string path, string pattern, bool recursive = false)
        {
            Utilities.ValidateIsNotNullOrEmpty(path, nameof(path));
            Utilities.ValidateIsNotNullOrEmpty(pattern, nameof(pattern));

            return Directory.GetFileSystemEntries(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        /// <inheritdoc/>
        public Stream OpenFile(string path)
        {
            Utilities.ValidateIsNotNullOrEmpty(path, nameof(Path));

            return File.OpenRead(path);
        }
    }
}