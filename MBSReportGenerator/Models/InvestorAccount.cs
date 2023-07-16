using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Models
{
    public class InvestorAccount
    {
        public String Investor { get; set; }

        public String SalesRep { get; set; }

        public List<InvestmentPortfolio> InvestmentPortfolio { get; set; }

        public InvestorAccount()
        {
            InvestmentPortfolio = new List<InvestmentPortfolio>();
        }

        public InvestorAccount(string investor, string salesRep)
        {
            Investor = investor;
            SalesRep = salesRep;

            InvestmentPortfolio = new List<InvestmentPortfolio>();
        }

        public void PopulatePortfolio(List<SalesTransactions> transactions)
        {
            var portfolioData = transactions
                .Where(x => x.Investor == Investor)
                .Select(x => x.Fund).Distinct()
                .ToList();
            
            foreach (var item in portfolioData)
            {
                InvestmentPortfolio newFund = new InvestmentPortfolio
                {
                    CurrentShares = 0,
                    Fund = item
                };

                var portfolioFundData = transactions
                    .Where(x => x.Investor == Investor && x.Fund == item)
                    .Select(x => new { Transaction = x.TransactionType, Amount = x.Shares })
                    .ToList();

                foreach (var transaction in portfolioFundData)
                {
                    newFund.UpdateShares(transaction.Amount, transaction.Transaction);
                }

                InvestmentPortfolio.Add(newFund);
            }
        }

    }
}
