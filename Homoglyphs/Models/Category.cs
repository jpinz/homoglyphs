// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using Homoglyphs.Models.Enums;
using Newtonsoft.Json;

namespace Homoglyphs.Models
{
    /// <summary>
    /// A class to represent a Category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The dictionary for all of the categories.
        /// The key is the <see cref="AlphabetType"/>, and the value is a list of the start and end codes.
        /// </summary>
        private readonly Dictionary<AlphabetType, IEnumerable<(int StartCode, int EndCode)>> _categoriesDictionary;

        /// <summary>
        /// The type of alphabet this category is using.
        /// </summary>
        private readonly AlphabetType? _alphabetType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// The default <see cref="Category"/> object contains all categories.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the json file cannot be read.</exception>
        public Category()
        {
            // Gets the list of categories from the json file.
            var categoriesList =
                JsonConvert.DeserializeObject<List<CategoryModel>>(File.ReadAllText(@".\categories.json")) ??
                throw new InvalidOperationException();

            // Convert the list of categories to a dictionary.
            this._categoriesDictionary = categoriesList
                .GroupBy(category => category.AlphabetType)
                .ToDictionary(
                    category => category.Key,
                    categoryGrouping =>
                    {
                        return categoryGrouping.Select(category => (category.StartCode, category.EndCode));
                    });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// This <see cref="Category"/> constructor is only for a specific alphabet.
        /// </summary>
        /// <param name="alphabetType">The given alphabet to populate this category with.</param>
        /// <exception cref="InvalidOperationException">Thrown if the json file cannot be read.</exception>
        public Category(AlphabetType alphabetType)
        {
            this._alphabetType = alphabetType;

            // Gets the list of categories from the json file.
            var categoriesList =
                JsonConvert.DeserializeObject<List<CategoryModel>>(File.ReadAllText(@".\categories.json")) ??
                throw new InvalidOperationException();

            // Convert the list of categories to a dictionary.
            this._categoriesDictionary = categoriesList
                .GroupBy(category => category.AlphabetType)
                .Where(c => c.Key == alphabetType)
                .ToDictionary(
                    category => category.Key,
                    categoryGrouping =>
                    {
                        return categoryGrouping.Select(category => (category.StartCode, category.EndCode));
                    });
        }

        /// <summary>
        /// Gets the list of characters representing an alphabet.
        /// </summary>
        /// <param name="alphabetType">The alphabet to get all the characters for.</param>
        /// <returns>A list of characters for this alphabet.</returns>
        public IEnumerable<char>? GetAlphabetChars(AlphabetType alphabetType)
        {
            var ranges = this.GetRanges(alphabetType);
            if (ranges == null)
            {
                yield break;
            }

            foreach (var (startCode, endCode) in ranges)
            {
                for (int i = startCode; i < endCode + 1; i++)
                {
                    yield return (char)i;
                }
            }
        }

        /// <summary>
        /// Gets the list of characters representing this category's alphabet.
        /// </summary>
        /// <returns>A list of characters for this alphabet.</returns>
        public IEnumerable<char>? GetAlphabetChars()
        {
            return this.GetAlphabetChars(this._alphabetType ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// Returns a tuple containing the <see cref="AlphabetType"/> and the regex block name that this character is found in.
        /// </summary>
        /// <param name="c">The character to find these values for.</param>
        /// <returns>The tuple containing the <see cref="AlphabetType"/> and regex block name.</returns>
        public (AlphabetType? Alphabet, string? BlockName) Detect(char c)
        {
            string? name = DetectName(c);
            AlphabetType? alphabet = DetectAlphabet(c);

            return (alphabet, name);
        }

        /// <summary>
        /// Returns the <see cref="AlphabetType"/> that this character appears in. If one exists.
        /// </summary>
        /// <param name="c">The character to find the <see cref="AlphabetType"/> for.</param>
        /// <returns>The <see cref="AlphabetType"/> this character is in, or null if there isn't an <see cref="AlphabetType"/> that contains this char.</returns>
        public AlphabetType? DetectAlphabet(char c)
        {
            foreach (var (alphabet, ranges) in this._categoriesDictionary)
            {
                foreach (var (startCode, endCode) in ranges)
                {
                    if (startCode <= c && c <= endCode)
                    {
                        return alphabet;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the list of code ranges for a given alphabet.
        /// </summary>
        /// <param name="alphabetType">The alphabet to get all the code ranges for.</param>
        /// <returns>A list of code ranges for this alphabet.</returns>
        private IEnumerable<(int StartCode, int EndCode)>? GetRanges(AlphabetType alphabetType)
        {
            return this._categoriesDictionary.TryGetValue(alphabetType, out var codes) ? codes : null;
        }

        /// <summary>
        /// Returns the regex block name that the character shows up in.
        /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#unicode-category-or-unicode-block-p"/>
        /// </summary>
        /// <param name="c">The character to get the block name for.</param>
        /// <returns>The block name that returns true in regex for this character.</returns>
        private static string? DetectName(char c)
        {
            return (from blockName in AlphabetRegexNames.Names let pattern = $@"\p{{{blockName}}}+" where Regex.IsMatch(c.ToString(), pattern) select blockName).FirstOrDefault();
        }
    }
}