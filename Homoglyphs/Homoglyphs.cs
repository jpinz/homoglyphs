using Homoglyphs.Models;
using Homoglyphs.Models.Enums;
using Newtonsoft.Json;

namespace Homoglyphs;

public class Homoglyphs
{
    public IDictionary<string, IEnumerable<string>> table;

    private IList<char> _alphabetChars = new List<char>();

    public Homoglyphs(List<Languages>? languagesList = null, List<Category>? categoriesList = null)
    {
        this.table = GetTable();
        this.GenerateAlphabet(languagesList, categoriesList);
    }

    public IEnumerable<string> GetCharVariants(char c)
    {
        var variants = this.table.TryGetValue(c.ToString(), out var v) ? v.ToList() : new List<string>();
        if (variants.Any())
        {
            foreach (var variant in variants)
            {
                var secondaryVariants = this.table.TryGetValue(variant, out var v2) ? v2.ToList() : new List<string>();
                variants = new List<string>(variants.Concat(secondaryVariants));
            }
        }

        variants.Add(c.ToString());

        return GetUniqueAndSorted(variants);
    }

    private void GenerateAlphabet(IList<Languages>? languagesList = null, IList<Category>? categoriesList = null)
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
            this._alphabetChars = new List<char>(this._alphabetChars.Concat(category.GetAlphabetChars() ?? Array.Empty<char>()));
        }
        if (languagesList != null && languagesList.Any())
        {
            foreach (var language in languagesList)
            {
                this._alphabetChars = new List<char>(this._alphabetChars.Concat(language.AlphabetChars));
            }
        }
    }

    private static IList<string> GetUniqueAndSorted(IEnumerable<string> list)
    {
        return list.Distinct().OrderBy(q => q).ToList();
    }

    private static IDictionary<string, IEnumerable<string>> GetTable()
    {
        return
            JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(File.ReadAllText(@".\confusables.json")) ??
            throw new InvalidOperationException();
    }
}