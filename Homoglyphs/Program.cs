// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using Homoglyphs.Models;
using Homoglyphs.Models.Enums;

namespace Homoglyphs;

public sealed class Program
{
    /// <summary>
    /// The default method for launching the CLI tool.
    /// </summary>
    /// <param name="args">Any arguments to be used with the CLI.</param>
    /// <returns>An integer representing the exit code of the CLI.</returns>
    public static int Main(string[] args)
    {
        var category = new Category();
        /*foreach (var c in categories.GetAlphabet(Alphabet.LATIN))
        {
            Console.WriteLine(c);
        }*/
        Console.WriteLine(category.Detect('Ж'));
        /*Console.WriteLine(categories.Detect('ต'));
        Console.WriteLine(categories.Detect('-'));
        Console.WriteLine(categories.Detect('↉'));
        Console.WriteLine(categories.Detect('C'));

        var languages = new Languages();

        foreach (var lang in languages.Detect('ต'))
        {
            Console.WriteLine("Thai: " + lang);
        }
        Console.WriteLine();
        foreach (var lang in languages.Detect('C'))
        {
            Console.WriteLine("C: " + lang);
        }*/

        // var homoglyphs = new Homoglyphs();
        /*foreach (var (c, homo) in homoglyphs.table)
        {
            foreach (var h in homo)
            {
                Console.WriteLine($"{c}: {h}");
            }
        }*/
        /*foreach (var variant in homoglyphs.GetCharVariants('.'))
        {
            Console.WriteLine(variant);
            foreach (char c in variant)
            {
                Console.Write("{0:X} ", Convert.ToUInt32(c));
            }
            Console.WriteLine();
        }*/

        return 0;
    }
}