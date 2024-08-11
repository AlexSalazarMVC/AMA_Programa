using FundacionAMA.Application.Services.BrigadeApp;
using FundacionAMA.Application.Services.BrigadeVolunteerApp;
using FundacionAMA.Domain.DTO.Brigade.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Interfaces.Controller.BrigadeVolunteer;
using FundacionAMA.Domain.Interfaces.Services;
using FundacionAMA.Domain.Shared.Entities.Operation;
using FundacionAMA.Domain.Shared.Extensions.Bussines;
using FundacionAMA.Domain.Shared.Interfaces.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FundacionAMA.API.Controllers.BrigadeVolunteer
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BrigadeVolunteerController : ControllerBase, IBrigadeVolunteerController, IBrigadeVolunteerGetIdController, IBrigadeVolunteerDatosMasivosController
    {
        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerController 
        private readonly IBrigadeVolunteerAppService _brigadeVolunterAppService;

        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerGetIdController 
        private readonly IBrigadeVolunteerAppGetIdService _brigadeVolunteerAppGetIdService;

        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerDatosMasivosController
        private readonly IBrigadeVolunteerDatosMasivosService _brigadeVolunteerDatosMasivosService;




        public BrigadeVolunteerController(
            IBrigadeVolunteerAppService brigadeVolunteerAppService, 
            IBrigadeVolunteerAppGetIdService brigadeVolunteerAppGetIdService, 
            IBrigadeVolunteerDatosMasivosService brigadeVolunteerDatosMasivosService)
        {
            _brigadeVolunterAppService = brigadeVolunteerAppService;
            _brigadeVolunteerAppGetIdService = brigadeVolunteerAppGetIdService;
            _brigadeVolunteerDatosMasivosService = brigadeVolunteerDatosMasivosService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IOperationResult<BrigadeVolunteerDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetById(int id)
        {
            IOperationResult<BrigadeVolunteerDto> Result = await _brigadeVolunterAppService.GetById(id);
            return StatusCode(Result);
        }


        [HttpGet("ObtenerPorId/{id}")]
        [ProducesResponseType(typeof(IOperationResult<BrigadeVolunteerGetByIdDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            IOperationResult<BrigadeVolunteerGetByIdDto> Result = await _brigadeVolunteerAppGetIdService.ObtenerPorId(id);
            return StatusCode(Result);
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var count = await _brigadeVolunterAppService.GetCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(IOperationResult), 201)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> Create(BrigadeVolunteerRequest entity)
        {
            // Agregado
            if (entity == null)
            {
                return BadRequest("El modelo no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Fin Agregado

            IOperationResult Result = await _brigadeVolunterAppService.Create(entity.ToRequest(this));
            return StatusCode(Result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IOperationResult), 204)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> Delete(int id)
        {
            IOperationResult Result = await _brigadeVolunterAppService.Delete(id.ToRequest(this));
            if (Result.Success)
            {
                // Devuelve una respuesta con contenido cuando la operación es exitosa
                var response = new
                {
                    message = Result.Message,
                    statusCode = Result.StatusCode.ToString(), // Convertir HttpStatusCode a string
                    success = Result.Success
                };

                return StatusCode((int)HttpStatusCode.OK, response);
            }

            // Devuelve una respuesta con contenido en caso de error
            var errorResponse = new
            {
                message = Result.Message,
                statusCode = Result.StatusCode.ToString(),
                success = Result.Success
            };

            return StatusCode((int)Result.StatusCode, errorResponse);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IOperationResultList<BrigadeVolunteerDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetAll([FromQuery] BrigadeVolunteerFilter filter)
        {
            IOperationResultList<BrigadeVolunteerDto> Result = await _brigadeVolunterAppService.GetAll(filter);
            return StatusCode(Result);
        }


        [HttpGet("identificacion/{identification}")]
        [ProducesResponseType(typeof(IOperationResult<BrigadeVolunteerDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetByIdentification(string identificacion)
        {
            IOperationResult<BrigadeVolunteerDto> Result = await _brigadeVolunterAppService.GetByIdentification(identificacion);
            return StatusCode(Result);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IOperationResult), 204)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> Update(int id, BrigadeVolunteerRequest entity)
        {
            IOperationResult Result = await _brigadeVolunterAppService.Update(id, entity.ToRequest(this));
            if (Result.Success)
            {
                // Devuelve una respuesta con contenido cuando la operación es exitosa
                var response = new
                {
                    message = Result.Message,
                    statusCode = Result.StatusCode.ToString(), // Convertir HttpStatusCode a string
                    success = Result.Success
                };

                return StatusCode((int)HttpStatusCode.OK, response);
            }

            // Devuelve una respuesta con contenido en caso de error
            var errorResponse = new
            {
                message = Result.Message,
                statusCode = Result.StatusCode.ToString(),
                success = Result.Success
            };

            return StatusCode((int)Result.StatusCode, errorResponse);
        }


        //METODOS DEL CONTROLADOR QUE PERMITEN LA INSERCION, EDICION Y BORRADO MASIVO DE DATOS
        [HttpPost("BulkInsert")]
        [ProducesResponseType(typeof(IOperationResult), 201)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> CreateMasivo([FromBody] IEnumerable<BrigadeVolunteerRequest> entity)
        {
            if (entity == null || !entity.Any())
            {
                return BadRequest("Las solicitudes no pueden estar vacías");
            }

            var operationRequests = entity.Select(request => new OperationRequest<BrigadeVolunteerRequest>(
                request,
                null, // Aquí puedes pasar el objeto adecuado o dejarlo como null
                DateTime.UtcNow,
                DateTime.UtcNow,
                null // Aquí puedes pasar el usuario actual si está disponible
            )).ToList();

            var result = await _brigadeVolunteerDatosMasivosService.CreateMasivo(operationRequests);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtAction(nameof(Create), result.Message);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }

     
    }
}
