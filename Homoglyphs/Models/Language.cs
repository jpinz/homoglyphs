// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

namespace Homoglyphs.Models;

public class Language
{
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

    private IEnumerable<char> _alphabet;

    public Language(string language)
    {
        this._alphabet = GetAlphabet(language);
    }

    public IEnumerable<char> AlphabetChars => this._alphabet;

    public bool IsInAlphabet(char c)
    {
        return this._alphabet.Contains(c);
    }

    public static IEnumerable<char> GetAlphabet(string language)
    {
        return LanguageDictionary.TryGetValue(language, out var alphabet)
            ? alphabet.ToCharArray()
            : throw new InvalidOperationException($"{language} is not a valid language!");
    }

    public static IEnumerable<string> DetectLanguage(char c)
    {
        foreach (var (name, alphabet) in LanguageDictionary)
        {
            if (alphabet.Contains(c))
            {
                yield return name;
            }
        }
    }
}