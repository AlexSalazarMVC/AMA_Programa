using FundacionAMA.Domain.Shared.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto
{
    public class BrigadeVolunteerFilter : RequestPaginated
    {
        public int? Id { get; set; }
        public int? BrigadeId { get; set; }
        public int? VolunteerId { get; set; }
        public string? Status { get; set; }

        //agregados
        public string? ResponsableBrigada { get; set; }
        public string? NombreBrigada { get; set; }
        public string? NombreVoluntario { get; set; }

    }
}
