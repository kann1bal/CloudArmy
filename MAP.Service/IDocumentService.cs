using MAP.Domain.Entities;
using MAP.Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
  public  interface IDocumentService: IService<Documentt>
    {
        IEnumerable<Documentt> SearchDocumentByName(string searchString);
    }
}
