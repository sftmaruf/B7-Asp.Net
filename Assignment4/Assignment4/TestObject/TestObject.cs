using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.TestObject
{
    public static class TestObject
    {
        public static Car GetCar()
        {
            return new Car()
            {
                Id = 1,
                Model = "XLancher",
                Price = 102309,
                Brand = new Brand()
                {
                    Country = "Bangladesh",
                    Name = "Lamborghini"
                },
                Door = new List<Door>()
                {
                    new Door()
                    {
                        Id = 1,
                        Brand = new Brand()
                        {
                            Country = "Inidia",
                            Name = "DoorBrand1"
                        }
                    },
                    new Door()
                    {  
                        Id = 2,
                        Brand = new Brand()
                        {
                            Country = "Italy",
                            Name = "DoorBrand2"
                        }
                    }
                },
                Wheels = new List<Wheel>()
                {
                    new Wheel()
                    {
                        Id = 1,
                        Brand = new Brand()
                        {
                            Country = "Bangladesh",
                            Name = "WheelBrand1"
                        }
                    },
                    new Wheel()
                    {

                        Id = 2,
                        Brand = new Brand()
                        {
                            Country = "Bangladesh",
                            Name = "WheelBrand2"
                        }
                    }
                }
            };
        }

        public static Course GetCourse()
        {
            return new Course()
            {
                Id = 1,
                Title = "C#",
                Fees = 20000,
                Teacher = new Instructor()
                {
                    Id = 1,
                    Name = "Jalal Uddin Ahmed",
                    Email = "jalal@gmail.com",
                    PresentAddress = new Address()
                    {
                        Street = "Mirpur 10",
                        City = "Dhaka",
                        Country = "Bangladesh"
                    },
                    PermanentAddress = new Address()
                    {
                        Street = "Unknown",
                        City = "Unknown",
                        Country = "Bangladesh"
                    },
                    PhoneNumbers = new List<Phone>()
                    {
                        new Phone()
                        {
                            Number = "01711111111",
                            Extension = "",
                            CountryCode = "880"
                        }
                    }
                },
                Topics = new List<Topic>()
                {
                    new Topic()
                    {
                        Id = new Guid("5beb0691-e31b-48d3-9007-da42ecc467ff"),
                        Title = "New topic",
                        Description = "Topic",
                        Sessions = new List<Session>()
                        {
                            new Session()
                            {
                                DurationInHour = 2,
                                LearningObjective = "New Session"
                            }
                        },
                    }
                },
                Tests = new List<AdmissionTest>()
                {
                    new AdmissionTest()
                    {
                        EndDateTime = DateTime.Now,
                        StartDateTime = DateTime.Now,
                        TestFees = 200
                    }
                }
            };
        }
    }
}
