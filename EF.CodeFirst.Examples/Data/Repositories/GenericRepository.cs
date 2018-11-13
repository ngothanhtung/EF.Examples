using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace EF.CodeFirst.Examples.Data.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal OnlineShopDb context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            this.context = new OnlineShopDb();
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {           
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.dbSet.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (entityToDelete == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.dbSet.Remove(entityToDelete);

                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                if (entityToUpdate == null)
                {
                    throw new ArgumentNullException("entity");
                }

                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
            {
                foreach (var error in validationErrors.ValidationErrors)
                {
                    msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}" + Environment.NewLine;
                }
            }
            return msg;
        }
    }
}