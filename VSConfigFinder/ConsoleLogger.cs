// <copyright file="ConsoleLogger.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System;

    /// <inheritdoc/>
    internal class ConsoleLogger : ILogger
    {
        /// <inheritdoc/>
        public void Log(string message) => Console.WriteLine(message);
    }
}
