using FundacionAMA.Domain.Services;

namespace FundacionAMA.Domain.Interfaces.Services
{
    public interface ICrudServiceBrigadaVoluntario<TInput, TOutput, TFiler, TId> where TInput : class where TOutput : class where TFiler : class where TId : struct
    {
        Task<IOperationResult<TOutput>> ObtenerPorId(TId id);
    }
}