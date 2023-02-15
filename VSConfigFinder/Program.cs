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
            
            parser.ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void Run(CommandLineOptions options)
        {
            var fileSystem = new FileSystem();
            var logger = new ConsoleLogger();

            ResolveCommandLineOptions(options);

            var finalConfig = new VSConfig()
            {
                // This is the new final .vsconfig, so version 1.0 is used.
                Version = new Version("1.0"),
                Components = Utilities.ReadComponents(fileSystem, options),
            };

            Utilities.CreateOutput(fileSystem, logger, finalConfig, options);
        }

        private static void ResolveCommandLineOptions(CommandLineOptions options)
        {
            if (options.CreateFile)
            {
                options.ConfigOutputPath ??= Directory.GetCurrentDirectory();
            }
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Please make sure that you have provided the correct arguments. Try --help to see all the available arguments and explanations.");
        }
    }
}