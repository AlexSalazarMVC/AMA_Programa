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

public class BaseRepositoryDatosMasivos<T> : IBaseRepositoryBrigadaVoluntarioDatosMasivo<T> where T : class, IEntity
{
    private readonly AMADbContext _context;

    private bool disposedValue;

    
    public BaseRepositoryDatosMasivos(AMADbContext context)
    {
        _context = context;
    }


    //INICIO 
    public async Task BulkInsertAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }

    public async Task SaveChangesAsync()
    {
        _ = await _context.SaveChangesAsync(CancellationToken.None);
    }
    //FIN


    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
 

    


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