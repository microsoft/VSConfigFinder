// <copyright file="UtilitiesTests.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder.Test
{
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
    }
}