// <copyright file="IFileSystem.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    /// <summary>
    /// The interface for the <see cref="FileSystem"/>.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Get files and directories within directory.
        /// </summary>
        /// <param name="path">Directory path to search for.</param>
        /// <param name="pattern">Pattern to match for file names. To match anything and everything, specify '*'</param>
        /// <param name="recursive">Optional: recursively search sub directories</param>
        /// <returns>Array of path to files that match the pattern within the directory.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public IEnumerable<string> GetFileSystemEntries(string path, string pattern, bool recursive = false);

        /// <summary>
        /// Opens a file for reading.
        /// </summary>
        /// <param name="path">The path to a file to open.</param>
        /// <returns>A stream of the opened file.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public Stream OpenFile(string path);

        /// <summary>
        /// Writes all text in a path.
        /// </summary>
        /// <param name="path">The path to a file to write.</param>
        /// <param name="data">The string data to write.</param>
        /// <exception cref="ArgumentException"><paramref name="path"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public void WriteAllText(string path, string data);
    }
}
