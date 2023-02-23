// <copyright file="SourceGenerationContext.cs" company="Microsoft Corporation">
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.
// </copyright>

namespace VSConfigFinder
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Creates a <see cref="JsonSerializerContext"/> for serializing/deserializing .vsconfigs.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(VSConfig))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
