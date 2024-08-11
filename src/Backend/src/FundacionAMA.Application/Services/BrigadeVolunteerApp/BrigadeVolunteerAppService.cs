using FundacionAMA.Application.Services.BrigadeApp;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Interfaces.Services;
using FundacionAMA.Domain.Shared.Interfaces.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Application.Services.BrigadeVolunteerApp
{
    internal class BrigadeVolunteerAppService : IBrigadeVolunteerAppService, IBrigadeVolunteerAppGetIdService, IBrigadeVolunteerDatosMasivosService
    {
        private readonly IBrigadeVolunteerService _brigadeVolunteerService;
        private readonly ILogger<BrigadeVolunteerAppService> _logger;

        //campo de inyeccion de dependencia de la interfaz IBrigadeVolunteerAppGetIdService
        private readonly IBrigadeVolunteerGetIdService _brigadeVolunteerGetIdService;

        //campo de inyeccion de dependencia de la interfaz IBrigadeVolunteerDatosMasivosService
        private readonly IBrigadeVolunteerDatosMasivosService _brigadeVolunteerDatosMasivosService;

        public BrigadeVolunteerAppService(
            IBrigadeVolunteerService brigadeVolunteerService, 
            ILogger<BrigadeVolunteerAppService> logger, 
            IBrigadeVolunteerGetIdService brigadeVolunteerGetIdService, 
            IBrigadeVolunteerDatosMasivosService brigadeVolunteerDatosMasivosService)
        {
            _brigadeVolunteerService = brigadeVolunteerService;
            _logger = logger;
            _brigadeVolunteerGetIdService = brigadeVolunteerGetIdService;
            _brigadeVolunteerDatosMasivosService = brigadeVolunteerDatosMasivosService;
        }

        public async Task<IOperationResult<BrigadeVolunteerDto>> GetById(int id)
        {
            return await _brigadeVolunteerService.GetById(id);
        }

        //unico metodo de la interfaz IBrigadeVolunteerAppGetIdService 
        public async Task<IOperationResult<BrigadeVolunteerGetByIdDto>> ObtenerPorId(int id)
        {
            return await _brigadeVolunteerGetIdService.ObtenerPorId(id);
        }

        public async Task<IOperationResult> Create(IOperationRequest<BrigadeVolunteerRequest> entity)
        {
            return await _brigadeVolunteerService.Create(entity);
        }

        public async Task<IOperationResult> Delete(IOperationRequest<int> id)
        {
            return await _brigadeVolunteerService.Delete(id);
        }

        public async Task<IOperationResultList<BrigadeVolunteerDto>> GetAll(BrigadeVolunteerFilter filter)
        {
            return await _brigadeVolunteerService.GetAll(filter);
        }

        public async Task<IOperationResult<BrigadeVolunteerDto>> GetByIdentification(string identificacion)
        {
            return await _brigadeVolunteerService.GetByIdentification(identificacion);
        }

        public async Task<IOperationResult> Update(int id, IOperationRequest<BrigadeVolunteerRequest> entity)
        {
            return await _brigadeVolunteerService.Update(id, entity);
        }

        public async Task<IOperationResult<int>> GetCount()
        {
            return await _brigadeVolunteerService.GetCount();
        }

        //METODOS DE LA INTERFAZ IBrigadeVolunteerDatosMasivosService QUE PERMITEN EL INGRESO, EDICION Y BORRADO MASIVO DE DATOS EN UNA SOLA PASADA
        public async Task<IOperationResult> CreateMasivo(IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entity)
        {
            return await _brigadeVolunteerDatosMasivosService.CreateMasivo(entity);
        }

        public async Task<IOperationResult> UpdateMasivo(int id, IEnumerable<IOperationRequest<BrigadeVolunteerRequest>> entity)
        {
            return await _brigadeVolunteerDatosMasivosService.UpdateMasivo(id,entity);
        }
    }
}
