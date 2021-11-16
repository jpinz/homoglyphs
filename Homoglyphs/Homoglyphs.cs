using Homoglyphs.Helpers;
using Homoglyphs.Models;
using Homoglyphs.Models.Enums;
using Newtonsoft.Json;

namespace Homoglyphs;

public class Homoglyphs
{
    public IDictionary<string, IEnumerable<string>> table;

    private List<char> _alphabetChars = new();
    private IList<Language>? languagesList;
    private IList<Category>? categoriesList;
    private Strategy _strategy;

    public Homoglyphs(
        Strategy strategy = Strategy.STRATEGY_IGNORE,
        IList<Language>? languagesList = null,
        IList<Category>? categoriesList = null)
    {
        this._strategy = strategy;
        this.GenerateAlphabet(languagesList, categoriesList);
        this.table = GetTable();
    }

    public IEnumerable<string> GetCharVariants(char c)
    {
        if (!this._alphabetChars.Contains(c))
        {
            switch (_strategy)
            {
                case Strategy.STRATEGY_LOAD:
                    // Load
                    break;
                case Strategy.STRATEGY_IGNORE:
                    // Ignore
                    return new[] { c.ToString() };
                case Strategy.STRATEGY_REMOVE:
                    // Remove
                    return new List<string>();
                default:
                    throw new InvalidOperationException();
            }
        }


        var variants = this.table.TryGetValue(c.ToString(), out var v) ? v.ToList() : new List<string>();
        if (variants.Any())
        {
            foreach (var variant in variants)
            {
                var secondaryVariants = this.table.TryGetValue(variant, out var v2) ? v2.ToList() : new List<string>();
                variants.AddRange(secondaryVariants);
            }
        }

        variants.Add(c.ToString());

        return GetUniqueAndSorted(variants);
    }

    private bool UpdateAlphabet(char c)
    {
        var langs = LanguageHelper.DetectLanguage(c).ToList();
        if (langs.Any())
        {
            foreach (var lang in langs)
            {
                this.languagesList?.Add(new Language(lang));
                this._alphabetChars.AddRange(LanguageHelper.GetAlphabet(lang));
            }
        }
        else
        {
            var category = new Category();
            var catAlphabet = category.DetectAlphabet(c);
            if (catAlphabet == null)
            {
                return false;
            }

            this.categoriesList?.Add(new Category((Alphabet)catAlphabet));
            this._alphabetChars.AddRange(category.GetAlphabetChars((Alphabet)catAlphabet) ?? Array.Empty<char>());
        }

        this.table = GetTable(this._alphabetChars);

        return true;
    }

    private void GenerateAlphabet(IList<Language>? languagesList = null, IList<Category>? categoriesList = null)
    {
        if (categoriesList == null || !categoriesList.Any())
        {
            categoriesList = new List<Category>()
            {
                new(Alphabet.COMMON),
                new(Alphabet.LATIN)
            };
        }

        foreach (var category in categoriesList)
        {
            this._alphabetChars.AddRange(category.GetAlphabetChars() ?? Array.Empty<char>());
        }

        if (languagesList != null && languagesList.Any())
        {
            foreach (var language in languagesList)
            {
                this._alphabetChars.AddRange(language.AlphabetChars);
            }
        }

        this.categoriesList = categoriesList;
        this.languagesList = languagesList ?? new List<Language>();
    }

    private static IList<string> GetUniqueAndSorted(IEnumerable<string> list)
    {
        return list.Distinct().OrderBy(q => q).ToList();
    }

    private static IDictionary<string, IEnumerable<string>> GetTable(IList<char>? alphabet = null)
    {
        var confusables = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(
                       File.ReadAllText(@".\confusables.json")) ??
                   throw new InvalidOperationException();
        if (alphabet == null || !alphabet.Any())
        {
            // If the alphabet is empty, just return the entire table.
            return confusables;
        }

        var dict = new Dictionary<string, IEnumerable<string>>();
        foreach (var c in alphabet)
        {
            var cString = c.ToString();
            if (confusables.TryGetValue(cString, out var variants))
            {
                foreach (var v in variants)
                {
                    if (alphabet.Contains(char.Parse(v)))
                    {
                        if (dict.ContainsKey(cString))
                        {
                            // Add the new variant if this character has already been seen.
                            dict[cString].ToList().Add(v);
                        }
                        else
                        {
                            // Add the new variant to the dictionary.
                            dict[cString] = new List<string> { v };
                        }
                    }
                }
            }
        }

        return dict;
    }
}