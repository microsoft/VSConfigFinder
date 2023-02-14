// <copyright file="VSConfig.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System;

    /// <summary>
    /// The class object that defines a .vsconfig file.
    /// </summary>
    public class VSConfig
    {
        /// <summary>
        /// Gets or sets the version of the .vsconfig file.
        /// </summary>
        public Version? Version { get; set; }

        /// <summary>
        /// Gets or sets the list of component ids.
        /// </summary>
        public string[]? Components { get; set; }
    }
}
