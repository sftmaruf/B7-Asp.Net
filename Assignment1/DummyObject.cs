using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class DummyObject
    {
        public static Dummy getObject(int option)
        {
            return option switch
            {
                1 => getTestObject1(),
                2 => getTestObject2(),
                _ => throw new ArgumentException("Invalid argument")
            };
        }
        private static Course getTestObject1()
        {
            var presentAddress = new Address()
            {
                Street = "1072 Poco Mas Drive",
                City = "Dallas",
                Country = "US"
            };

            var parmanentAddress = new Address()
            {
                Street = "3634 Limer Street",
                City = "Atlanta",
                Country = "US"
            };

            var phonesNumbers = new List<Phone>()
            {
                new Phone() { Number = "202-555-0162" , Extension = "000", CountryCode = "+1" },
                new Phone() { Number = "202-555-0139" , Extension = "000", CountryCode = "+1" },
            };

            var teacher = new Instructor()
            {
                Name = "Jalal Uddin",
                Email = "jalaluddin@gmail.com",
                PresentAddress = presentAddress,
                PermanentAddress = parmanentAddress,
                PhoneNumbers = phonesNumbers,
            };

            var topics = new List<Topic>()
            {
                new Topic() { Title = "Tool Installation", Description = "Installing Visual Studio", Sessions = new List<Session>() { new Session() { DurationInHour = 2, LearningObjective = "Install necessary tool for the course" } } },
                new Topic() { Title = "Version Control", Description = "Why we need version control", Sessions =  new List<Session>() { new Session() { DurationInHour = 2, LearningObjective = "Learn about version control" } } }
            };

            var tests = new List<AdmissionTest>()
            {
                new AdmissionTest() { StartDateTime = new DateTime(2022, 05, 26, 9, 0, 0), EndDateTime = new DateTime(2022, 05, 26, 10, 0, 0), TestFees = 100d },
                new AdmissionTest() { StartDateTime = new DateTime(2022, 06, 10, 9, 0, 0), EndDateTime = new DateTime(2022, 05, 26, 10, 0, 0), TestFees = 100d },
                new AdmissionTest() { StartDateTime = new DateTime(2022, 06, 27, 9, 0, 0), EndDateTime = new DateTime(2022, 05, 26, 10, 0, 0), TestFees = 100d },
            };

            Course course = new Course()
            {
                Title = "Asp.Net",
                Teacher = teacher,
                Topics = topics,
                Fees = 30_000d,
                Tests = tests
            };
            return course;
        }
        private static TestObject getTestObject2()
        {
            TestChildObject nestedObject = new();
            nestedObject.stringType = "property1";
            nestedObject.intType = 10;
            nestedObject.objectType = new() { name = "plane" };

            TestObject testObject = new TestObject();
            testObject.stringType = "Strign1";
            testObject.stringType1 = "Strign2";
            testObject.intType = 10;
            testObject.doubleType = 20.10;
            testObject.longType = 1111110;
            testObject.objectType = nestedObject;

            return testObject;
        }
    }

    public interface Dummy { }

    #region testobject-1
    public class Course : Dummy
    {
        public string? Title { get; set; }
        public Instructor? Teacher { get; set; }
        public List<Topic>? Topics { get; set; }
        public double Fees { get; set; }
        public List<AdmissionTest>? Tests { get; set; }
    }

    public class AdmissionTest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }

    public class Topic
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Session>? Sessions { get; set; }
    }

    public class Session
    {
        public int DurationInHour { get; set; }
        public string? LearningObjective { get; set; }
    }

    public class Instructor
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Address? PresentAddress { get; set; }
        public Address? PermanentAddress { get; set; }
        public List<Phone>? PhoneNumbers { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class Phone
    {
        public string? Number { get; set; }
        public string? Extension { get; set; }
        public string? CountryCode { get; set; }
    }
    #endregion

    #region testobject-2
    public class TestObject : Dummy
    {
        public string? stringType;
        public string? stringType1;
        public int intType;
        public double doubleType;
        public long longType;
        public TestChildObject? objectType;
        public DateTime dateType = DateTime.Now;
        public TestChildObject? objectPropertyType { get; set; }
        public IList<string>? listType = new List<string>() { "sft", "maruf", null };
        public IDictionary<string, int> dictionaryType = new Dictionary<string, int>() { { "sft", 1 }, { "Maruf", 20 } };
        public void TestMethod()
        {
            Console.WriteLine("This is a test method");
        }
    }

    public class TestChildObject
    {
        public string? stringType;
        public int intType;
        public ArrayList arrayListType = new ArrayList() { "car", "Robot", 1 };
        public string[] stringArray = new string[] { "1", "2", "3" };
        public Toy[] objectTypesArray = new Toy[] { new Toy() { name = "car" }, new Toy() { name = "robot" } };
        public Toy? objectType;
    }

    public class Toy
    {
        public string? name;
        public int price = 10;
    }
    #endregion
}