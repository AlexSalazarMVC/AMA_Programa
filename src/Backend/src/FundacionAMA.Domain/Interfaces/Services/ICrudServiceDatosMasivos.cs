using FundacionAMA.Domain.Services;

namespace FundacionAMA.Domain.Interfaces.Services
{
   
    public interface ICrudServiceDatosMasivos<TInput, TOutput, TFiler, TId> where TInput : class where TOutput : class where TFiler : class where TId : struct
    {
        Task<IOperationResult> CreateMasivo(TInput entity);

        Task<IOperationResult> UpdateMasivo(TId id, TInput entity);

        //Task<IOperationResult> DeleteMasivo(IOperationRequest<TId> id);
    }
}