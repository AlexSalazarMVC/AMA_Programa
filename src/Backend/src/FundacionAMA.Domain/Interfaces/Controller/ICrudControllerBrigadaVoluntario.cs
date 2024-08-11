using Microsoft.AspNetCore.Mvc;

namespace FundacionAMA.Domain.Interfaces.Controller
{
    public interface ICrudControllerBrigadaVoluntario<TInputDto, TQueryFrom, TId> where TInputDto : class where TQueryFrom : class where TId : struct
    {
        Task<IActionResult> ObtenerPorId(TId id);
    }
}
