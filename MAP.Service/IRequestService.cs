using MAP.Domain.Entities;
using MAP.Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
    public interface IRequestService:IService<Request>
    {
        IEnumerable<Request> SearchRequestsByName(string searchString);
        IEnumerable<Request> SearchRequestsBySubject(string searchStringS);
        Request GetRequestById(int Id);

        void Edit(Request R);

        //  void Detach(Request R);
        int GetRequestCount(int indice,Priority priority);
        int GetStatistique(Kind kind, Status status);

    }
}
