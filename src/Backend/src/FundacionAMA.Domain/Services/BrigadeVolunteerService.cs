using AutoMapper;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Entities;
using FundacionAMA.Domain.Interfaces.Repositories;
using FundacionAMA.Domain.Shared.Extensions.Bussines;
using FundacionAMA.Domain.Shared.Extensions.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Services
{
    internal class BrigadeVolunteerService : IBrigadeVolunteerService, IBrigadeVolunteerGetIdService, IBrigadeVolunteerDatosMasivosService
    {
        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerService
        private readonly IBrigadeVolunteerRepository _brigadeVolunteerRepository;
        private readonly ILogger<BrigadeVolunteerService> _logger;
        private readonly IMapper _mapper;

        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerGetIdService
        private readonly IBrigadeVolunteerGetIdRepository _brigadeVolunteerGetIdRepository;


        //campo de inyeccion de dependencias de la interfaz IBrigadeVolunteerDatosMasivosService
        private readonly IBrigadeVolunteerDatosMasivosRepository _brigadeVolunteerDatosMasivosRepository;

        public BrigadeVolunteerService(
            IBrigadeVolunteerRepository brigadeVolunteerRepository, 
            ILogger<BrigadeVolunteerService> logger, 
            IMapper mapper, 
            IBrigadeVolunteerGetIdRepository brigadeVolunteerGetIdRepository, 
            IBrigadeVolunteerDatosMasivosRepository brigadeVolunteerDatosMasivosRepository)
        {
            _brigadeVolunteerRepository = brigadeVolunteerRepository;
            _logger = logger;
            _mapper = mapper;
            _brigadeVolunteerGetIdRepository = brigadeVolunteerGetIdRepository;
            _brigadeVolunteerDatosMasivosRepository = brigadeVolunteerDatosMasivosRepository;
        }

        public async Task<IOperationResult<BrigadeVolunteerDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo dato de BrigadaVoluntario");

                var entity = await _brigadeVolunteerRepository.GetByIdAsync(
            query => query
                        .Include(bv => bv.Brigade)
                        .ThenInclude(b => b.Person)
                        .Include(bv => bv.Volunteer)
                        .ThenInclude(v => v.Person),
            id);
                if (entity == null)
                {
                    return new OperationResult<BrigadeVolunteerDto>(HttpStatusCode.NotFound, "Registro no encontrado");
                }

                _logger.LogInformation("Registro obtenido exitosamente");
                var dto = _mapper.Map<BrigadeVolunteerDto>(entity);
                return new OperationResult<BrigadeVolunteerDto>(HttpStatusCode.OK, result: dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener registro de BrigadaVoluntario por id");
                return new OperationResult<BrigadeVolunteerDto>(ex);
            }

        }

        //unico metodo de la Interfaz IBrigadeVolunteerGetIdService
        public async Task<IOperationResult<BrigadeVolunteerGetByIdDto>> ObtenerPorId(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo dato de BrigadaVoluntario");

                var entity = await _brigadeVolunteerGetIdRepository.GetByIdAsync(
            query => query
                        .Include(bv => bv.Brigade)
                        .ThenInclude(b => b.Person)
                        .Include(bv => bv.Volunteer)
                        .ThenInclude(v => v.Person),
            id);
                if (entity == null)
                {
                    return new OperationResult<BrigadeVolunteerGetByIdDto>(HttpStatusCode.NotFound, "Registro no encontrado");
                }

                _logger.LogInformation("Registro obtenido exitosamente");
                var dto = _mapper.Map<BrigadeVolunteerGetByIdDto>(entity);
                return new OperationResult<BrigadeVolunteerGetByIdDto>(HttpStatusCode.OK, result: dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener registro de BrigadaVoluntario por id");
                return new OperationResult<BrigadeVolunteerGetByIdDto>(ex);
            }
        }


        //public async Task<IOperationResult> Create(IOperationRequest<BrigadeVolunteerRequest> entity)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Creando registro en la tabla BrigadaVoluntario");
        //        BrigadeVoluntareer request = entity.Data.MapTo<BrigadeVoluntareer>();

        //        //Verificar si el registro ya existe en la tabla BrigadaVoluntario
        //        var exists = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.BrigadeId == request.BrigadeId && bv.VolunteerId == request.VolunteerId);
        //        if (exists)
        //        {
        //            _logger.LogWarning("El registro ya existe en la tabla BrigadaVoluntario");
        //            return new OperationResult(HttpStatusCode.Conflict, "El Registro ya Existe, Crea uno distinto");
        //        }


        //        // Verificar si BrigadeId o VolunteerId son iguales a cero
        //        if (request.BrigadeId == 0 || request.VolunteerId == 0)
        //        {
        //            _logger.LogWarning("¡Campos vacios!¡Todos los campos del formulario son requeridos!");
        //            return new OperationResult(HttpStatusCode.InternalServerError, "¡Campos vacios!¡Todos los campos del formulario son requeridos!");
        //        }

        //        //Verificar si el voluntario que se desea añadir en la brigada no ha sido agregado anteriormente

        //        var voluntarioexiste = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.Brigade.PersonId == request.VolunteerId);
        //        if (voluntarioexiste)
        //        {
        //            _logger.LogWarning("El voluntario ya existe en esta brigada. Prueba añadiendo un voluntario diferente");
        //            return new OperationResult(HttpStatusCode.Conflict, "El voluntario ya está añadido en esta brigada");
        //        }



        //        //Si no existen registros duplicados ni hay otros problemas se guarda el 
        //        await _brigadeVolunteerRepository.InsertAsync(request);
        //        await _brigadeVolunteerRepository.SaveChangesAsync(entity);
        //        _logger.LogInformation("Registro en tabla BrigadaVoluntario creado con éxito");
        //        return new OperationResult(HttpStatusCode.Created, "La BrigadaVoluntario fue creada con éxito");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error al crear BrigadaVoluntario");
        //        return await ex.ToResultAsync();
        //    }

        //}

        public async Task<IOperationResult> Create(IOperationRequest<BrigadeVolunteerRequest> entity)
        {
            try
            {
                _logger.LogInformation("Creando registro en la tabla BrigadaVoluntario");
                BrigadeVoluntareer request = entity.Data.MapTo<BrigadeVoluntareer>();

                // Verificar si el registro ya existe en la tabla BrigadaVoluntario
                var exists = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.BrigadeId == request.BrigadeId && bv.VolunteerId == request.VolunteerId);
                if (exists)
                {
                    _logger.LogWarning($"El registro con BrigadeId: {request.BrigadeId} y VolunteerId: {request.VolunteerId} ya existe en la tabla BrigadaVoluntario");
                    return new OperationResult(HttpStatusCode.Conflict, $"El registro con BrigadeId: {request.BrigadeId} y VolunteerId: {request.VolunteerId} ya existe.");
                }

                // Verificar si BrigadeId o VolunteerId son iguales a cero
                if (request.BrigadeId == 0 || request.VolunteerId == 0)
                {
                    _logger.LogWarning($"Campos vacíos en el registro: BrigadeId: {request.BrigadeId}, VolunteerId: {request.VolunteerId}");
                    return new OperationResult(HttpStatusCode.BadRequest, $"¡Campos vacíos! BrigadeId: {request.BrigadeId} y VolunteerId: {request.VolunteerId} son requeridos.");
                }

                // Verificar si el voluntario que se desea añadir en la brigada no ha sido agregado anteriormente
                var voluntarioexiste = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.Brigade.PersonId == request.VolunteerId);
                if (voluntarioexiste)
                {
                    _logger.LogWarning($"El voluntario con VolunteerId: {request.VolunteerId} ya existe en la brigada con BrigadeId: {request.BrigadeId}");
                    return new OperationResult(HttpStatusCode.Conflict, $"El voluntario con VolunteerId: {request.VolunteerId} ya está añadido en esta brigada con BrigadeId: {request.BrigadeId}");
                }

                // Si no existen registros duplicados ni hay otros problemas se guarda el registro
                await _brigadeVolunteerRepository.InsertAsync(request);
                await _brigadeVolunteerRepository.SaveChangesAsync(entity);
                _logger.LogInformation($"Registro creado con éxito en la tabla BrigadaVoluntario: BrigadeId: {request.BrigadeId}, VolunteerId: {request.VolunteerId}");
                return new OperationResult(HttpStatusCode.Created, $"La BrigadaVoluntario con BrigadeId: {request.BrigadeId} y VolunteerId: {request.VolunteerId} fue creada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear BrigadaVoluntario");
                return await ex.ToResultAsync();
                
            }
        }


        public async Task<IOperationResult> Delete(IOperationRequest<int> id)
        {
            try
            {
                _logger.LogInformation("Eliminando Registro en BrigadaVoluntario");
                BrigadeVoluntareer? brigada = await _brigadeVolunteerRepository.GetByIdAsync(id.Data);
                if (brigada == null)
                {
                    return new OperationResult(HttpStatusCode.NotFound, "Registro no encontrado");
                }
                await _brigadeVolunteerRepository.EliminarRegistroAsync(brigada); //ELIMINA COMPLETAMENTE EL REGISTRO
                //await _brigadeVolunteerRepository.DeleteAsync(brigada); //HACE UN SOFT DELETE E INACTIVA EL ID Y EL CAMPO ACTIVE
                await _brigadeVolunteerRepository.SaveChangesAsync(id);
                _logger.LogInformation("Registro eliminado exitosamente");
                return new OperationResult(HttpStatusCode.NoContent, "Registro eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar registro");
                return await ex.ToResultAsync();
            }
        }



        public Task<IOperationResultList<BrigadeVolunteerDto>> GetAll(BrigadeVolunteerFilter filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo registros");
                Task<IOperationResultList<BrigadeVolunteerDto>> brigada = _brigadeVolunteerRepository
                    .All
                    .Where(GetFilterBrigadeVolunteer(filter))
                    .ToResultListAsync<BrigadeVoluntareer, BrigadeVolunteerDto>(filter.Offset, filter.Take);
                _logger.LogInformation("Registros obtenidos exitosamente");
                return brigada;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener brigadas");
                return ex.ToResultListAsync<BrigadeVolunteerDto>();
            }
        }

        private static Expression<Func<BrigadeVoluntareer, bool>> GetFilterBrigadeVolunteer(BrigadeVolunteerFilter filter)
        {
            return e => e.Active && (
                        (filter.Id == null || e.Id == filter.Id) &&
                        (filter.BrigadeId == null || e.BrigadeId == filter.BrigadeId) &&
                        (string.IsNullOrWhiteSpace(filter.NombreBrigada) || e.Brigade.Name.Contains(filter.NombreBrigada)) &&
                        (string.IsNullOrWhiteSpace(filter.ResponsableBrigada) || e.Brigade.Person.NameCompleted.Contains(filter.ResponsableBrigada)) &&
                       
                        (filter.VolunteerId == null || e.VolunteerId == filter.VolunteerId)) &&
                        (string.IsNullOrWhiteSpace(filter.NombreVoluntario) || e.Volunteer.Person.NameCompleted.Contains(filter.NombreVoluntario)) &&
                        (string.IsNullOrWhiteSpace(filter.Status) || e.Status.Contains(filter.Status));
        }


        public async Task<IOperationResult<BrigadeVolunteerDto>> GetByIdentification(string identificacion)
        {
            try
            {
                _logger.LogInformation("Obteniendo brigada");
                BrigadeVoluntareer? brigada = await _brigadeVolunteerRepository.GetByIdAsync(identificacion);
                if (brigada == null)
                {
                    return new OperationResult<BrigadeVolunteerDto>(HttpStatusCode.NotFound, "No se encontro la brigada");
                }

                _logger.LogInformation("Brigada obtenida con exito");
                return await brigada.ToResultAsync<BrigadeVoluntareer, BrigadeVolunteerDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener brigada");
                return await ex.ToResultAsync<BrigadeVolunteerDto>();
            }
        }

        public async Task<IOperationResult> Update(int id, IOperationRequest<BrigadeVolunteerRequest> entity)
        {
            try
            {
                _logger.LogInformation("Actualizando brigada");
                BrigadeVoluntareer? BrigadaVolExistente = await _brigadeVolunteerRepository.GetByIdAsync(id);
                if (BrigadaVolExistente == null)
                {
                    return new OperationResult(HttpStatusCode.NotFound, "No se encontro el registro");
                }



                _mapper.Map(entity.Data, BrigadaVolExistente);

                await _brigadeVolunteerRepository.UpdateAsync(BrigadaVolExistente);
                await _brigadeVolunteerRepository.SaveChangesAsync();


                _logger.LogInformation("Registro actualizado exitosamente");
                return new OperationResult(HttpStatusCode.NoContent, "Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar registro");
                return await ex.ToResultAsync();
            }
        }

        public async Task<IOperationResult<int>> GetCount()
        {
            try
            {
                _logger.LogInformation("Contando datos de tabla brigadaVoluntario");
                var count = await _brigadeVolunteerRepository.All.CountAsync();
                _logger.LogInformation("Contador de tabla brigadaVoluntario obtenido con éxito");

                // Retorna un resultado exitoso con el conteo
                return new OperationResult<int>(HttpStatusCode.OK, result: count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al contar datos de tabla BrigadaVoluntario");

                // Retorna un resultado fallido con un mensaje de error
                return new OperationResult<int>(HttpStatusCode.InternalServerError, message: "Error al contar datos de tabla brigadaVoluntario");
            }
        }

        //Desde aquí inician los metodos que permiten insertar, actualizar y eliminar datos multiples (insert massive)


        //public async Task<IOperationResult> CreateMasivo(IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entities)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Creando registros en la tabla BrigadaVoluntario");

        //        var brigadeVolunteerEntities = new List<BrigadeVoluntareer>();

        //        foreach (var entity in entities)
        //        {
        //            BrigadeVoluntareer request = entity.Data.MapTo<BrigadeVoluntareer>();

        //            // Verificar si el registro ya existe en la tabla BrigadaVoluntario
        //            var exists = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.BrigadeId == request.BrigadeId && bv.VolunteerId == request.VolunteerId);
        //            if (exists)
        //            {

        //                _logger.LogWarning("El registro ya existe en la tabla BrigadaVoluntario");
        //                return new OperationResult(HttpStatusCode.Conflict, "Uno o más registros ya existen, crea otros distintos");
        //            }

        //            // Verificar si BrigadeId o VolunteerId son iguales a cero
        //            if (request.BrigadeId == 0 || request.VolunteerId == 0)
        //            {
        //                _logger.LogWarning("¡Campos vacios!¡Todos los campos del formulario son requeridos!");
        //                return new OperationResult(HttpStatusCode.InternalServerError, "¡Campos vacios!¡Todos los campos del formulario son requeridos!");
        //            }

        //            // Verificar si el voluntario que se desea añadir en la brigada no ha sido agregado anteriormente
        //            var voluntarioexiste = await _brigadeVolunteerRepository.ExistsAscyn(bv => bv.Brigade.PersonId == request.VolunteerId);
        //            if (voluntarioexiste)
        //            {
        //                _logger.LogWarning("El voluntario ya existe en esta brigada. Prueba añadiendo un voluntario diferente");
        //                return new OperationResult(HttpStatusCode.Conflict, "Uno o más voluntarios ya están añadidos en esta brigada");
        //            }

        //            brigadeVolunteerEntities.Add(request);
        //        }

        //        // Si no existen registros duplicados ni hay otros problemas, se guarda el registro
        //        await _brigadeVolunteerDatosMasivosRepository.BulkInsertAsync(brigadeVolunteerEntities);
        //        await _brigadeVolunteerDatosMasivosRepository.SaveChangesAsync();

        //        _logger.LogInformation("Registros en tabla BrigadaVoluntario creados con éxito");
        //        return new OperationResult(HttpStatusCode.Created, "Las BrigadaVoluntario fueron creadas con éxito");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error al crear BrigadaVoluntario");
        //        return await ex.ToResultAsync();
        //    }
        //}


        //public async Task<IOperationResult> CreateMasivo(IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entities)
        //{
        //    var results = new List<string>();
        //    var toInsert = new List<BrigadeVoluntareer>();

        //    foreach (var entity in entities)
        //    {
        //        _logger.LogInformation("Validando registro en la tabla BrigadeVolunteer");

        //        var request = entity.Data.MapTo<BrigadeVoluntareer>();

        //        // Validación: Verificar si BrigadeId o VolunteerId son iguales a cero
        //        if (request.BrigadeId == 0 || request.VolunteerId == 0)
        //        {
        //            results.Add($"Registro con BrigadeId {request.BrigadeId} y VolunteerId {request.VolunteerId} tiene valores inválidos (Id no puede ser cero).");
        //            continue;
        //        }

        //        // Validación: Verificar si el registro ya existe en la tabla BrigadeVolunteer
        //        var existingRecord = await _brigadeVolunteerRepository.FirstOrDefaultAsync(
        //            bv => bv.BrigadeId == request.BrigadeId && bv.VolunteerId == request.VolunteerId);

        //        if (existingRecord != null)
        //        {
        //            results.Add($"Registro con BrigadeId {request.BrigadeId} y VolunteerId {request.VolunteerId} ya existe.");
        //            continue;
        //        }

        //        // Si todas las validaciones pasan, agregar el registro a la lista de inserción
        //        toInsert.Add(request);
        //    }

        //    if (toInsert.Any())
        //    {
        //        await _brigadeVolunteerDatosMasivosRepository.BulkInsertAsync(toInsert);
        //        await _brigadeVolunteerDatosMasivosRepository.SaveChangesAsync();

        //        _logger.LogInformation("Registros en la tabla BrigadeVolunteer creados con éxito");
        //        results.Add("Registros creados con éxito");
        //    }
        //    else
        //    {
        //        _logger.LogWarning("No se crearon registros debido a errores de validación");
        //    }

        //    return new OperationResult(HttpStatusCode.Created, string.Join("; ", results));
        //}

        public async Task<IOperationResult> CreateMasivo(IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entities)
        {
            var results = new List<string>();
            var toInsert = new List<BrigadeVoluntareer>();
            HttpStatusCode statusCode = HttpStatusCode.OK;

            foreach (var entity in entities)
            {
                _logger.LogInformation("Validando registro en la tabla BrigadeVolunteer");

                var request = entity.Data.MapTo<BrigadeVoluntareer>();

                // Validación: Verificar si BrigadeId o VolunteerId son iguales a cero
                //if (request.BrigadeId == 0 || request.VolunteerId == 0)
                //{
                //    results.Add($"Registro con BrigadeId {request.BrigadeId} y VolunteerId {request.VolunteerId} tiene valores inválidos (Id no puede ser cero).");
                //    statusCode = HttpStatusCode.BadRequest;
                //    continue;
                //}

                // Validación: Verificar si el registro ya existe en la tabla BrigadeVolunteer
                var existingRecord = await _brigadeVolunteerRepository.FirstOrDefaultAsync(
                    bv => bv.BrigadeId == request.BrigadeId && bv.VolunteerId == request.VolunteerId);
                


                if (existingRecord != null)
                {
                    

                    results.Add($"Registro con BrigadeId {request.BrigadeId} y VolunteerId {request.VolunteerId} ya existe.");
                    statusCode = HttpStatusCode.Conflict;
                    continue;
                }

                // Si todas las validaciones pasan, agregar el registro a la lista de inserción
                toInsert.Add(request);
            }

            if (statusCode == HttpStatusCode.OK)
            {
                if (toInsert.Any())
                {
                    await _brigadeVolunteerDatosMasivosRepository.BulkInsertAsync(toInsert);
                    await _brigadeVolunteerDatosMasivosRepository.SaveChangesAsync();

                    _logger.LogInformation("Registros en la tabla BrigadeVolunteer creados con éxito");
                    results.Add("Registros creados con éxito");
                }
                else
                {
                    _logger.LogWarning("No se crearon registros debido a errores de validación");
                    results.Add("No se crearon registros debido a errores de validación");
                    statusCode = HttpStatusCode.NoContent;
                }
            }

            return new OperationResult(statusCode, string.Join("; ", results));
        }






        public Task<IOperationResult> UpdateMasivo(int id, IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IOperationResult> UpdateMasivo(IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entities)
        {
            try
            {
                _logger.LogInformation("Actualizando múltiples registros en la tabla BrigadaVoluntario");

                foreach (var entity in entities)
                {
                    // Usamos BrigadeId y VolunteerId para identificar los registros existentes
                    var existingRecord = await _brigadeVolunteerRepository.FirstOrDefaultAsync(
                        bv => bv.BrigadeId == entity.Data.BrigadeId && bv.VolunteerId == entity.Data.VolunteerId);

                    if (existingRecord != null)
                    {
                        _mapper.Map(entity.Data, existingRecord);
                        await _brigadeVolunteerRepository.UpdateAsync(existingRecord);
                    }
                    else
                    {
                        _logger.LogWarning($"No se encontró el registro para BrigadeId {entity.Data.BrigadeId} y VolunteerId {entity.Data.VolunteerId}");
                        return new OperationResult(HttpStatusCode.NotFound, $"No se encontró el registro para BrigadeId {entity.Data.BrigadeId} y VolunteerId {entity.Data.VolunteerId}");
                    }
                }

                await _brigadeVolunteerRepository.SaveChangesAsync();
                _logger.LogInformation("Registros actualizados exitosamente");
                return new OperationResult(HttpStatusCode.NoContent, "Registros actualizados exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar registros");
                return await ex.ToResultAsync();
            }
        }

    }
}
