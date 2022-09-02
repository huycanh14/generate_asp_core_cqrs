using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Persistence.Entities;

namespace TuyenSinh_api.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /***
         * Đẩy tất cả các Dbset và dbContext vào đây => để đảm bảo được nhiều repository cùng thực hiện được trên 1 transection
         */
        private readonly CleanArchitectureContext _context;
        public IDotDangKyRepository dotDangKy { get; private set; }
        public IUserRepository user { get; private set; }
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(CleanArchitectureContext context)
        {
            _context = context;
            dotDangKy = new DotDangKyRepository(_context);
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            _repositories[typeof(User)] = new UserRepository(_context);
            _repositories[typeof(DotDangKy)] = new DotDangKyRepository(_context);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new BaseRepository<TEntity>(_context);
            }

            return (IBaseRepository<TEntity>)_repositories[type];
        }

        public void LazyLoadingEnabledFalse()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context.ChangeTracker.LazyLoadingEnabled = false;
        }

        public async Task RollbackAsync()
        {
            var _transaction = await _context.Database.BeginTransactionAsync();
            await _transaction.RollbackAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
