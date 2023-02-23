// <copyright file="Utilities.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// Utilities class for the tool.
    /// </summary>
    public class Utilities
    {
        private static readonly string ConfigExtension = ".vsconfig";
        private static readonly string Add = "--add";

        /// <summary>
        /// Validate whether the parameter is null or empty.
        /// </summary>
        /// <param name="s">The parameter.</param>
        /// <param name="paramName">The nameof(parameter).</param>
        public static void ValidateIsNotNullOrEmpty([NotNull] string s, string paramName)
        {
            IsNotNull(s, paramName);
            IsNotEmpty(s, paramName);
        }

        /// <summary>
        /// Validate whether the parameter is null.
        /// </summary>
        /// <param name="o">The parameter object.</param>
        /// <param name="paramName">The nameof(parameter).</param>
        /// <exception cref="ArgumentNullException"><paramref name="o"/> is null.</exception>
        public static void IsNotNull([NotNull] object o, string paramName)
        {
            if (o is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validate whether the parameter is empty.
        /// </summary>
        /// <param name="s">The string parameter object.</param>
        /// <param name="paramName">The nameof(parameter).</param>
        /// <exception cref="ArgumentException"><paramref name="s"/> is empty.</exception>
        public static void IsNotEmpty([NotNull] string s, string paramName)
        {
            if (s == string.Empty)
            {
                throw new ArgumentException("The string is empty.", paramName);
            }
        }

        /// <summary>
        /// Create an output from the final <see cref="VSConfig"/> and given <see cref="CommandLineOptions"/>.
        /// </summary>
        /// <param name="fileSystem">The <see cref="IFileSystem"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="finalConfig">The final <see cref="VSConfig"/> to export.</param>
        /// <param name="options">The command line options.</param>
        public static void CreateOutput(IFileSystem fileSystem, ILogger logger, VSConfig finalConfig, CommandLineOptions options)
        {
            if (options.CreateFile)
            {
                // Create a file
                var jsonString = JsonSerializer.Serialize(finalConfig, typeof(VSConfig), SourceGenerationContext.Default);
                var outputPath = Path.Combine(options.ConfigOutputPath!, ConfigExtension);

                fileSystem.WriteAllText(outputPath, jsonString);
                logger.Log($"Successfully created the final .vsconfig at {outputPath}");
            }
            else
            {
                // output to a command line
                var output = CreateCommandLineOutput(finalConfig);
                logger.Log(output);
            }
        }

        /// <summary>
        /// Read all the components in the nested .vsconfigs recursively, starting from the given path. 
        /// </summary>
        /// <param name="fileSystem">The <see cref="IFileSystem"/>.</param>
        /// <param name="options">The command line options.</param>
        /// <returns></returns>
        public static string[] ReadComponents(IFileSystem fileSystem, CommandLineOptions options)
        {
            var pathsToVsConfigs = fileSystem.GetFileSystemEntries(options.FolderPath!, "*" + ConfigExtension, recursive: true);

            var componentsSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
            };
            var context = new SourceGenerationContext(serializerOptions);

            foreach (var path in pathsToVsConfigs)
            {
                string[]? components;
                using (var stream = fileSystem.OpenFile(path))
                {
                    var config = JsonSerializer.Deserialize(stream, typeof(VSConfig), context);
                    if (config is VSConfig vsconfig)
                    {
                        components = vsconfig.Components;
                    }
                    else
                    {
                        throw new ArgumentException("Failed to read components. Please make sure the .vsconfig file input is in the correct format.");
                    }
                }

                if (components is not null)
                {
                    componentsSet.UnionWith(components);
                }
            };

            return componentsSet.ToArray();
        }

        private static string CreateCommandLineOutput(VSConfig finalConfig)
        {
            var output = new StringBuilder();

            if (finalConfig.Components is not null)
            {
                foreach (var component in finalConfig.Components)
                {
                    if (!string.IsNullOrEmpty(component))
                    {
                        output.AppendFormat("{0} {1} ", Add, component);
                    }
                }
            }

            return output.ToString().TrimEnd();
        }
    }
}