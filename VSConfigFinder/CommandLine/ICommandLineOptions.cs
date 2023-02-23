// <copyright file="ICommandLineOptions.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    /// <summary>
    /// The interface for the command line options.
    /// </summary>
    public interface ICommandLineOptions
    {
        /// <summary>
        /// Gets or sets the folder paths to be used as the root (starting point) of the search.
        /// </summary>
        IEnumerable<string>? FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the output gets created as a consolidated .vsconfig file instead of the Visual Studio Installer setup command line arguments.
        /// If --createFile is passed in, <see cref="ConfigOutputPath"/> can also be passed in to indicate the output directory.folder path to output the consolidated .vsconfig instead of the command line arguments.
        /// </summary>
        bool CreateFile { get; set; }

        /// <summary>
        /// Gets or sets the optional folder path to use if --createFile is passed in. 
        /// If empty or null, uses the current directory as the output path.
        /// This can only be used in conjunction with --createFile. If passed in without --createFile, the parameter will be ignored.
        /// </summary>
        string? ConfigOutputPath { get; set; }
    }
}
