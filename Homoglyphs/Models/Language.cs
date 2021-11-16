// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using Homoglyphs.Helpers;

namespace Homoglyphs.Models;

/// <summary>
/// A class to represent a Language.
/// </summary>
public class Language
{
    /// <summary>
    /// Gets the list of characters in this language's alphabet.
    /// </summary>
    public IEnumerable<char> AlphabetChars { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    /// <param name="languageCode">The language code to populate this language's alphabet with.</param>
    public Language(string languageCode)
    {
        this.AlphabetChars = LanguageHelper.GetAlphabet(languageCode);
    }

    /// <summary>
    /// Checks to see if the given character appears in this language's alphabet.
    /// </summary>
    /// <param name="c">The character to check for.</param>
    /// <returns>If the character appears in this language's alphabet.</returns>
    public bool IsInAlphabet(char c)
    {
        return this.AlphabetChars.Contains(c);
    }
}