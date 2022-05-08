using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleClassLibraryTest;

[TestClass]
public class SampleClassLibraryTestInitialization
{
    [AssemblyInitialize()]
    public static void AssemblyInitialize(TestContext tc)
    {
        // TODO: Initialize for all tests within an assembly
        tc.WriteLine("In AssemblyInitialize() method");
    }

    [AssemblyCleanup()]
    public static void AssemblyCleanup()
    {
        // TODO: Clean up after all 
        // tests in an assembly
    }
}