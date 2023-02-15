// <copyright file="FileSystem.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System.IO;

    /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void WriteAllText(string path, string data)
        {
            Utilities.ValidateIsNotNullOrEmpty(path, nameof(Path));

            File.WriteAllText(path, data);
        }
    }
}