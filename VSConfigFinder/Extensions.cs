// <copyright file="Extensions.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions class for the tool.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The extension method to get entries from multiple paths.
        /// </summary>
        /// <param name="fileSystem">The <see cref="IFileSystem"/>.</param>
        /// <param name="paths">The list of top-level paths.</param>
        /// <param name="pattern">pattern to match for file names. To match anything and everything, specify '*'</param>
        /// <param name="recursive">Optional: recursively search sub directories</param>
        /// <returns></returns>
        public static IEnumerable<string> GetFileSystemEntries(this IFileSystem fileSystem, IEnumerable<string> paths, string pattern, bool recursive = false)
        {
            Utilities.IsNotNull(paths, nameof(paths));
            Utilities.ValidateIsNotNullOrEmpty(pattern, nameof(pattern));

            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var path in paths)
            {
                result.UnionWith(fileSystem.GetFileSystemEntries(path, pattern, recursive));
            }

            return result;
        }
    }
}
