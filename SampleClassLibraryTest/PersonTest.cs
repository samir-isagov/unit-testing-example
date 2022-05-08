using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleClassLibrary.PersonClasses;

namespace SampleClassLibraryTest;

[TestClass]
public class PersonTest : TestBase
{
    [TestMethod]
    public void IsNullTest()
    {
        PersonManager mgr = new PersonManager();
        Person per;

        per = mgr.CreatePerson("", "Sheriff", true);

        Assert.IsNull(per);
    }

    [TestMethod]
    public void IsInstanceOfTypeTest()
    {
        PersonManager mgr = new PersonManager();
        Person per;

        per = mgr.CreatePerson("Paul", "Sheriff",
            true);

        Assert.IsInstanceOfType(per,
            typeof(Supervisor));
    }

}