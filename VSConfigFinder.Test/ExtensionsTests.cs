// <copyright file="ExtensionsTests.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder.Test
{
    using Moq;
    using Xunit;

    public class ExtensionsTests
    {
        [Fact]
        public void GetFileSystemEntries_MultiplePaths_ReturnSet()
        {
            var path1 = "C:\\path1";
            var subpath1 = Path.Combine(path1, "subpath1", ".vsconfig");
            var subpath2 = Path.Combine(path1, "subpath2", "something.vsconfig");
            var path2 = "C:\\path2";
            var subpath3 = Path.Combine(path2, "subpath3", ".vsconfig");
            var subpath4 = Path.Combine(path2, "subpath4", "something.vsconfig");
            var path3 = path2;
            var subpath5 = subpath3;
            var subpath6 = subpath4;

            var fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(x => x.GetFileSystemEntries(path1, "*.vsconfig", true)).Returns(new[] { subpath1, subpath2 });
            fileSystem.Setup(x => x.GetFileSystemEntries(path2, "*.vsconfig", true)).Returns(new[] { subpath3, subpath4 });
            fileSystem.Setup(x => x.GetFileSystemEntries(path3, "*.vsconfig", true)).Returns(new[] { subpath5, subpath6 });

            var paths = new[] { path1, path2, path3 };

            var result = Extensions.GetFileSystemEntries(fileSystem.Object, paths, "*.vsconfig", recursive: true);

            Assert.Equal(4, result.Count());
            Assert.Contains(subpath1, result);
            Assert.Contains(subpath2, result);
            Assert.Contains(subpath3, result);
            Assert.Contains(subpath4, result);
        }
    }
}
