using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MAP.Presentation.Controllers
{
    public class DocumentWebApiController : ApiController
    {
        IDocumentService MyService = null;


        private DocumentService ms = new DocumentService();
        IProjectService MyProjectService;

        List<DocumentVM> Documents = new List<DocumentVM>();
        public DocumentWebApiController()
        {
            MyProjectService = new ProjectService();

            MyService = new DocumentService();
            Index();
            Documents = Index().ToList();
        }


        public List<DocumentVM> Index()
        {
            List<Documentt> mandates = ms.getDocuments();
            List<DocumentVM> mandatesXml = new List<DocumentVM>();
            foreach (Documentt f in mandates)
            {
                mandatesXml.Add(new DocumentVM
                {

                    DocumentId = f.DocumentId,
                    DateDoc = f.DateDoc,
                    Name = f.Name,
                    Size = f.Size,
                    ImageUrl= f.ImageUrl,
                    TypeVm =(TypeVm)f.FileType,
                    ProjectId = f.ProjectId,
                    ProjectNames = MyProjectService.GetById((int)f.ProjectId).Title,
                    Extension = Path.GetExtension(f.ImageUrl)

                });
            }
            return mandatesXml;
        }

        public IEnumerable<DocumentVM> Get()
        {
            return Documents;
        }
        [HttpPost]
        [Route("api/Doc")]
        public Documentt Post(Documentt DOC)
        {


            MyService.Add(DOC);
            MyService.Commit();

            return DOC;
        }
        // GET: api/RecWebApi/5

        public Documentt Get(int id)
        {
            Documentt DOC = MyService.GetById(id);

            return DOC;
        }
        // POST: api/RecWebApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RecWebApi/5
        [HttpPut]
        [Route("api/DocUp")]
        public void Put(int id, Documentt loo)
        {
            Documentt Doc = MyService.GetById(id);


            Doc.DateDoc = loo.DateDoc;
            Doc.Name = loo.Name;
            Doc.Size = loo.Size;
            Doc.ImageUrl = loo.ImageUrl;

        }

       
        // DELETE: api/RecWebApi/5
        public IHttpActionResult Delete(int id)

        {
            Documentt comp = MyService.GetById(id);

            MyService.Delete(comp);
            MyService.Commit();

            return Ok(comp);


        }
    }
}
