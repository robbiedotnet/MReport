using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Models
{
    public class InvestmentAccountLists
    {
        public List<InvestorAccount> InvestmentAccounts { get; set; }

        public InvestmentAccountLists()
        {
            InvestmentAccounts = new List<InvestorAccount>();
        }

        public void PopulateList(List<SalesTransactions> transactions)
        {
            var investorList = transactions
                .Select(x => new {x.Investor, x.SalesRep}).Distinct()
                .ToList();

            foreach (var item in investorList)
            {
                InvestorAccount newAccount = new InvestorAccount(item.Investor, item.SalesRep);
                InvestmentAccounts.Add(newAccount);
            }
        }
    }
}
