using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.DAL.Models;

namespace Forum.DAL.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        IList<TEntity> GetAll();
        IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(object id);
        TEntity Find(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> GetIQueryable();
        IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount);
        int Count();
        object Insert(TEntity entity, bool saveChanges = false);
        void Delete(object id, bool saveChanges = false);
        void Delete(TEntity entity, bool saveChanges = false);
        void Update(TEntity entity, bool saveChanges = false);
        TEntity Update(TEntity entity, object key, bool saveChanges = false);
        void Commit();
    }
}
