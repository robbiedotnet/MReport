using MBSReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Reports
{
    class ProfitReport : ReportFiles
    {
        public void GenerateNetProfitReport(List<SalesTransactions> salesData, string fileName)
        {
            var sbResultFile = new StringBuilder();
            sbResultFile.AppendLine("Investor, Fund, Total_Profit");

            var generatedReport = salesData
                .GroupBy(x => new { x.Investor, x.Fund })
                .Select(a => new { ProfitAmount = a.Sum(b => b.TotalPrice), Fund = a.Key.Fund, Investor = a.Key.Investor })
                .ToList();

            foreach (var item in generatedReport)
            {
                sbResultFile.AppendFormat("'{0}', '{1}', {2}, {3}", item.Investor, item.Fund, item.ProfitAmount, Environment.NewLine);
            }

            WriteReportFile(sbResultFile, fileName);
        }

    }
}
