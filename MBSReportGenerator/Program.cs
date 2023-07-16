using MBSReportGenerator.Models;
using MBSReportGenerator.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the file directory info where the data files are stored (see app.config)
            var dirFileStagingPath = Path.Combine(ConfigurationManager.AppSettings["MBSReport_Base"],
                ConfigurationManager.AppSettings["MBSReport_StagingFolder"]);

            var stagedFiles = Directory.GetFiles(dirFileStagingPath, ConfigurationManager.AppSettings["MBSReport_InputNameConvention"]);
            foreach (var file in stagedFiles)
            {
                string DateStampSuffix = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                SalesDataFile salesData = new SalesDataFile();
                salesData.PopulateFromCsvManual(file);

                InvestmentAccountLists accounts = new InvestmentAccountLists();
                accounts.PopulateList(salesData.Transactions);

                foreach (InvestorAccount account in accounts.InvestmentAccounts)
                {
                    account.PopulatePortfolio(salesData.Transactions);
                }

                // Report 1: Generate Sales Summary
                DateTime currentDate = new DateTime(2018, 4, 26); //Setting this as current date to simulate report generation 
                SalesSummary ss = new SalesSummary();
                ss.GenerateSalesReport(salesData.Transactions, String.Format("SalesSummaryReport_{0}.csv", DateStampSuffix), currentDate);

                // Report 1: Generate Asset Summary Report
                AssestUnderManagement aum = new AssestUnderManagement();
                aum.GenerateAssetReport(accounts, String.Format("AssestSummary_{0}.csv", DateStampSuffix));

                // Report 3: Generating Break Report (with Negative Share because file has no info to track balance).
                BreakReport br = new BreakReport();
                br.GenerateBreakReport(accounts, String.Format("BreakReport_{0}.csv", DateStampSuffix)); 
                
                // Report 4: Generating the profit report
                ProfitReport rf = new ProfitReport();
                rf.GenerateNetProfitReport(salesData.Transactions, String.Format("ProfitReport_{0}.csv", DateStampSuffix));
            }
        }
    }
}
