using MAP.Data.Infrastructure;
using MAP.Domain.Entities;
using MAP.Service.Pattern;
using MAPData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
    public class RequestService:Service<Request>, IRequestService
    {
        static IDataBaseFactory Factory = new DataBaseFactory(); //usine de fabrication du contexte
        static IUnitOfWork utk = new UnitOfWork(Factory); //communication avec la base
        public RequestService():base(utk)
        {

        }

        public IEnumerable<Request> SearchRequestsByName(string searchString)
        {
            IEnumerable<Request> RequestsDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                RequestsDomain = GetMany(x => x.Name.Contains(searchString));
            }
            return RequestsDomain;

        }

        public Request GetRequestById(int Id)
        {
            return utk.getRepository<Request>().GetById(Id);
        }
        public void Edit(Request R)
        {
            utk.getRepository<Request>().Update(R);
            utk.Commit();
        }

        public IEnumerable<Request> SearchRequestsBySubject(string searchStringS)
        {
            IEnumerable<Request> RequestsDomain = GetMany();
            if (!String.IsNullOrEmpty(searchStringS))
            {
                RequestsDomain = GetMany(x => x.Name.Contains(searchStringS));
            }
            return RequestsDomain;

        }

        public int GetRequestCount (int indice,Priority priority)
        {
            if (indice == 0)
                return utk.getRepository<Request>().GetMany().Where(x => x.Priority == priority).Count();

            if (indice == 1)
                return utk.getRepository<Request>().GetMany().Where(x => x.Priority == priority).Count();

            if (indice == 2)
                return utk.getRepository<Request>().GetMany().Where(x => x.Priority == priority).Count();

            if (indice == 3)
                return utk.getRepository<Request>().GetMany().Where(x => x.Priority == priority).Count();
            return 0;
        }


        public int GetStatistique(Kind kind,Status status)
        {
            return utk.getRepository<Request>().GetMany().Where(a => a.Kind==kind).Where(b => b.Status == status).OrderBy(c=>c.Kind).Count();
            
        }





    }
}
