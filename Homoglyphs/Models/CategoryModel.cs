// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using Homoglyphs.Models.Enums;
using Newtonsoft.Json;

namespace Homoglyphs.Models;

public class CategoryModel
{
    [JsonProperty("Alphabet")]
    public Alphabet Alphabet { get; set; }

    [JsonProperty("Start")]
    public int StartCode { get; set; }

    [JsonProperty("End")]
    public int EndCode { get; set; }
}
