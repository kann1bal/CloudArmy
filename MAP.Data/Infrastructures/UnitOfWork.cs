using MAP.Data;
using MAP.Data.Infrastructure;

namespace MAPData.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectContext datacontext;
        private IDataBaseFactory dbFactory;
        public UnitOfWork(IDataBaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            datacontext = dbFactory.DataContext;
        }
        public void Commit()
        {
            datacontext.SaveChanges();
        }
        public void Dispose()
        {
            datacontext.Dispose();
        }

        public IRepositoryBase<T> getRepository<T>() where T : class
        {
            return new RepositoryBase<T>(dbFactory);
        }

      
    }
}
