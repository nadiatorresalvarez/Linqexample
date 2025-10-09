using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lab8_NadiaTorres.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly dbContextnLab8 _ctx;
    private IDbContextTransaction? _transaction;

    public IClientRepository Clients { get; }
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IOrderDetailRepository OrderDetails { get; }

    public UnitOfWork(dbContextnLab8 ctx)
    {
        _ctx = ctx;
        // Repositorios existentes reutilizados, comparten el mismo DbContext
        Clients = new ClientRepository(_ctx);
        Products = new ProductRepository(_ctx);
        Orders = new OrderRepository(_ctx);
        OrderDetails = new OrderDetailRepository(_ctx);
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await _ctx.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> CommitAsync() => await _ctx.SaveChangesAsync();

    public void Dispose()
    {
        _transaction?.Dispose();
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}