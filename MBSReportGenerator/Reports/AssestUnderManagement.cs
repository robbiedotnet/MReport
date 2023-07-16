using MBSReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Reports
{
    class AssestUnderManagement : ReportFiles
    {
        public void GenerateAssetReport(InvestmentAccountLists accounts, string fileName)
        {
            var sbResultFile = new StringBuilder();
            sbResultFile.AppendLine("Sales Rep, Investor, Fund, Current Share Held, Current Value");

            foreach (InvestorAccount account in accounts.InvestmentAccounts)
            {
                foreach (InvestmentPortfolio fund in account.InvestmentPortfolio)
                {
                    PrintLine(fund, sbResultFile, account);
                }
            }

            WriteReportFile(sbResultFile, fileName);
        }

        private static void PrintLine(InvestmentPortfolio fund, StringBuilder sbResultFile, InvestorAccount account)
        {
            sbResultFile.AppendFormat("'{0}', '{1}', '{2}', {3}, 'Currently missing do not having current price data on fund' {4}",
                account.SalesRep, account.Investor, fund.Fund, fund.CurrentShares, Environment.NewLine);

        }
    }
}
