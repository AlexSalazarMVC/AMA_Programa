using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.DTO.Volunteer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto
{
    public class BrigadeVolunteerDto 
    {
        public int Id { get; set; } //

        public int BrigadeId { get; set; } //
        public string NombredeBrigada { get; set; }
        public string descripcionBrigada { get; set; }
        public int IdResponsableBrigada { get; set; } //
        public string responsableBrigada { get; set; }
        public string Status { get; set; } //
        public int VolunteerId { get; set; } //
        public string NombreVoluntario { get; set; }//

        public bool RolVoluntario { get; set; } //COMPRUEBA SI LA PERSONA EN BRIGADA VOLUNTARIO ES O NO VOLUNTARIO
    }
}
