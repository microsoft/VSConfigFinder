// <copyright file="UtilitiesTests.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder.Test
{
    using Moq;
    using System.Text;
    using Xunit;

    public class UtilitiesTests
    {
        const string VSConfig = ".vsconfig";

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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateOutput_Creates_FileOrArguments_With_Expected_String(bool createFile)
        {
            var fileSystem = new Mock<IFileSystem>();
            var logger = new Mock<ILogger>();

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
                FolderPath = new[] { "C:\\input" },
                CreateFile = createFile,
                ConfigOutputPath = "C:\\output",
            };

            Utilities.CreateOutput(fileSystem.Object, logger.Object, finalConfig, options);

            if (createFile)
            {
                var outputPath = Path.Combine(options.ConfigOutputPath, VSConfig);
                fileSystem.Verify(x => x.WriteAllText(outputPath, jsonString));
            }
            else
            {
                var addArguments = "--add Microsoft.VisualStudio.Component.NuGet --add Microsoft.VisualStudio.Component.Roslyn.Compiler --add Microsoft.Component.MSBuild --add Microsoft.NetCore.Component.Runtime.6.0";
                logger.Verify(x => x.Log(addArguments));
            }
        }

        [Fact]
        public void ReadComponents_Reads_AllNestedDirectories_And_OutputsAllComponents()
        {
            /*
             * folder structure:
             * pathA
             *   - .vsconfig
             *   - pathB
             *      - .vsconfig
             */

            var fileSystem = new Mock<IFileSystem>();

            var options = new CommandLineOptions
            {
                FolderPath = new[] { "C:\\pathA" },
                ConfigOutputPath = "C:\\output",
            };

            // pathA
            var pathA = "C:\\pathA";
            var pathAConfigFile = Path.Combine(pathA, VSConfig);

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
            var pathB = Path.Combine(pathA, "pathB");
            var pathBConfigFile = Path.Combine(pathB, VSConfig);

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

            fileSystem.Setup(x => x.GetFileSystemEntries("C:\\pathA", "*" + VSConfig, true)).Returns(new[] { pathAConfigFile, pathBConfigFile });

            fileSystem.Setup(x => x.OpenFile(pathAConfigFile)).Returns(pathAReader);
            fileSystem.Setup(x => x.OpenFile(pathBConfigFile)).Returns(pathBReader);

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