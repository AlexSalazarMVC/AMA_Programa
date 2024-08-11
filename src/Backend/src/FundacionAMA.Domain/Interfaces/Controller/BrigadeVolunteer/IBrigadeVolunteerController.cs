using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Interfaces.Controller.BrigadeVolunteer
{
    public interface IBrigadeVolunteerController : ICrudController<BrigadeVolunteerRequest, BrigadeVolunteerFilter, int>
    {
    }


    public interface IBrigadeVolunteerGetIdController : ICrudControllerBrigadaVoluntario<BrigadeVolunteerRequest, BrigadeVolunteerFilter, int>
    {
    }

    public interface IBrigadeVolunteerDatosMasivosController : ICrudControllerBrigadaVoluntarioDatosMasivos<IEnumerable<BrigadeVolunteerRequest>, BrigadeVolunteerFilter, int>
    {
    }



}
