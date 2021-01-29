using ApiBoilerPlate.API.v1;
using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO.Response;
using ApiBoilerPlate.DTO.Request;
using ApiBoilerPlate.Infrastructure.Configs;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiBoilerPlate.Test.v1
{
    public class PersonnelControllerTests
    {
        private readonly Mock<IPersonnelManager> _mockDataManager;
        private readonly PersonnelController _controller;

        public PersonnelControllerTests()
        {
            var logger = Mock.Of<ILogger<PersonnelController>>();

            var mapperProfile = new MappingProfileConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(configuration);

            _mockDataManager = new Mock<IPersonnelManager>();

            _controller = new PersonnelController(_mockDataManager.Object, mapper, logger);
        }

        private IEnumerable<Personnel> GetFakePersonnelLists()
        {
            return new List<Personnel>
            {
                new Personnel()
                {
                    Id = 1,
                    FirstName = "Cersi",
                    LastName = "Lanister",
                    DateOfBirth = Convert.ToDateTime("01/15/1980"),
                    Type = EmployeeType.Supervisor
                },
                new Personnel()
                {
                    Id = 2,
                    FirstName = "Jon",
                    LastName = "Snow",
                    DateOfBirth = Convert.ToDateTime("02/15/1990"),
                    Type = EmployeeType.Supervisor
                },
                new Personnel()
                {
                    Id = 2,
                    FirstName = "Greg",
                    LastName = "Moller",
                    DateOfBirth = Convert.ToDateTime("02/15/1990"),
                    Type = EmployeeType.Manager
                },
                new Personnel()
                {
                    Id = 2,
                    FirstName = "George",
                    LastName = "Carson",
                    DateOfBirth = Convert.ToDateTime("02/15/1990"),
                    Type = EmployeeType.Standard
                }



            };
        }

        private AddPersonnelRequest FakeCreateRequestObject()
        {
            return new AddPersonnelRequest()
            {
                FirstName = "George",
                LastName = "Carson",
                DateOfBirth = Convert.ToDateTime("02/15/1990"),
                Type = EmployeeType.Standard
            };
        }


        [Fact]
        public async Task Get_All_Personnel_Returns_Ok()
        {

            // Arrange
            _mockDataManager.Setup(manager => manager.GetAllAsync())
               .ReturnsAsync(GetFakePersonnelLists());

            // Act
            var result = await _controller.Get();

            // Assert
            var personnelResponse = Assert.IsType<List<PersonnelQueryResponse>>(result);
            Assert.Equal(4, personnelResponse.Count);
            var match = personnelResponse.FindAll(x => x.Type == EmployeeType.Standard);
            Assert.Single(match);
        }


        [Fact]
        public async Task Post_Create_Personnel_Returns_OK()
        {

            _mockDataManager.Setup(manager => manager.CreateAsync(It.IsAny<Personnel>()))
                .ReturnsAsync(It.IsAny<long>());

            var personnel = await _controller.Post(FakeCreateRequestObject());

            var response = Assert.IsType<ApiResponse>(personnel);
            Assert.Equal(201, response.StatusCode);
        }

    }
}
