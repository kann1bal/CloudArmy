using MAP.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service.Pattern
{
    public class Service<T> : IService<T> where T : class
    {
        IUnitOfWork utk;
        public Service(IUnitOfWork utk)
        {
            this.utk = utk;
        }
        public void Add(T entity)
        {
            utk.getRepository<T>().Add(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            utk.getRepository<T>().Delete(where);
        }

        public void Delete(T entity)
        {
            utk.getRepository<T>().Delete(entity);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return utk.getRepository<T>().Get(where);
        }

        public T GetById(string id)
        {
            return utk.getRepository<T>().GetById(id);
        }

        public T GetById(int id)
        {
            return utk.getRepository<T>().GetById(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> orderBy = null)
        {
            return utk.getRepository<T>().GetMany(where, orderBy);
        }

        public void Update(T entity)
        {
            utk.getRepository<T>().Update(entity);
        }
        public void Commit()
        {
            try { utk.Commit(); }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()

        {
            utk.Dispose();
        }
    }
}
