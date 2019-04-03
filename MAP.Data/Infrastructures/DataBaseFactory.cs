using MAP.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Infrastructure
{
    public class DataBaseFactory :Disposable, IDataBaseFactory
    {
        private ProjectContext dataContext;
        public DataBaseFactory()
        {
            dataContext=new ProjectContext();
        }
        public ProjectContext DataContext
        {
            get
            {
                return dataContext;
            }
        }

        protected override void DisposeCore()
        {
            if(DataContext!=null)
            DataContext.Dispose();
        }
    }
}
