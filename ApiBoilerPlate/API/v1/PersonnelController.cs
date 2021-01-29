using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO.Request;
using ApiBoilerPlate.DTO.Response;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ApiBoilerPlate.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly ILogger<PersonnelController> _logger;
        private readonly IPersonnelManager _personManager;
        private readonly IMapper _mapper;
        public PersonnelController(IPersonnelManager personManager, IMapper mapper, ILogger<PersonnelController> logger)
        {
            _personManager = personManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonnelQueryResponse>), Status200OK)]
        public async Task<IEnumerable<PersonnelQueryResponse>> Get()
        {
            var data = await _personManager.GetAllAsync();
            var persons = _mapper.Map<IEnumerable<PersonnelQueryResponse>>(data);

            return persons;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] AddPersonnelRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState);  }

            var person = _mapper.Map<Personnel>(createRequest);
            return new ApiResponse("Record successfully created.", await _personManager.CreateAsync(person), Status201Created);     
        }

    }
}