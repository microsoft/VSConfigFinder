// <copyright file="Utilities.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Text.Json;

    public class Utilities
    {
        private static readonly string ConfigExtension = ".vsconfig";
        private static readonly string Add = "--add";

        public static void ValidateIsNotNullOrEmpty([NotNull] string s, string paramName)
        {
            IsNotNull(s, paramName);
            IsNotEmpty(s, paramName);
        }

        public static void IsNotNull([NotNull] object o, string paramName)
        {
            if (o is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsNotEmpty([NotNull] string s, string paramName)
        {
            if (s == string.Empty)
            {
                throw new ArgumentException("The string is empty.", paramName);
            }
        }

        public static void CreateOutput(VSConfig finalConfig, CommandLineOptions options)
        {
            if (options.CreateFile)
            {
                // Create a file
                var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(finalConfig, serializerOptions);
                var outputPath = Path.Combine(options.ConfigOutputPath!, ConfigExtension);

                File.WriteAllText(outputPath, jsonString);
                Console.WriteLine($"Successfully created the final .vsconfig at {outputPath}");
            }
            else
            {
                // output to a command line
                var output = CreateCommandLineOutput(finalConfig);
                Console.WriteLine(output);
            }
        }

        private static string CreateCommandLineOutput(VSConfig finalConfig)
        {
            var output = new StringBuilder(Add + " ");

            foreach (var component in finalConfig.Components)
            {
                if (!string.IsNullOrEmpty(component))
                {
                    output.AppendFormat("{0} ", component);
                }
            }

            return output.ToString();
        }
    }
}
