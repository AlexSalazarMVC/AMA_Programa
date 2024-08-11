using FundacionAMA.Domain.Entities;
using FundacionAMA.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Infrastructure.Persistence.Repository.BrigadeVolunteerRepo
{
    public class BrigadeVolunteerRepository : BaseRepository<BrigadeVoluntareer>, IBrigadeVolunteerRepository
    {
        public BrigadeVolunteerRepository(AMADbContext context) : base(context)
        {
        }
    }

    public class BrigadeVolunteerGetIdRepository : BaseRepositoryBrigadaVoluntario<BrigadeVoluntareer>, IBrigadeVolunteerGetIdRepository
    {
        public BrigadeVolunteerGetIdRepository(AMADbContext context) : base(context)
        {
        }
    }

    public class BrigadeVolunteerDatosMasivosRepository : BaseRepositoryDatosMasivos<BrigadeVoluntareer>, IBrigadeVolunteerDatosMasivosRepository
    {
        public BrigadeVolunteerDatosMasivosRepository(AMADbContext context) : base(context)
        {
        }
    }


}
