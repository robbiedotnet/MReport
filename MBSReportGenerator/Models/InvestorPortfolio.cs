using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Models
{
    public class InvestmentPortfolio
    {
        public String Fund { get; set; }
        public decimal CurrentShares { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool ErrorNegativeShares { get; set; }


        public void UpdateShares(decimal amount, string transactionType)
        {
            switch (transactionType.ToUpper())
            {
                case "BUY":
                    CurrentShares += amount;
                    break;
                case "SELL":
                    CurrentShares += (amount * -1);
                    break;
            }
        }

        public void CheckNegativeShares()
        {
            if (CurrentShares < 0) ErrorNegativeShares = true;
        }
    }


}
