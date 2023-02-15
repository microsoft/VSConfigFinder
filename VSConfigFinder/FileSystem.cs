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
        public IEnumerable<string> GetFileSystemEntries(IEnumerable<string> paths, string pattern, bool recursive = false)
        {
            Utilities.IsNotNull(paths, nameof(paths));
            Utilities.ValidateIsNotNullOrEmpty(pattern, nameof(pattern));

            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            foreach (var path in paths)
            {
                result.UnionWith(Directory.GetFileSystemEntries(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            }

            return result;
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