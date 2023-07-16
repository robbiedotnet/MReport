using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace MBSReportGenerator.Models
{

    //TXN_DATE,TXN_TYPE,TXN_SHARES,TXN_PRICE,FUND,INVESTOR,SALES_REP
    public class SalesTransactions
    {
        [Name("TXN_DATE")]
        public DateTime TransactionDate { get; set; }
        
        [Name("TXN_TYPE")]
        public String TransactionType { get; set; }
        
        [Name("FUND")]
        public String Fund { get; set; }
        
        [Name("INVESTOR")]
        public String Investor { get; set; }
        
        [Name("SALES_REP")]
        public String SalesRep { get; set; }
        
        [Name("TXN_SHARES")]
        public decimal Shares { get; set; }

        [TypeConverter(typeof(CurrencyDecimalConverter))]
        [Name("TXN_PRICE")]
        public decimal PricePerShare { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
