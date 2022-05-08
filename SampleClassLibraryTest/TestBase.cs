using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleClassLibraryTest;

public class TestBase
{
    public TestContext TestContext { get; set; }
    protected string _GoodFileName;
    public DataTable TestDataTable { get; set; }

    public DataTable LoadDataTable(string sql, string connection)
    {
        TestDataTable = null;

        try {
            // Create a connection
            using (SqlConnection ConnectionObject = new SqlConnection(connection)) {
                // Create command object
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject)) {
                    // Create a SQL Data Adapter
                    using (SqlDataAdapter da = new SqlDataAdapter(CommandObject)) {
                        // Fill DataTable using Data Adapter
                        TestDataTable = new DataTable();
                        da.Fill(TestDataTable);
                    }
                }
            }
        }
        catch (Exception ex) {
            TestContext.WriteLine("Error in LoadDataTable() method." + Environment.NewLine + ex.ToString());
        }

        return TestDataTable;
    }

    protected void WriteDescription(Type typ)
    {
        string testName = TestContext.TestName;

        // Find the test method currently executing
        MemberInfo method = typ.GetMethod(testName);
        if (method != null) {
            // See if the [Description] attribute exists on this test
            Attribute attr = method.GetCustomAttribute(
                typeof(DescriptionAttribute));
            if (attr != null) {
                // Cast the attribute to a DescriptionAttribute
                DescriptionAttribute dattr = (DescriptionAttribute)attr;
                // Display the test description
                TestContext.WriteLine("Test Description: " + dattr.Description);
            }
        }
    }

    protected void SetGoodFileName()
    {
        _GoodFileName = TestContext.Properties["GoodFileName"].ToString();
        if (_GoodFileName.Contains("[AppPath]")) {
            _GoodFileName = _GoodFileName.Replace("[AppPath]",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
    }
}