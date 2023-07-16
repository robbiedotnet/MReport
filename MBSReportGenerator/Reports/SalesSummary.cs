using MBSReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSReportGenerator.Reports
{
    public class SalesSummary : ReportFiles
    {
        public void GenerateSalesReport(List<SalesTransactions> transactions, string fileName, DateTime currentDate)
        {
            var sbResultFile = new StringBuilder();
            sbResultFile.AppendLine("Sales Rep, YTD, MTD, QYD, ITD");

            var salesRepList = transactions.Select(x => x.SalesRep).Distinct();
            foreach (string salesRep in salesRepList)
            {
                sbResultFile.AppendLine(PrintSalesRepData(transactions, salesRep, currentDate));
            }

            WriteReportFile(sbResultFile, fileName);
        }

        private string PrintSalesRepData(List<SalesTransactions> transactions, string SalesRep, DateTime currentDate)
        {
            DateTime ytdState = new DateTime(currentDate.Year, 1, 1);
            DateTime mtdState = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime qtdState = new DateTime(currentDate.Year, getQuarterMonth(currentDate), 1);
            DateTime itdState = new DateTime(2000, 1, 1);

            decimal ytdSales = GetTotalCashSold(transactions, SalesRep, ytdState, currentDate);
            decimal mtdSales = GetTotalCashSold(transactions, SalesRep, mtdState, currentDate);
            decimal qtdSales = GetTotalCashSold(transactions, SalesRep, qtdState, currentDate);
            decimal itdSales = GetTotalCashSold(transactions, SalesRep, itdState, currentDate);

            return String.Format("{0}, {1}, {2}, {3}, {4}", SalesRep, ytdSales, mtdSales, qtdSales, itdSales);
        }

        private int getQuarterMonth(DateTime currentDate)
        {
            switch (currentDate.Month)
            {
                case 1:
                case 2:
                case 3:
                    return 1;

                case 4:
                case 5:
                case 6:
                    return 4;

                case 7:
                case 8:
                case 9:
                    return 7;

                case 10:
                case 11:
                case 12:
                    return 10;
            }
            return 1;
        }

        private decimal GetTotalCashSold(List<SalesTransactions> transactions, string salesRep, DateTime startDate, DateTime endDate)
        {
            var salesRepData = transactions
                .Select(x => x)
                .Where(x => x.SalesRep == salesRep && x.TransactionType.ToUpper() == "SELL"
                                                   && x.TransactionDate >= startDate && x.TransactionDate <= endDate)
                .ToList();

            var totalSales = salesRepData
                .GroupBy(x => new {x.SalesRep})
                .Select(x =>
                    new {TotalSum = x.Sum(y => y.TotalPrice), x.Key.SalesRep}).FirstOrDefault();

            if (totalSales == null) return 0;
            return totalSales.TotalSum;
        }
    }
}
