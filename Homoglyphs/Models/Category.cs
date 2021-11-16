// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using Homoglyphs.Models.Enums;
using Newtonsoft.Json;

namespace Homoglyphs.Models
{
    public class Category
    {
        private readonly Dictionary<Alphabet, IEnumerable<(int, int)>> categoriesDictionary;
        private readonly Alphabet? _alphabet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// The default <see cref="Category"/> object contains all categories.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the json file cannot be read.</exception>
        public Category()
        {
            var categoriesList =
                JsonConvert.DeserializeObject<List<CategoryModel>>(File.ReadAllText(@".\categories.json")) ??
                throw new InvalidOperationException();
            this.categoriesDictionary = categoriesList
                .GroupBy(category => category.Alphabet)
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
        /// <param name="alphabet">The given alphabet to populate this category with.</param>
        /// <exception cref="InvalidOperationException">Thrown if the json file cannot be read.</exception>
        public Category(Alphabet alphabet)
        {
            this._alphabet = alphabet;
            var categoriesList =
                JsonConvert.DeserializeObject<List<CategoryModel>>(File.ReadAllText(@".\categories.json")) ??
                throw new InvalidOperationException();
            this.categoriesDictionary = categoriesList
                .GroupBy(category => category.Alphabet)
                .Where(c => c.Key == alphabet)
                .ToDictionary(
                    category => category.Key,
                    categoryGrouping =>
                    {
                        return categoryGrouping.Select(category => (category.StartCode, category.EndCode));
                    });
        }

        /// <summary>
        /// Gets the list of code ranges for a given alphabet.
        /// </summary>
        /// <param name="alphabet">The alphabet to get all the code ranges for.</param>
        /// <returns>A list of code ranges for this alphabet.</returns>
        public IEnumerable<(int StartCode, int EndCode)>? GetRanges(Alphabet alphabet)
        {
            return this.categoriesDictionary.TryGetValue(alphabet, out var codes) ? codes : null;
        }

        /// <summary>
        /// Gets the list of characters representing an alphabet.
        /// </summary>
        /// <param name="alphabet">The alphabet to get all the characters for.</param>
        /// <returns>A list of characters for this alphabet.</returns>
        public IEnumerable<char>? GetAlphabetChars(Alphabet alphabet)
        {
            var ranges = this.GetRanges(alphabet);
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
            return this.GetAlphabetChars(this._alphabet ?? throw new InvalidOperationException());
        }

        public (Alphabet? Alphabet, string? BlockName) Detect(char c)
        {
            string? name = DetectName(c);
            Alphabet? alphabet = DetectAlphabet(c);

            return (alphabet, name);
        }

        private string? DetectName(char c)
        {
            foreach (var blockName in AlphabetBlockNames.Names)
            {
                string pattern = $@"\p{{{blockName}}}+";
                if (Regex.IsMatch(c.ToString(), pattern))
                {
                    return blockName;
                }
            }

            return null;
        }

        public Alphabet? DetectAlphabet(char c)
        {
            foreach (var (alphabet, ranges) in this.categoriesDictionary)
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
    }
}