using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Interfaces.Services
{
    public interface IBrigadeVolunteerService : ICrudService<IOperationRequest<BrigadeVolunteerRequest>, BrigadeVolunteerDto, BrigadeVolunteerFilter, int>
    {
    }

    
    public interface IBrigadeVolunteerGetIdService : ICrudServiceBrigadaVoluntario<IOperationRequest<BrigadeVolunteerRequest>, BrigadeVolunteerGetByIdDto, BrigadeVolunteerFilter, int>
    {
    }


    public interface IBrigadeVolunteerDatosMasivosService 
        : ICrudServiceDatosMasivos<IEnumerable<IOperationRequest<BrigadeVolunteerRequest>>, BrigadeVolunteerDto, BrigadeVolunteerFilter, int>
    {
    }









}
