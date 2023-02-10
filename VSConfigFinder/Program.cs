// <copyright file="Program.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using CommandLine;
    using System.IO;

    /// <summary>
    /// <see cref="Program"/> class for the .vsconfig finder tool.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the tool.
        /// </summary>
        /// <param name="args">The program arguments</param>
        public static void Main(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.CaseSensitive = false;
            });

            // Take in the command line arguments
            parser.ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void Run(CommandLineOptions options)
        {
            Console.WriteLine("Hello! I succeeded!");
            Console.WriteLine($"--createFile: {options.CreateFile}");
            Console.WriteLine($"--configOutputPath: {options.ConfigOutputPath}");

            if (options.CreateFile)
            {
                options.ConfigOutputPath ??= Directory.GetCurrentDirectory();
            }
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Oops, failed");
        }
    }
}