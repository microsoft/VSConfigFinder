// <copyright file="ILogger.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    /// <summary>
    /// The interface to log user messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log the message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message);
    }
}
