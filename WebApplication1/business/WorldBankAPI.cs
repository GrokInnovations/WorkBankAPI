using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
//using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters    ;

/// <summary>
/// Summary description for WorldBankAPI
/// </summary>
public class WorldBankAPI
{
    public WorldBankAPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
//https://app.quicktype.io?share=Il2Z3H183sujV1sGB1QJ
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class Indicator
{


    [JsonProperty("indicator")]
    public indicator indicator { get; set; }

    public string CountryName { get { return Country.Value; } }

    [JsonProperty("country")]
    public Country Country { get; set; }

    [JsonProperty("countryiso3code")]
    public string Countryiso3Code { get; set; }

    [JsonProperty("date")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Date { get; set; }

    [JsonProperty("value")]
    public double? Value { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("obs_status")]
    public string ObsStatus { get; set; }

    [JsonProperty("decimal")]
    public long Decimal { get; set; }

   // public CountryGDP CountryGDP { get; set; }
   
}
public partial class indicator
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}



public partial class CountryGDP
{

    [JsonProperty("indicator")]
    public indicator indicator { get; set; }
    [JsonProperty("country")]
    public Country Country { get; set; }

    public string CountryName{get { return Country.Value; }}

    [JsonProperty("countryiso3code")]
    public string Countryiso3Code { get; set; }

    [JsonProperty("date")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Date { get; set; }

    [JsonProperty("value")]
    public double? Value { get ; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("obs_status")]
    private string ObsStatus { get; set; }

    [JsonProperty("decimal")]
    private long Decimal { get; set; }
}
public partial class Country
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class GDPPages
{
    [JsonProperty("page")]
    public long Page { get; set; }

    [JsonProperty("pages")]
    public long Pages { get; set; }

    [JsonProperty("per_page")]
    public long PerPage { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("sourceid")]
     public string  Sourceid { get; set; }
    //[JsonConverter(typeof(ParseStringConverter))]

    [JsonProperty("lastupdated")]
    public DateTimeOffset Lastupdated { get; set; }
}



public partial class GDPDATA
{
    public GDPPages GDPPageHeader;
    public List<Indicator> Indicators;

    public static implicit operator GDPDATA(GDPPages GDPPageHeader) => new GDPDATA { GDPPageHeader = GDPPageHeader };
    public static implicit operator GDPDATA(List<Indicator> IndicatorsList) =>  new GDPDATA { Indicators = IndicatorsList };
}

public class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}

public class GDPDATAConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(GDPDATA) ;

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
       

            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<GDPPages>(reader);
               return new GDPDATA { GDPPageHeader = objectValue };

               // return new WelcomeUnion { FluffyWelcome = objectValue };
                
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<Indicator>>(reader);
                return new GDPDATA {  Indicators= arrayValue };


        }
       
       
        throw new Exception("Cannot unmarshal type WelcomeUnion");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        var value = (GDPDATA)untypedValue;
        if (value.Indicators != null)
        {
            serializer.Serialize(writer, value.Indicators);
            return;
        }
        if (value.GDPPageHeader != null)
        {
            serializer.Serialize(writer, value.GDPPageHeader);
            return;
        }
        throw new Exception("Cannot marshal type WelcomeUnion");
    }

    public static readonly GDPDATAConverter Singleton = new GDPDATAConverter();
}