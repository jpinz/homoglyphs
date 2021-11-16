// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using Homoglyphs.Models;

namespace Homoglyphs.Helpers;

/// <summary>
/// The helper class for <see cref="Language"/>.
/// </summary>
public static class LanguageHelper
{
    /// <summary>
    /// Gets the alphabet for the given language (if it exists).
    /// </summary>
    /// <param name="languageCode">The ISO 639-1 code for the language to get the alphabet for.</param>
    /// <returns>A list of the characters in the language's alphabet.</returns>
    /// <exception cref="InvalidOperationException">The language doesn't exist, or we don't support it.</exception>
    public static IEnumerable<char> GetAlphabet(string languageCode)
    {
        return LanguageDictionary.TryGetValue(languageCode.ToUpperInvariant(), out var alphabet)
            ? alphabet.ToCharArray()
            : throw new InvalidOperationException($"{languageCode.ToUpperInvariant()} is not a valid/supported language!");
    }

    /// <summary>
    /// Finds the language(s) that the given character appears in.
    /// </summary>
    /// <param name="c">The character to find what language(s) it appears in their alphabet.</param>
    /// <returns>A list of the ISO 639-1 codes for the language(s) this character appears in.</returns>
    public static IEnumerable<string> DetectLanguage(char c)
    {
        foreach (var (code, alphabet) in LanguageDictionary)
        {
            if (alphabet.Contains(c))
            {
                yield return code;
            }
        }
    }

    /// <summary>
    /// This is the dictionary for all the languages we currently support and their alphabet of characters.
    /// The key is the ISO 639-1 code of the language, and the value is the alphabet.
    /// </summary>
    private static readonly IDictionary<string, string> LanguageDictionary = new Dictionary<string, string>()
    {
        { "AR", @"ءآأؤإئابةتثجحخدذرزسشصضطظعغػؼؽؾؿـفقكلمنهوىيًٌٍَُِّ" },
        { "BE", @"ʼЁІЎАБВГДЕЖЗЙКЛМНОПРСТУФХЦЧШЫЬЭЮЯабвгдежзйклмнопрстуфхцчшыьэюяёіў" },
        { "BG", @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЮЯабвгдежзийклмнопрстуфхцчшщъьюя" },
        { "CA", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÈÉÍÏÒÓÚÜÇàèéíïòóúüç·" },
        { "CZ", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÁÉÍÓÚÝáéíóúýČčĎďĚěŇňŘřŠšŤťŮůŽž" },
        { "DA", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÅÆØåæø" },
        { "DE", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÄÖÜßäöü" },
        { "EL", @"ΪΫΆΈΉΊΌΎΏΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩΐΰϊϋάέήίαβγδεζηθικλμνξοπρςστυφχψωόύώ" },
        { "EN", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" },
        { "EO", @"ABCDEFGHIJKLMNOPRSTUVZabcdefghijklmnoprstuvzĈĉĜĝĤĥĴĵŜŝŬŭ" },
        { "ES", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÁÉÍÑÓÚÜáéíñóúü" },
        { "ET", @"ABDEGHIJKLMNOPRSTUVabdeghijklmnoprstuvÄÕÖÜäõöü" },
        { "FI", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÄÅÖäåöŠšŽž" },
        { "FR", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÂÇÈÉÊÎÏÙÛàâçèéêîïùûŒœ" },
        { "HE", @"אבגדהוזחטיךכלםמןנסעףפץצקרשתװױײ" },
        { "HR", @"ABCDEFGHIJKLMNOPRSTUVZabcdefghijklmnoprstuvzĆćČčĐđŠšŽž" },
        { "HU", @"ABCDEFGHIJKLMNOPRSTUVZabcdefghijklmnoprstuvzÁÉÍÓÖÚÜáéíóöúüŐőŰű" },
        { "IT", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÈÉÌÒÓÙàèéìòóù" },
        { "LT", @"ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyzĄąČčĖėĘęĮįŠšŪūŲųŽž" },
        { "LV", @"ABCDEFGHIJKLMNOPRSTUVZabcdefghijklmnoprstuvzĀāČčĒēĢģĪīĶķĻļŅņŠšŪūŽž" },
        { "MK", @"ЃЅЈЉЊЌЏАБВГДЕЖЗИКЛМНОПРСТУФХЦЧШабвгдежзиклмнопрстуфхцчшѓѕјљњќџ" },
        { "NL", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" },
        { "PL", @"ABCDEFGHIJKLMNOPRSTUWYZabcdefghijklmnoprstuwyzÓóĄąĆćĘęŁłŃńŚśŹźŻż" },
        { "PT", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÁÂÃÇÉÊÍÓÔÕÚàáâãçéêíóôõú" },
        { "RO", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÂÎâîĂăȘșȚț" },
        { "RU", @"ЁАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюяё" },
        { "SK", @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÁÄÉÍÓÔÚÝáäéíóôúýČčĎďĹĺĽľŇňŔŕŠšŤťŽž" },
        { "SL", @"ABCDEFGHIJKLMNOPRSTUVZabcdefghijklmnoprstuvzČčŠšŽž" },
        {
            "SR",
            @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzЂЈЉЊЋЏАБВГДЕЖЗИКЛМНОПРСТУФХЦЧШабвгдежзиклмнопрстуфхцчшђјљњћџ"
        },
        { "TH", @"กขฃคฅฆงจฉชซฌญฎฏฐฑฒณดตถทธนบปผฝพฟภมยรฤลฦวศษสหฬอฮฯะัาำิีึืฺุู฿เแโใไๅๆ็่้๊๋์ํ๎๏๐๑๒๓๔๕๖๗๘๙๚๛" },
        { "TR", @"ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyzÂÇÎÖÛÜâçîöûüĞğİıŞş" },
        { "VI", @"ABCDEGHIKLMNOPQRSTUVXYabcdeghiklmnopqrstuvxyÂÊÔâêôĂăĐđƠơƯư" },
    };
}