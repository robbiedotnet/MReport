using CsvHelper.Configuration;
using MBSReportGenerator.Models;

public sealed class TransactionMap : ClassMap<SalesTransactions>
{
    public TransactionMap()
    {
        Map(m => m.PricePerShare).TypeConverter<CurrencyDecimalConverter>();
    }
}