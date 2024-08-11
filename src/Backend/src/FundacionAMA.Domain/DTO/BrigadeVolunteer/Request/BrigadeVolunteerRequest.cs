using FundacionAMA.Domain.DTO.Volunteer.Dto;
using FundacionAMA.Domain.DTO.Volunteer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.DTO.BrigadeVolunteer.Request
{
    public class BrigadeVolunteerRequest
    {
        public int BrigadeId { get; set; }
        public int VolunteerId { get; set; }
        public string Status { get; set; }
    }
}
