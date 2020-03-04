using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.DAL.EF.Context;
using Forum.DAL.Models;
using Forum.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext context;
        private DbSet<TEntity> entities;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }


        public IList<TEntity> GetAll()
        {
            return entities.ToList();
        }

        public IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match)
        {
            return entities.Where(match).ToList();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = entities;
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }
            return queryable;
        }

        public TEntity GetById(object id)
        {
            return entities.Find(id);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return entities.SingleOrDefault(match);
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return entities.AsQueryable<TEntity>();
        }

        public IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = entities.Count();
            return entities.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        public int Count()
        {
            return entities.Count();
        }

        public virtual object Insert(TEntity entity, bool saveChanges = false)
        {
            var rtn = entities.Add(entity);
            if (saveChanges)
            {
                context.SaveChanges();
            }
            return rtn;
        }

        public virtual void Delete(object id, bool saveChanges = false)
        {
            var item = GetById(id);
            entities.Remove(item);
            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity, bool saveChanges = false)
        {
            entities.Attach(entity);
            entities.Remove(entity);
            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity, bool saveChanges = false)
        {
            var entry = context.Entry(entity);
            entities.Attach(entity);
            entry.State = EntityState.Modified;
            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        public virtual TEntity Update(TEntity entity, object key, bool saveChanges = false)
        {
            if (entity == null)
                return null;
            var exist = entities.Find(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(entity);
                if (saveChanges)
                {
                    context.SaveChanges();
                }
            }
            return exist;
        }

        public virtual void Commit()
        {
            context.SaveChanges();
        }
    }
}
