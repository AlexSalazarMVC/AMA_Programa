using AutoMapper;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Net;

namespace FundacionAMA.Domain.Services
{
    internal class BrigadeService : IBrigadeService
    {
        private readonly IBrigadeRepository _brigadeRepository;
        private readonly ILogger<BrigadeService> _logger;
        private readonly IMapper _mapper;

        //inicio
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IBrigadeVolunteerRepository _brigadeVolunteerRepository;
        //fin


        public BrigadeService(
            IBrigadeRepository brigadeRepository, 
            ILogger<BrigadeService> logger, 
            IMapper mapper, 
            IVolunteerRepository volunteerRepository,//añadido 
            IBrigadeVolunteerRepository brigadeVolunteerRepository)//añadido
        {
            _brigadeRepository = brigadeRepository;
            _logger = logger;
            _mapper = mapper;
            _volunteerRepository = volunteerRepository;//añadido
            _brigadeVolunteerRepository = brigadeVolunteerRepository;//añadido
        }

        public async Task<IOperationResult> Create(IOperationRequest<BrigadeRequest> entity)
        {
            try
            {
                _logger.LogInformation("Creando brigada");
                Brigade request = entity.Data.MapTo<Brigade>();

                // Verificar si el registro ya existe
                var exists = await _brigadeRepository.ExistsAscyn(b => b.Name == request.Name);
                if (exists)
                {
                    _logger.LogWarning("El registro ya existe en la tabla Brigada");
                    return new OperationResult(HttpStatusCode.Conflict, "El Registro ya Existe!, ¡Crea uno distinto!");
                }

                // Verificar si el campo nombre esta vacio o es nulo
                if (request.Name.IsNullOrEmpty())
                {
                    _logger.LogWarning("¡Campos vacios!¡Todos los campos del formulario son requeridos!");
                    return new OperationResult(HttpStatusCode.InternalServerError, "¡Campos vacios!¡Todos los campos del formulario son requeridos!");
                }
                // Guardar el registro en la entidad Brigade
                await _brigadeRepository.InsertAsync(request);
                await _brigadeRepository.SaveChangesAsync(entity);
                _logger.LogInformation("Brigada creada exitosamente");

                ////desde aqui
                // Verificar si el PersonId se encuentra en la entidad Volunteer
                var volunteer = await _volunteerRepository.GetByIdAsync(request.PersonId);
                if (volunteer != null)
                {
                    // Crear un nuevo registro en BrigadeVolunteer
                    var brigadeVolunteer = new BrigadeVoluntareer
                    {
                        Id = 0,
                        BrigadeId = request.Id,
                        VolunteerId = volunteer.PersonId
                    };

                    await _brigadeVolunteerRepository.InsertAsync(brigadeVolunteer);
                    await _brigadeVolunteerRepository.SaveChangesAsync();
                    _logger.LogInformation("Registro en BrigadeVolunteer creado exitosamente");
                }
                ////hasta aqui

                return new OperationResult(HttpStatusCode.Created, "Brigada creada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear brigada");
                return await ex.ToResultAsync();
            }
        }

        public async Task<IOperationResult> Delete(IOperationRequest<int> id)
        {
            try
            {
                _logger.LogInformation("Eliminando brigada");
                Brigade? brigada = await _brigadeRepository.GetByIdAsync(id.Data);
                if (brigada == null)
                {
                    return new OperationResult(System.Net.HttpStatusCode.NotFound, "No se encontro la brigada");
                }
                //inicio
                await _brigadeRepository.EliminarRegistroAsync(brigada); //ELIMINA COMPLETAMENTE EL REGISTRO
                //await _brigadeRepository.DeleteAsync(brigada); //aplica soft delete
                //fin
                await _brigadeRepository.SaveChangesAsync(id);
                _logger.LogInformation("Brigada eliminada con exito");
                return new OperationResult(System.Net.HttpStatusCode.NoContent, "La brigada fue eliminada con exito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar brigada");
              //return await ex.ToResultAsync();
              return new OperationResult(HttpStatusCode.InternalServerError, "¡Esta Brigada tiene voluntarios asociados!¡Elimina los voluntarios asociados a la brigada e intenta de nuevo!\"");
            }
        }

        public Task<IOperationResultList<BrigadeDto>> GetAll(BrigadeFilter filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo brigadas");
                Task<IOperationResultList<BrigadeDto>> brigada = _brigadeRepository
                    .All
                    .Where(GetFilterBrigade(filter))
                    .ToResultListAsync<Brigade, BrigadeDto>(filter.Offset, filter.Take);
                _logger.LogInformation("Brigadas obtenidas con exito");
                return brigada;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener brigadas");
                return ex.ToResultListAsync<BrigadeDto>();
            }
        }

        public async Task<IOperationResult<int>> GetCount()
        {
            try
            {
                _logger.LogInformation("Contando brigadas");
                var count = await _brigadeRepository.All.CountAsync();
                _logger.LogInformation("Contador de brigadas obtenido con éxito");

                // Retorna un resultado exitoso con el conteo
                return new OperationResult<int>(HttpStatusCode.OK, result: count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al contar brigadas");

                // Retorna un resultado fallido con un mensaje de error
                return new OperationResult<int>(HttpStatusCode.InternalServerError, message: "Error al contar brigadas");
            }
        }

        private static Expression<Func<Brigade, bool>> GetFilterBrigade(BrigadeFilter filter)
        {
            return e => e.Active && (
                        (string.IsNullOrWhiteSpace(filter.Description) || e.Description.Contains(filter.Description)) &&
                        (string.IsNullOrWhiteSpace(filter.Name) || e.Name.Contains(filter.Name)) &&
                        (filter.CompanyId == null || e.CompanyId == filter.CompanyId) &&
                        (!filter.Start.HasValue || e.Start == filter.Start) &&
                        (!filter.End.HasValue || e.End == filter.End) &&
                        (filter.PersonId == null || e.PersonId == filter.PersonId) &&
                        (filter.Id == null || e.Id == filter.Id));
        }

        public async Task<IOperationResult<BrigadeDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo brigada");
                Brigade? brigada = await _brigadeRepository.GetByIdAsync(id);
                if (brigada == null)
                {
                    return new OperationResult<BrigadeDto>(System.Net.HttpStatusCode.NotFound, "No se encontro la brigada");
                }

                _logger.LogInformation("Brigada obtenida con exito");
                return await brigada.ToResultAsync<Brigade, BrigadeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener brigada");
                return await ex.ToResultAsync<BrigadeDto>();
            }
        }

        //esto lo modifico
        public async Task<IOperationResult<BrigadeDto>> GetByIdentification(string identification)
        {
            try
            {
                _logger.LogInformation("Obteniendo brigada");
                Brigade? brigada = await _brigadeRepository.GetByIdAsync(identification);
                if (brigada == null)
                {
                    return new OperationResult<BrigadeDto>(System.Net.HttpStatusCode.NotFound, "No se encontro la brigada");
                }

                _logger.LogInformation("Brigada obtenida con exito");
                return await brigada.ToResultAsync<Brigade, BrigadeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener brigada");
                return await ex.ToResultAsync<BrigadeDto>();
            }
        }
        //

        public async Task<IOperationResult> Update(int id, IOperationRequest<BrigadeRequest> entity)
        {
            try
            {
                _logger.LogInformation("Actualizando brigada");
                Brigade? BrigadaExistente = await _brigadeRepository.GetByIdAsync(id);
                if (BrigadaExistente == null)
                {
                    return new OperationResult(HttpStatusCode.NotFound, "No se encontro la brigada");
                }
                Brigade request = entity.Data.MapTo<Brigade>();

                //var exists = await _brigadeRepository.ExistsAscyn(b => b.Name == request.Name && request.Id != id);
                //if (exists)
                //{
                //    _logger.LogWarning("El registro ya existe en la tabla BrigadaVoluntario");
                //    return new OperationResult(HttpStatusCode.Conflict, "");
                //}


                _mapper.Map(entity.Data, BrigadaExistente);
                await _brigadeRepository.UpdateAsync(BrigadaExistente);
                await _brigadeRepository.SaveChangesAsync();

                _logger.LogInformation("Brigada actualizada con exito");
                return new OperationResult(HttpStatusCode.NoContent, "Brigada actualizada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar brigada");
                return await ex.ToResultAsync();
            }
        }

        
    }
}