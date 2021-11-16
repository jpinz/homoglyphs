// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

namespace Homoglyphs.Models.Enums;

/// <summary>
/// This enum represents the strategic options we can use for what to do if the char isn't in the alphabet.
/// </summary>
public enum Strategy
{
    /// <summary>
    /// Load the category for this char if it isn't loaded, but is available.
    /// </summary>
    STRATEGY_LOAD,

    /// <summary>
    /// Add the char to the result, but do nothing special.
    /// </summary>
    STRATEGY_IGNORE,

    /// <summary>
    /// Remove the char from the result.
    /// </summary>
    STRATEGY_REMOVE,
}