using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleClassLibrary.PersonClasses;

namespace SampleClassLibraryTest;

[TestClass]
public class CollectionAssertClassTest : TestBase
{
    [TestMethod]
    public void AreCollectionsEqualWithComparerTest()
    {
        PersonManager mgr = new PersonManager();
        List<Person> peopleExpected = new List<Person>();
        List<Person> peopleActual;

        peopleExpected.Add(new Person() { FirstName = "Paul", LastName = "Sheriff" });
        peopleExpected.Add(new Person() { FirstName = "John", LastName = "Kuhn" });
        peopleExpected.Add(new Person() { FirstName = "Jim", LastName = "Ruhl" });

        peopleActual = mgr.GetPeople();

        // Provide your own "Comparer" to determine equality
        CollectionAssert.AreEqual(peopleExpected, peopleActual,
            Comparer<Person>.Create((x, y) =>
                x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
    }

    [TestMethod]
    public void AreCollectionsEquivalentTest()
    {
        PersonManager mgr = new PersonManager();
        List<Person> peopleExpected = new List<Person>();
        List<Person> peopleActual;

        // Get Person objects
        peopleActual = mgr.GetPeople();

        // Add same Person objects to new collection, but in a different order
        peopleExpected.Add(peopleActual[1]);
        peopleExpected.Add(peopleActual[2]);
        peopleExpected.Add(peopleActual[0]);

        // Checks for same objects, but in any order
        CollectionAssert.AreEquivalent(peopleExpected, peopleActual);
    }

    [TestMethod]
    public void IsCollectionOfTypeTest()
    {
        PersonManager mgr = new PersonManager();
        List<Person> peopleActual = new List<Person>();

        peopleActual = mgr.GetSupervisors();

        CollectionAssert.AllItemsAreInstancesOfType(
            peopleActual, typeof(Supervisor));
    }

    [TestMethod]
    public void AreCollectionsEqualTest()
    {
        PersonManager mgr = new PersonManager();
        List<Person> peopleExpected;
        List<Person> peopleActual;

        peopleActual = mgr.GetPeople();
        peopleExpected = peopleActual;

        // NOTE: By default it compares the person objects to see if they are Equal (they refer to the same object)
        CollectionAssert.AreEqual(peopleExpected, peopleActual);
    }

    [TestMethod]
    public void AreCollectionsEqual()
    {
        PersonManager mgr = new PersonManager();
        List<Person> peopleExpected = new List<Person>();
        List<Person> peopleActual = new List<Person>();

        peopleExpected.Add(new Person() { FirstName = "Paul", LastName = "Sheriff" });
        peopleExpected.Add(new Person() { FirstName = "John", LastName = "Kuhn" });
        peopleExpected.Add(new Person() { FirstName = "Jim", LastName = "Ruhl" });

        peopleActual = mgr.GetPeople();

        // NOTE: By default it compares the person objects to see if they are Equal (they refer to the same object)
        CollectionAssert.AreNotEqual(peopleExpected, peopleActual);
    }
}