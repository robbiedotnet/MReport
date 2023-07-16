using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MBSReportGenerator.Models
{
    public class SalesDataFile
    {
        public List<SalesTransactions> Transactions { get; set; }
        public string FileName { get; set; }

        public SalesDataFile()
        {
            Transactions = new List<SalesTransactions>();
        }

        public void PopulateFromCsv(string fileSource)
        {

            using (var reader = new StreamReader(fileSource))
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);
                config.HeaderValidated = null;
                config.MissingFieldFound = null;
                using (var csv = new CsvReader(reader, config))            {
                    //csv.Configuration.TypeConverterCache.AddConverter<decimal>(new CurrencyDecimalConverter());
                    Transactions = csv.GetRecords<SalesTransactions>().ToList();
                }
            }

            FileName = Path.GetFileName(fileSource);
        }

        public void PopulateFromCsvManual(string fileSource)
        {
            using (var reader = new StreamReader(fileSource))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            {
                
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new SalesTransactions
                    {
                        TransactionDate = csv.GetField<DateTime>("TXN_DATE"),
                        TransactionType = csv.GetField("TXN_TYPE"),
                        Fund = csv.GetField("FUND"),
                        Investor = csv.GetField("INVESTOR"),
                        SalesRep = csv.GetField("SALES_REP"),
                        Shares = csv.GetField<decimal>("TXN_SHARES"),
                        PricePerShare = Decimal.Parse(csv.GetField("TXN_PRICE").Replace("$", "").Trim())

                    };

                    record.TotalPrice = record.Shares * record.PricePerShare;
                    if (record.TransactionType.ToUpper() == "BUY") { record.TotalPrice *= -1;}
                Transactions.Add(record);
                }
            }

            FileName = Path.GetFileName(fileSource);
        }
    }
}
