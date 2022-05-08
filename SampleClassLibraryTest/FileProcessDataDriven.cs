using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleClassLibrary;

namespace SampleClassLibraryTest;

[TestClass]
public class FileProcessDataDriven : TestBase
{
    [TestMethod()]
    public void FileExistsTestFromDB()
    {
        FileProcess fp = new FileProcess();
        bool fromCall = false;
        bool testFailed = false;
        string fileName;
        bool expectedValue;
        bool causesException;
        string sql = "SELECT * FROM tests.FileProcessTest";
        string conn = TestContext.Properties["ConnectionString"].ToString(); ;

        // Load data from SQL Server table
        LoadDataTable(sql, conn);

        if (TestDataTable != null) {
            // Loop through all rows in table
            foreach (DataRow row in TestDataTable.Rows) {
                // Get values from data row
                fileName = row["FileName"].ToString();
                expectedValue = Convert.ToBoolean(row["ExpectedValue"]);
                causesException = Convert.ToBoolean(row["CausesException"]);

                try {
                    // See if file exists
                    fromCall = fp.FileExists(fileName);
                }
                catch (ArgumentNullException) {
                    // See if a null value was expected
                    if (!causesException) {
                        testFailed = true;
                    }
                }
                catch (Exception) {
                    testFailed = true;
                }

                TestContext.WriteLine("Testing File: '{0}', Expected Value: '{1}', Actual Value: '{2}', Result: '{3}'", fileName, expectedValue, fromCall, (expectedValue == fromCall ? "Success" : "FAILED"));

                // Check assertion
                if (expectedValue != fromCall) {
                    testFailed = true;
                }
            }

            if (testFailed) {
                Assert.Fail("Data Driven Tests Have Failed, Check Additional Output for More Information.");
            }
        }
    }
}