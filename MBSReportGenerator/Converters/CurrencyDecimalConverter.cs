using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

public class CurrencyDecimalConverter : ITypeConverter
{
    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return Decimal.Parse(text.Replace("$", ""));
    }

    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value.ToString();
    }
}