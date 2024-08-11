using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Interfaces.Controller
{
    public interface ICrudControllerBrigadaVoluntarioDatosMasivos<TInputDto, TQueryFrom, TId> where TInputDto : class where TQueryFrom : class where TId : struct
    {
        Task<IActionResult> CreateMasivo(TInputDto entity);
        //Task<IActionResult> UpdateMasivo(TId id, TInputDto entity);
        //Task<IActionResult> DeleteMasivo(TId id);
    }
}
