// <copyright file="CommandLineOptions.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using CommandLine;

    /// <inheritdoc/>
    public class CommandLineOptions : ICommandLineOptions
    {
        /// <inheritdoc/>
        [Option("folderpath", Required = true, HelpText = "The source folder path to use as the root. The search will start from the root towards the bottom.")]
        public string FolderPath { get; set; }

        /// <inheritdoc/>
        [Option("createfile", Required = false, Default = false, HelpText = "(Default: false) Bool flag that indicates whether the output gets created as a consolidated .vsconfig file instead of the Visual Studio Installer setup command line arguments.\n" +
            "If --createFile is passed in, --configOutputPath can also be passed in to indicate the output directory.")]
        public bool CreateFile { get; set; }

        /// <inheritdoc/>
        [Option("configoutputpath", Required = false, HelpText = "The optional folder path to use if --createFile is passed in. If empty or null, uses the current directory as the output path.\n" +
            "This can only be used in conjunction with --createFile. If passed in without --createFile, the parameter will be ignored.")]
        public string? ConfigOutputPath { get; set; }
    }
}
