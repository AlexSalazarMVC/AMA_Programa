using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace FundacionAMA.Domain.Interfaces.Repositories;

public interface IBaseRepositoryBrigadaVoluntarioDatosMasivo<T> : IDisposable where T : class
{
    Task BulkInsertAsync(IEnumerable<T> entities);
    Task SaveChangesAsync();
}


