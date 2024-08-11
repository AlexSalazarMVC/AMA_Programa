using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.DTO.Volunteer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto
{
    public class BrigadeVolunteerGetByIdDto
    {
        public int Id { get; set; } //
        public int BrigadeId { get; set; } //
        public string NombredeBrigada { get; set; }
        public int VolunteerId { get; set; } //
        public string NombreVoluntario { get; set; }//
    }
}
