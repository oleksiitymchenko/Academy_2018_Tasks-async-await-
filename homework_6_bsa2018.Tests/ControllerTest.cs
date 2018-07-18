using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.Controllers;
using homework_5_bsa2018.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Moq;
using Xunit;

namespace homework_6_bsa2018.Tests
{
    public class ControllerTest
    {
        [Fact]
        public void Get_WhenListIsNotNull_Returns_Status200()
        {
            var ServiceMock = new Mock<IService<PilotDTO>>();
            ServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult( new List<PilotDTO>()
                { new PilotDTO {Id=1,FirstName="Sasha",LastName="Sidorov",Experience=5 } }.AsEnumerable()));

            PilotsController controller = new PilotsController(ServiceMock.Object);

            OkObjectResult message = (OkObjectResult)controller.Get().Result;

            Assert.Equal(200, message.StatusCode.Value);
        }

            [Fact]
        public void Put_ModelIsIncorrect_Returns_StatusCode400()
        {
            int Id = 1;
            var pilot = new PilotDTO { Id = 1, FirstName = "", LastName = "Sidorov", Experience = 255 };
            var ServiceMock = new Mock<IService<PilotDTO>>();

            //when model is incorrect this method will throw ArgumentNullException
            ServiceMock.Setup(s => s.UpdateAsync(Id, pilot)).Throws(new Exception());

            PilotsController controller = new PilotsController(ServiceMock.Object);

            var context = new ValidationContext(pilot, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(pilot, context, result, false);

            var message = controller.Put(Id, pilot).Result;


            Assert.False(valid);
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [Fact]
        public void Validate_WhenObjectIsIncorrect_ModelIsNotValid()
        {
            var badModel = new PilotDTO()
            { FirstName = "", LastName = "Sidorov", Experience = 5 };

            var context = new ValidationContext(badModel, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(badModel, context, result, true);

            Assert.False(valid);
        }

        [Fact]
        public void Validate_WhenObjectIscorrect_ModelIsValid()
        {
            var goodModel = new PilotDTO()
            { Id = 1, FirstName = "Vitalik", LastName = "Sidorov", Experience = 5 };

            var context = new ValidationContext(goodModel, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(goodModel, context, result, true);

            Assert.True(valid);
        }

        
    }
}
