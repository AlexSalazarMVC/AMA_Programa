using FundacionAMA.Domain.Entities;
using FundacionAMA.Domain.Interfaces.Repositories;
using FundacionAMA.Domain.Shared.Extensions.Bussines;
using FundacionAMA.Domain.Shared.Extensions.DataExtension;
using FundacionAMA.Domain.Shared.Interfaces;
using FundacionAMA.Domain.Shared.Interfaces.Operations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;
using System.Security.Cryptography;

namespace FundacionAMA.Infrastructure.Persistence.Repository;

public class BaseRepositoryBrigadaVoluntario<T> : IBaseRepositoryBrigadaVoluntario<T> where T : class, IEntity
{
    private readonly AMADbContext _context;

    private bool disposedValue;

    
    public BaseRepositoryBrigadaVoluntario(AMADbContext context)
    {
        _context = context;
    }



    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
 

    //INICIO
    public async Task<T?> GetByIdAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, params object[] keyValues)
    {
        var query = _context.Set<T>().AsQueryable();
        if (include != null)
        {
            query = include(query);
        }
        return await query.SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == (int)keyValues[0]);
    }
    //FIN


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: eliminar el estado administrado (objetos administrados)
            }

            // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            // TODO: establecer los campos grandes como NULL
            disposedValue = true;
        }
    }

    
}