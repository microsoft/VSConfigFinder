// <copyright file="UtilitiesTests.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder.Test
{
    using Moq;
    using System.Text;
    using System.Text.Json;
    using Xunit;

    public class UtilitiesTests
    {
        [Fact]
        public void ValidateIsNotNullOrEmpty_NullOrEmpty_String_Throws_AppropriateException()
        {
            string? nullStr = null;
            Assert.Throws<ArgumentNullException>(() => Utilities.ValidateIsNotNullOrEmpty(nullStr!, nameof(nullStr)));

            var emptyStr = string.Empty;
            Assert.Throws<ArgumentException>(() => Utilities.ValidateIsNotNullOrEmpty(emptyStr, nameof(emptyStr)));
        }

        [Fact]
        public void ValidateIsNotNullOrEmpty_NotNullOrEmpty_String_Succeeds()
        {
            var str = "some string";
            Utilities.ValidateIsNotNullOrEmpty(str, nameof(str));
        }

        [Fact]
        public void CreateOutput_Creates_File_With_Expected_String()
        {
            var fileSystem = new Mock<IFileSystem>();
            var finalConfig = new VSConfig
            {
                Version = new Version("1.0"),
                Components = new[]
                {
                    "Microsoft.VisualStudio.Component.NuGet",
                    "Microsoft.VisualStudio.Component.Roslyn.Compiler",
                    "Microsoft.Component.MSBuild",
                    "Microsoft.NetCore.Component.Runtime.6.0"
                },
            };

            var jsonString = """
                {
                  "Version": "1.0",
                  "Components": [
                    "Microsoft.VisualStudio.Component.NuGet",
                    "Microsoft.VisualStudio.Component.Roslyn.Compiler",
                    "Microsoft.Component.MSBuild",
                    "Microsoft.NetCore.Component.Runtime.6.0"
                  ]
                }
                """;

            var options = new CommandLineOptions
            {
                FolderPath = "C:\\input",
                CreateFile = true,
                ConfigOutputPath = "C:\\output",
            };

            Utilities.CreateOutput(fileSystem.Object, finalConfig, options);

            var outputPath = Path.Combine(options.ConfigOutputPath, ".vsconfig");
            fileSystem.Verify(x => x.WriteAllText(outputPath, jsonString));
        }

        [Fact]
        public void ReadComponents_Reads_AllNestedDirectories_And_OutputsAllComponents()
        {
            var fileSystem = new Mock<IFileSystem>();

            var options = new CommandLineOptions
            {
                FolderPath = "C:\\input",
                ConfigOutputPath = "C:\\output",
            };

            // pathA
            var pathA = "C:\\pathA";
            var pathAConfig = """
                {
                  "Version": "1.0",
                  "Components": [
                    "Microsoft.VisualStudio.Component.NuGet",
                    "Microsoft.Component.MSBuild",
                  ]
                }
                """;
            var pathAReader = new MemoryStream(Encoding.UTF8.GetBytes(pathAConfig));

            // pathB
            var pathB = "C:\\pathB";
            var pathBConfig = """
                {
                  "Version": "1.0",
                  "Components": [
                    "Microsoft.VisualStudio.Component.NuGet",
                    "Microsoft.VisualStudio.Component.Roslyn.Compiler",
                  ]
                }
                """;
            var pathBReader = new MemoryStream(Encoding.UTF8.GetBytes(pathBConfig));

            fileSystem.Setup(x => x.GetFileSystemEntries(options.FolderPath, ".vsconfig", true)).Returns(new[] { pathA, pathB });

            fileSystem.Setup(x => x.OpenFile(pathA)).Returns(pathAReader);
            fileSystem.Setup(x => x.OpenFile(pathB)).Returns(pathBReader);

            var components = Utilities.ReadComponents(fileSystem.Object, options);
            Assert.Equal(3, components.Length);
            Assert.Collection(
                components,
                x => Assert.Equal("Microsoft.VisualStudio.Component.NuGet", x),
                x => Assert.Equal("Microsoft.Component.MSBuild", x),
                x => Assert.Equal("Microsoft.VisualStudio.Component.Roslyn.Compiler", x));
        }
    }
}