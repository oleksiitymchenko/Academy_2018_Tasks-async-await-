using Xunit;
using Moq;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.BLL.Services;
using System.Reflection;
using System.Collections.Generic;
using homework_5_bsa2018.DAL.Models;
using System;

namespace homework_6_bsa2018.Tests
{
    public class CustomMapperTest
    {
        [Fact]
        public void CreateOrUpdateCrew_WhenMapsFromDtoTOModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var pilot = new Pilot() { Id = 1, FirstName = "Petro", LastName = "Poroh", Experience = 3 };
            var stew1 = new Stewardess() { Id = 1, FirstName = "Nastya", LastName = "Kutsyk", DateOfBirth = new DateTime(2008, 2, 2) };
            var stew2 = new Stewardess() { Id = 1, FirstName = "Vitalina", LastName = "Avgustova", DateOfBirth = new DateTime(1970, 6, 23) };

            IService<CrewDTO> service = new CrewService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(o => o.Pilots.Get(1))
                .Returns(pilot);
            mockUnitOfWork.Setup(o => o.Save());
            mockUnitOfWork.Setup(o => o.Stewardesses.Get(1))
                .Returns(stew1);
            mockUnitOfWork.Setup(o => o.Stewardesses.Get(2))
                .Returns(stew2);

            MethodInfo methodInfo = typeof(CrewService).GetMethod("TransformCrew", BindingFlags.NonPublic | BindingFlags.Instance);

            var crewdto = new CrewDTO() { Id = 1, PilotId = 1, StewardressIds = new List<int>() { 1, 2 } };
            object[] parameters = { crewdto };

            Crew mappedCrew = (Crew)methodInfo.Invoke(service, parameters);

            Crew expected = new Crew() { Pilot = pilot, Stewardesses = new List<Stewardess>() { stew1, stew2 } };

            Assert.Equal(expected.Pilot, mappedCrew.Pilot);
            Assert.Equal(expected.Stewardesses, mappedCrew.Stewardesses);

        }

        [Fact]
        public void CreateOrUpdateCrew_WhenMapsFromDtoTOModelAndNoPilotOrStewardess()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var pilot = new Pilot() { Id = 1, FirstName = "Petro", LastName = "Poroh", Experience = 3 };
            var stew1 = new Stewardess() { Id = 1, FirstName = "Nastya", LastName = "Kutsyk", DateOfBirth = new DateTime(2008, 2, 2) };
            var stew2 = new Stewardess() { Id = 1, FirstName = "Vitalina", LastName = "Avgustova", DateOfBirth = new DateTime(1970, 6, 23) };

            IService<CrewDTO> service = new CrewService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(o => o.Pilots.Get(1))
                .Returns((Pilot)null);
            mockUnitOfWork.Setup(o => o.Save());
            mockUnitOfWork.Setup(o => o.Stewardesses.Get(1))
                .Returns((Stewardess)null);
            mockUnitOfWork.Setup(o => o.Stewardesses.Get(2))
                .Returns((Stewardess)null);

            MethodInfo methodInfo = typeof(CrewService).GetMethod("TransformCrew", BindingFlags.NonPublic | BindingFlags.Instance);

            var crewdto = new CrewDTO() {  PilotId = 1, StewardressIds = new List<int>() { 1, 2 } };
            object[] parameters = { crewdto };

            Assert.Throws<TargetInvocationException>(delegate 
            { Crew mappedCrew = (Crew)methodInfo.Invoke(service, parameters); });
        }

        [Fact]
        public void CreateOrUpdatePlane_WhenMapsFromDtoToModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            PlaneType planeType = new PlaneType()
            { Id = 1, Model = "Mriya", Carrying = 20000, Places = 200 };

            IService<PlaneDTO> service = new PlaneService(mockUnitOfWork.Object);
            
            mockUnitOfWork.Setup(o => o.PlaneTypes.Get(1))
                .Returns(planeType);

            MethodInfo methodInfo = typeof(PlaneService).GetMethod("TransformPlane", BindingFlags.NonPublic | BindingFlags.Instance);

            var planedto = new PlaneDTO()
            { TypePlaneId = 1, Name = "AN-225",
                Created = new DateTime(1999, 9, 23).ToShortDateString(),
                LifeTime = new TimeSpan(100, 100, 100, 100).ToString() };

            object[] parameters = { planedto };

            Plane mappedPlane = (Plane)methodInfo.Invoke(service, parameters);

            Plane expected = new Plane()
            { TypePlane = planeType, Name="AN-225",
                Created = new DateTime(1999,9,23), LifeTime = new TimeSpan(100,100,100,100)};

            Assert.Equal(expected.Name, mappedPlane.Name);
            Assert.Equal(expected.TypePlane, mappedPlane.TypePlane);
            Assert.Equal(expected.Created, mappedPlane.Created);
            Assert.Equal(expected.LifeTime, mappedPlane.LifeTime);
        }

        [Fact]
        public void CreateOrUpdatePlane_WhenMapsFromDtoToModel_AndNoPlaneType()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IService<PlaneDTO> service = new PlaneService(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(o => o.PlaneTypes.Get(1))
                .Returns((PlaneType)null);

            MethodInfo methodInfo = typeof(PlaneService)
                .GetMethod("TransformPlane", BindingFlags.NonPublic | BindingFlags.Instance);

            var planedto = new PlaneDTO()
            {
                TypePlaneId = 1,
                Name = "AN-225",
                Created = new DateTime(1999, 9, 23).ToShortDateString(),
                LifeTime = new TimeSpan(100, 100, 100, 100).ToString()
            };

            object[] parameters = { planedto };

            Assert.Throws<TargetInvocationException>(delegate
            { Plane mappedPlane = (Plane)methodInfo.Invoke(service, parameters); });
        }

        [Fact]
        public void CreateOrUpdateFlight_WhenMapsFromDtoToModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var flightdto = new FlightDTO()
            { Id=1,Number="MH-17",StartPoint="Kiev",FinishPoint="Moscow",
                StartTime=new DateTime(2018,9,23,12,30,0).ToString(),
                FinishTime = new DateTime(2018, 9, 23, 14, 30, 0).ToString(),
            TicketIds = new List<int>() {1,2 }};

            var ticket1 = new Ticket() { Id = 1, FlightNumber = "MH-17", Price = 200 };
            var ticket2 = new Ticket() { Id = 2, FlightNumber = "MH-17", Price = 200 };

            IService<FlightDTO> service = new FlightService(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(o => o.Tickets.Get(1))
                .Returns(ticket1);
            mockUnitOfWork.Setup(o => o.Tickets.Get(2))
               .Returns(ticket2);

            MethodInfo methodInfo = typeof(FlightService).GetMethod("TransformFlight", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters = { flightdto };

            Flight mappedFlight = (Flight)methodInfo.Invoke(service, parameters);

            Flight expected = new Flight()
            {
                Number = "MH-17",
                StartPoint = "Kiev",
                FinishPoint = "Moscow",
                StartTime = new DateTime(2018, 9, 23, 12, 30, 0),
                FinishTime = new DateTime(2018, 9, 23, 14, 30, 0),
                Tickets = new List<Ticket>() { ticket1, ticket2 }
            };

            Assert.Equal(expected.Number, mappedFlight.Number);
            Assert.Equal(expected.FinishPoint, mappedFlight.FinishPoint);
            Assert.Equal(expected.StartPoint, mappedFlight.StartPoint);
            Assert.Equal(expected.StartTime, mappedFlight.StartTime);
            Assert.Equal(expected.FinishTime, mappedFlight.FinishTime);
            Assert.Equal(expected.Tickets, mappedFlight.Tickets);
        }

        [Fact]
        public void CreateOrUpdateFlight_WhenMapsFromDtoToModel_AndNoTickets()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var flightdto = new FlightDTO()
            {
                Id = 1,
                Number = "MH-17",
                StartPoint = "Kiev",
                FinishPoint = "Moscow",
                StartTime = new DateTime(2018, 9, 23, 12, 30, 0).ToString(),
                FinishTime = new DateTime(2018, 9, 23, 14, 30, 0).ToString(),
                TicketIds = new List<int>() { 1, 2 }
            };

            IService<FlightDTO> service = new FlightService(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(o => o.Tickets.Get(1))
                .Returns((Ticket)null);
            mockUnitOfWork.Setup(o => o.Tickets.Get(2))
               .Returns((Ticket)null);

            MethodInfo methodInfo = typeof(FlightService).GetMethod("TransformFlight", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters = { flightdto };

            Assert.Throws<TargetInvocationException>(delegate
            { Flight mappedFlight = (Flight)methodInfo.Invoke(service, parameters); });
        }

        [Fact]
        public void CreateOrUpdateDeparture_WhenMapsFromDtoToModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var departuredto = new DepartureDTO()
            {
                Id = 1,
                CrewId = 1,
                PlaneId = 1,
                DepartureTime = new DateTime(2018, 9, 15, 12, 30, 0).ToString(),
                FlightNumber = "MH-17"
            };
            var planeType = new PlaneType()
            { Id = 1, Model = "Mriya", Carrying = 20000, Places = 200 };
            var plane = new Plane()
            {
                Id= 1,
                TypePlane = planeType,
                Name = "AN-225",
                Created = new DateTime(1999, 9, 23),
                LifeTime = new TimeSpan(100, 100, 100, 100)
            };
            var pilot = new Pilot() { Id = 1, FirstName = "Petro", LastName = "Poroh", Experience = 3 };
            var stew1 = new Stewardess() { Id = 1, FirstName = "Nastya", LastName = "Kutsyk", DateOfBirth = new DateTime(2008, 2, 2) };
            var stew2 = new Stewardess() { Id = 1, FirstName = "Vitalina", LastName = "Avgustova", DateOfBirth = new DateTime(1970, 6, 23) };
            var crew = new Crew() {Id=1, Pilot = pilot, Stewardesses = new List<Stewardess>() { stew1, stew2 } };
            var expected = new Departure()
            {
                Id = 1,
                DepartureTime = new DateTime(2018, 9, 15, 12, 30, 0),
                FlightNumber = "MH-17",
                Plane = plane,
                Crew = crew,
            };

            IService<DepartureDTO> service = new DepartureService(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(o => o.Planes.Get(1))
                .Returns(plane);
            mockUnitOfWork.Setup(o => o.Crews.Get(1))
               .Returns(crew);

            MethodInfo methodInfo = typeof(DepartureService).GetMethod("TransformDeparture", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters = { departuredto };

            Departure mappedDeparture = (Departure)methodInfo.Invoke(service, parameters);

            Assert.Equal(expected.Crew, mappedDeparture.Crew);
            Assert.Equal(expected.Plane, mappedDeparture.Plane);
            Assert.Equal(expected.DepartureTime, mappedDeparture.DepartureTime);
            Assert.Equal(expected.FlightNumber, mappedDeparture.FlightNumber);
        }

        [Fact]
        public void CreateOrUpdateDeparture_WhenMapsFromDtoToModel_AndNoPlaneCrew()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(o => o.Crews.Get(1))
               .Returns((Crew)null);
            mockUnitOfWork.Setup(o => o.Planes.Get(1))
               .Returns((Plane)null);

            var departuredto = new DepartureDTO()
            {
                Id = 1,
                CrewId = 1,
                PlaneId = 1,
                DepartureTime = new DateTime(2018, 9, 15, 12, 30, 0).ToString(),
                FlightNumber = "MH-17"
            };

            IService<DepartureDTO> service = new DepartureService(mockUnitOfWork.Object);
           
            MethodInfo methodInfo = typeof(DepartureService).GetMethod("TransformDeparture", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters = { departuredto };

            Assert.Throws<TargetInvocationException>(delegate
            { Departure mappedFlight = (Departure)methodInfo.Invoke(service, parameters); });
        }
    }
}
