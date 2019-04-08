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
   public class DocumentService:Service<Documentt>,IDocumentService
    {


        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public DocumentService() : base(utk)
        {


        }

       
        public IEnumerable<Documentt> SearchDocumentByName(string searchString)
        {
            IEnumerable<Documentt> DocumentDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                DocumentDomain = GetMany(x => x.ProjectId.ToString().Contains(searchString));
            }


            return DocumentDomain;
        }
        public List<Documentt> getTasksbyIdProject(int ProjectId)
        {
            List<Documentt> ListDocuments = new List<Documentt>();
            ListDocuments = GetMany(b => b.ProjectId==ProjectId).ToList();
            return ListDocuments;
        }


    }
}
