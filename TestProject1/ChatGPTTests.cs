using NUnit.Framework;
using System.IO;
using System.Linq;
using MBSReportGenerator.Models;
using CsvHelper;
using System;

namespace MBSReportGenerator.Tests
{
    public class SalesDataFileTests
    {
        private string _testCsvFilePath;
        private SalesDataFile _salesDataFile;

        [SetUp]
        public void Setup()
        {
            _testCsvFilePath = Path.Combine(@"D:\Doc\TestData", "testdata.csv");
            _salesDataFile = new SalesDataFile();
        }

        [Test]
        public void PopulateFromCsv_WhenCalled_PopulatesTransactionsList()
        {
            _salesDataFile.PopulateFromCsv(_testCsvFilePath);
            Assert.IsNotNull(_salesDataFile.Transactions);
            Assert.IsTrue(_salesDataFile.Transactions.Any());
        }

        [Test]
        public void PopulateFromCsv_WhenCalled_SetsFileName()
        {
            _salesDataFile.PopulateFromCsv(_testCsvFilePath);
            Assert.AreEqual("testdata.csv", _salesDataFile.FileName);
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalled_PopulatesTransactionsList()
        {
            _salesDataFile.PopulateFromCsvManual(_testCsvFilePath);
            Assert.IsNotNull(_salesDataFile.Transactions);
            Assert.IsTrue(_salesDataFile.Transactions.Any());
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalled_SetsFileName()
        {
            _salesDataFile.PopulateFromCsvManual(_testCsvFilePath);
            Assert.AreEqual("testdata.csv", _salesDataFile.FileName);
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalled_SetsTransactionData()
        {
            _salesDataFile.PopulateFromCsvManual(_testCsvFilePath);
            var transaction = _salesDataFile.Transactions.First();
            Assert.AreEqual(new DateTime(2022, 1, 1), transaction.TransactionDate);
            Assert.AreEqual("Buy", transaction.TransactionType);
            Assert.AreEqual("Fund1", transaction.Fund);
            Assert.AreEqual("Investor1", transaction.Investor);
            Assert.AreEqual("Rep1", transaction.SalesRep);
            Assert.AreEqual(100, transaction.Shares);
            Assert.AreEqual(10, transaction.PricePerShare);
            Assert.AreEqual(-1000, transaction.TotalPrice);
        }

        [Test]
        public void PopulateFromCsv_WhenCalledWithNonExistentFile_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => _salesDataFile.PopulateFromCsv("nonexistent.csv"));
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalledWithNonExistentFile_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => _salesDataFile.PopulateFromCsvManual("nonexistent.csv"));
        }

        [Test]
        public void PopulateFromCsv_WhenCalledWithInvalidCsv_ThrowsCsvHelperException()
        {
            var invalidCsvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invalid.csv");
            File.WriteAllText(invalidCsvFile, "This is not a valid csv file");
            Assert.Throws<HeaderValidationException>(() => _salesDataFile.PopulateFromCsv(invalidCsvFile));
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalledWithInvalidCsv_ThrowsCsvHelperException()
        {
            var invalidCsvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invalid.csv");
            File.WriteAllText(invalidCsvFile, "This is not a valid csv file");
            Assert.Throws<CsvHelperException>(() => _salesDataFile.PopulateFromCsvManual(invalidCsvFile));
        }

        [Test]
        public void PopulateFromCsv_WhenCalledWithCsvMissingHeaders_ThrowsCsvMissingFieldException()
        {
            var csvMissingHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "missingheaders.csv");
            File.WriteAllText(csvMissingHeaders, "Data1,Data2,Data3\nvalue1,value2,value3");
            Assert.Throws<HeaderValidationException>(() => _salesDataFile.PopulateFromCsv(csvMissingHeaders));
        }

        [Test]
        public void PopulateFromCsvManual_WhenCalledWithCsvMissingHeaders_ThrowsCsvMissingFieldException()
        {
            var csvMissingHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "missingheaders.csv");
            File.WriteAllText(csvMissingHeaders, "Data1,Data2,Data3\nvalue1,value2,value3");
            Assert.Throws<CsvHelper.MissingFieldException>(() => _salesDataFile.PopulateFromCsvManual(csvMissingHeaders));
        }


    }
}
