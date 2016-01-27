#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ideal.Core.Common.Paging;
using Ideal.Core.Interfaces.Data;
using Ideal.Core.Interfaces.Paging;
using Ideal.Core.Model;

namespace Ideal.Infrastructure.Data
{
    /// <summary>
    /// An abstract baseclass handling basic CRUD operations against the context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : DomainObject
    {
        protected readonly IDbSet<T> _dbset;
        protected readonly IDatabaseFactory _databaseFactory;
        private IDataContext _context;

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
            this._dbset = this.DataContext.DbSet<T>();
        }

        public virtual IQueryable<T> Query
        {
            get { return _dbset; }
        }

        public IDataContext DataContext
        {
            get { return this._context ?? (this._context = this._databaseFactory.Get()); }
        }

        protected string EntitySetName { get; set; }

        public virtual void SaveOrUpdate(T entity)
        {
            if (UnitOfWork.IsPersistent(entity))
            {
                this.DataContext.Entry(entity).State = EntityState.Modified;
            }
            else
                this._dbset.Add(entity);
        }

        public virtual T GetById(int id)
        {
            return this.Query.SingleOrDefault(e => e.Id == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.Query;
        }

        public virtual IQueryable<T> GetAllReadOnly()
        {
            return this.Query.AsNoTracking();
        }

        public virtual void Delete(T entity)
        {
            this._dbset.Remove(entity);
        }

        public void BulkDelete(List<int> keys)
        {
            keys.ForEach(i => Delete(GetById(i)));
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100)
        {
            return this.Query.Where(expression).Take(maxHits);
        }

        public IPage<T> Page(int page = 1, int pageSize = 10)
        {
            var internalPage = page - 1;
            var data = this.Query.OrderByDescending(k => k.Created).Skip(pageSize * internalPage).Take(pageSize).AsEnumerable();
            return new Page<T>(data, this._dbset.Count(), pageSize, page);
        }

        public long Count()
        {
            return _dbset.LongCount();
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            return expression != null ? _dbset.Where(expression).LongCount() : Count();
        }

        public void Dispose()
        {
            DataContext.ObjectContext().Dispose();
        }
    }
}