using MBSReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Reports
{
    class BreakReport : ReportFiles
    {
        public void GenerateBreakReport(InvestmentAccountLists accounts, string fileName)
        {
            var sbResultFile = new StringBuilder();
            sbResultFile.AppendLine("Account, Fund, Error Message");

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
            fund.CheckNegativeShares();

            if (fund.ErrorNegativeShares)
            {
                sbResultFile.AppendFormat("'{0}', '{1}', 'Error Negative Shares in Fund: {2}', {3}", account.Investor,
                    fund.Fund, fund.CurrentShares, Environment.NewLine);
            }
        }
    }
}
