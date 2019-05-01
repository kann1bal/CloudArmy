using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MAP.Data.Infrastructure
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ProjectContext dataContext;
        private  IDbSet<T> dbset;
        private IDataBaseFactory datadbaseFactory;
        public RepositoryBase(IDataBaseFactory factory)
        {
            datadbaseFactory = factory;
            dataContext = factory.DataContext;
            dbset = dataContext.Set<T>();
        }

        protected ProjectContext DataContext {
            get { return dataContext = datadbaseFactory.DataContext; }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> condition)
        {
            IEnumerable<T> objects = dbset.Where(condition).AsEnumerable();

            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual T Get(Expression<Func<T, bool>> condition)
        {
            return dbset.Where(condition).FirstOrDefault<T>();
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy=null)
        {
            IQueryable<T> query = dbset;

            if (condition != null)
                query = query.Where(condition);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return query;
        }
         
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }


    }
}
