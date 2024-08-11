using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Interfaces.Repositories
{
    public interface IBrigadeVolunteerRepository : IBaseRepository<BrigadeVoluntareer>
    {
    }

    public interface IBrigadeVolunteerGetIdRepository : IBaseRepositoryBrigadaVoluntario<BrigadeVoluntareer>
    {
    }

    public interface IBrigadeVolunteerDatosMasivosRepository : IBaseRepositoryBrigadaVoluntarioDatosMasivo<BrigadeVoluntareer>
    {
    }




}
