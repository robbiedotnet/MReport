using System;
using MBSReportGenerator.Reports;
using MBSReportGenerator.Models;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            DateTime dtStart = new DateTime(2018, 1, 1);
            DateTime dtEnd = new DateTime(2018, 2, 1);

            SalesDataFile salesData = new SalesDataFile();
            salesData.PopulateFromCsvManual(@"D:\Applications\MRS_BPO\Reports\Input\Data.txt");

            SalesSummary ss = new SalesSummary();
            //ss.GetTotalCashSold(salesData.Transactions, "John Q. Public", dtStart, dtEnd);

            Assert.Pass();
        }
    }
}