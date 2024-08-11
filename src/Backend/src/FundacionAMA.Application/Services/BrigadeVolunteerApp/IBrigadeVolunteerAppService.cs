using FundacionAMA.Domain.DTO.Brigade.Dto;
using FundacionAMA.Domain.DTO.Brigade.FilterDto;
using FundacionAMA.Domain.DTO.Brigade.Request;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Interfaces.Services;
using FundacionAMA.Domain.Shared.Interfaces.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Application.Services.BrigadeVolunteerApp
{
    public interface IBrigadeVolunteerAppService : ICrudService<IOperationRequest<BrigadeVolunteerRequest>, BrigadeVolunteerDto, BrigadeVolunteerFilter, int>
    {
    }

    public interface IBrigadeVolunteerAppGetIdService : ICrudServiceBrigadaVoluntario<IOperationRequest<BrigadeVolunteerRequest>, BrigadeVolunteerGetByIdDto, BrigadeVolunteerFilter, int>
    {
    }

    public interface IBrigadeVolunteerAppDatosMasivosService : ICrudServiceDatosMasivos<IOperationRequest<BrigadeVolunteerRequest>, BrigadeVolunteerDto, BrigadeVolunteerFilter, int>
    {
    }









}
