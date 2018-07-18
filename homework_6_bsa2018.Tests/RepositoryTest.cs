using System;
using System.Collections.Generic;
using System.Linq;
using homework_5_bsa2018.DAL;
using homework_5_bsa2018.DAL.Models;
using homework_5_bsa2018.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace homework_6_bsa2018.Tests
{
    public class RepositoryTest
    {
        private IQueryable<Pilot> data = 
            new List<Pilot>()  {
               new Pilot() {Id=1,FirstName="Vasya",LastName="Glupov",Experience=4 },
               new Pilot() {Id=2,FirstName="Kostya",LastName="Sidirov",Experience=5 },
               new Pilot() {Id=3,FirstName="Ivan",LastName="Ivanov",Experience=6 },
            }.AsQueryable();

        [Fact]
        public void UpdateTestPilots_When_WrongId()
        { 
            var mockSet = new Mock<DbSet<Pilot>>();
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportContext>();
            mockContext.Setup(c => c.Pilots).Returns(mockSet.Object);

            var repository = new PilotRepository(mockContext.Object);

            Assert.Throws<ArgumentNullException>(() => repository.Update(-1, data.FirstOrDefault()));
        }

        [Fact]
        public void DeletePilots_When_WrongId()
        {
            var mockSet = new Mock<DbSet<Pilot>>();
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportContext>();
            mockContext.Setup(c => c.Pilots).Returns(mockSet.Object);

            var repository = new PilotRepository(mockContext.Object);

            Assert.Throws<ArgumentNullException>(() => repository.Delete(2));
        }

        [Fact]
        public void GetPilot_When_WrongId_ThenReturnsNull()
        {

            var mockSet = new Mock<DbSet<Pilot>>();
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportContext>();
            mockContext.Setup(c => c.Pilots).Returns(mockSet.Object);

            var repository = new PilotRepository(mockContext.Object);

            var result = repository.GetAsync(-1);

            Assert.Null(result);
         }

        [Fact]
        public void GetPilot_When_CorrectId_ThenReturnsPilot()
        {

            var mockSet = new Mock<DbSet<Pilot>>();
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pilot>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportContext>();
            mockContext.Setup(c => c.Pilots).Returns(mockSet.Object);
            mockContext.Setup(c => c.Pilots.Find(1)).Returns(mockSet.Object.Find(1));
            var repository = new PilotRepository(mockContext.Object);
            
            var result = repository.GetAsync(1);

            Assert.Null(result);
        }
    }
}



