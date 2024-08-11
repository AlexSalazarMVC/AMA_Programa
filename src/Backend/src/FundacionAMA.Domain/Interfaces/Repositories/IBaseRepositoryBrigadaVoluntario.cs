using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace FundacionAMA.Domain.Interfaces.Repositories;

public interface IBaseRepositoryBrigadaVoluntario<T> : IDisposable where T : class
{
    Task<T?> GetByIdAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, params object[] keyValues);
}


