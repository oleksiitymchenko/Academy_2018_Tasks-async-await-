using homework_5_bsa2018.DAL.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace homework_5_bsa2018.DAL
{
    public class SeedData
    {
        public void Initialize(AirportContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Tickets.Any())
            {
                Console.WriteLine("TICKETS INITIALIZATING");
                List<Ticket> Tickets = new List<Ticket>()
                {
                    new Ticket()
                    { FlightNumber = "MH-17", Price = 200.5 },
                    new Ticket()
                    { FlightNumber = "AA-26", Price = 150.5 },
                     new Ticket()
                    { FlightNumber = "MH-17", Price = 200.5 }
                };
                context.Tickets.AddRange(Tickets);
            }
         
            context.SaveChanges();




            if (!context.Crews.Any())
            {
                List<Crew> Crews = new List<Crew>()
        {
             new Crew()
            {
                Pilot = new Pilot()
                { FirstName ="Petro", LastName ="Poroshenko", Experience =3, },
                Stewardesses = new List<Stewardess>()
                { new Stewardess()
                    {FirstName = "Iryna", LastName = "Gerashenko", DateOfBirth = DateTime.Parse("05/05/1965") },

                   new Stewardess()
                    { FirstName = "Natalia", LastName = "Korolevska", DateOfBirth = DateTime.Parse("05/05/1975") },
                }
            },
             new Crew()
            {
                Pilot =  new Pilot()
                {   FirstName ="Oleg", LastName ="Lyashko", Experience =5, },
                Stewardesses = new List<Stewardess>()
                {
                    new Stewardess()
                    { FirstName = "Yulya", LastName = "Tymoshenko", DateOfBirth = DateTime.Parse("05/05/1980") }
                }
            }
        };
                context.AddRange(Crews);
            }
            context.SaveChanges();

            if (!context.Departures.Any())
            {
                List<Departure> Departures = new List<Departure>()
        {
            new Departure()
            {
                DepartureTime = DateTime.Parse("2018-05-01 7:45:42Z"),
                Crew = new Crew
                 {
                Pilot = new Pilot()
                {
                    FirstName ="Petro",
                    LastName ="Poroshenko",
                    Experience =3,
                },
                Stewardesses = new List<Stewardess>()
                {
                   new Stewardess()
                    {
                       
                        FirstName = "Iryna",
                        LastName = "Gerashenko",
                        DateOfBirth = DateTime.Parse("05/05/1965")
                    },

                   new Stewardess()
                    {
                       
                        FirstName = "Natalia",
                        LastName = "Korolevska",
                        DateOfBirth = DateTime.Parse("05/05/1975")
                    },
                }
            },
                Plane = new Plane()
                {
                    Name = "Mriya",
                    TypePlane = new PlaneType()
                    {
                        Model = "AN225",
                        Places = 100,
                        Carrying = 20000000
                    },
                    Created = DateTime.Parse("9/11/2001"),
                    LifeTime = TimeSpan.Parse("20:59:59.9999999")
            },
                FlightNumber = "MH-17"
            }
        };
                context.Departures.AddRange(Departures);
            }
            context.SaveChanges();

            if (!context.Flights.Any())
            {
                List<Flight> Flights = new List<Flight>()
        {
            new Flight()
            {
                StartPoint = "Borispil",
                StartTime = DateTime.Parse("2018-05-01 7:34:42Z"),
                FinishPoint = "Moscow",
                FinishTime = DateTime.Parse("2018-05-01 9:34:42Z"),
                Number = "MH-17",
                Tickets = new List<Ticket>()
                {
                    new Ticket()
                    {
                        FlightNumber = "MH-17",
                        Price = 200
                    },
                     new Ticket()
                    {
                        FlightNumber = "MH-17",
                        Price = 200
                    }
                }
            }
        };
                context.Flights.AddRange(Flights);
            }
            context.SaveChanges();

       

            if (!context.PlaneTypes.Any())
            {
                List<PlaneType> PlaneTypes = new List<PlaneType>()
        {
            new PlaneType()
            {
                
                Model = "AN225",
                Places = 100,
                Carrying = 20000000
            },
             new PlaneType()
            {
                Model = "TU154",
                Places = 230,
                Carrying = 100500
            },
              new PlaneType()
            {
                Model = "Boeing 737",
                Places = 350,
                Carrying = 500100
            }
        };
                context.PlaneTypes.AddRange(PlaneTypes);
            }
            context.SaveChanges();

          

        }

    }
}
