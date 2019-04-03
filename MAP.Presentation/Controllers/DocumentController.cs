using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class DocumentController : Controller
    {
        IDocumentService MyDocService;
        IProjectService MyProjectService;

        public DocumentController()
        {
            MyDocService = new DocumentService();

            MyProjectService = new ProjectService();
        }
        // GET: Document
        public ActionResult Index(string searchString)
        {
            var documents = new List<DocumentVM>();
            foreach (Document f in MyDocService.SearchDocumentByName(searchString))
            {
                documents.Add(new DocumentVM()
                {
                    DocumentId =f.DocumentId,
                    DateDoc = f.DateDoc,
                    Name = f.Name,
                    Size = f.Size,
                    TypeVm = (TypeVm)f.Type,
                    ImageUrl = f.ImageUrl,
                    ProjectId = f.ProjectId

                });

            }
            return View(documents);
        }

        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            var p = MyDocService.GetById(id);
            DocumentVM Docvm = new DocumentVM();

            
            Docvm.DateDoc = p.DateDoc;
            Docvm.Name = p.Name;
            Docvm.ImageUrl = p.ImageUrl;
            Docvm.Size = p.Size;
            Docvm.TypeVm = (TypeVm)p.Type;
            //Docvm.P = MyProjectService.GetById(p.ProjectId).Name;
            Docvm.ProjectId = p.ProjectId;           

           


            return View(Docvm);

        }
    

        // GET: Document/Create
        public ActionResult Create()
        {
            var MyProjects = MyProjectService.GetMany();
            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Name");
            //viewbag :variable pour tronsporter les données du controller lil vue 
            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(DocumentVM DocVM, HttpPostedFileBase Image)
        {
            Document t1 = new Document()
            {
                Name = DocVM.Name,
                DateDoc = DocVM.DateDoc,
                Size = DocVM.Size,
                Type = (Domain.Entities.Type)DocVM.TypeVm,
                ImageUrl = Image.FileName,
                ProjectId = DocVM.ProjectId
            };
            MyDocService.Add(t1);
            MyDocService.Commit();
            var path = Path.Combine(Server.MapPath("~/Content/Upload/"), Image.FileName);
            //ajout de l'image dans un dossier Upload
            Image.SaveAs(path);
            return RedirectToAction("Index");
        }

        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {

            var MyProjects = MyProjectService.GetMany();
            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Name");
            //viewbag :variable pour tronsporter les données du controller lil vue 
            
            var p = MyDocService.GetById(id);
            DocumentVM DocVm = new DocumentVM();
            p.DateDoc = DocVm.DateDoc;
            p.Name = DocVm.Name;
            p.ImageUrl = DocVm.ImageUrl;
            p.Size = DocVm.Size;
            p.Type = (Domain.Entities.Type)DocVm.TypeVm;

          
            return View(DocVm);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DocumentVM DocVm, HttpPostedFileBase Image)
        {
            var p = MyDocService.GetById(id);
            p.DateDoc = DocVm.DateDoc;
            p.Name = DocVm.Name;
            p.ImageUrl = Image.FileName;
            p.Size = DocVm.Size;
            p.Type = (Domain.Entities.Type)DocVm.TypeVm;
            MyDocService.Update(p);
            MyDocService.Commit();
            var path = Path.Combine(Server.MapPath("~/Content/Upload/"), Image.FileName);
            //ajout de l'image dans un dossier Upload
            Image.SaveAs(path);

            return View();
            
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            var p = MyDocService.GetById(id);
            DocumentVM Docvm = new DocumentVM();

            Docvm.DateDoc = p.DateDoc;
            Docvm.Name = p.Name;
            Docvm.ImageUrl = p.ImageUrl;
            Docvm.Size = p.Size;
            Docvm.TypeVm = (TypeVm)p.Type;
            //Docvm.P = MyProjectService.GetById(p.ProjectId).Name;
            Docvm.ProjectId = p.ProjectId;
            return View(Docvm);
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DocumentVM DocVm)
        {
            var p = MyDocService.GetById(id);
            p.DateDoc = DocVm.DateDoc;
            p.Name = DocVm.Name;
            p.ImageUrl = DocVm.ImageUrl;
            p.Size = DocVm.Size;
            p.Type = (Domain.Entities.Type)DocVm.TypeVm;
            MyDocService.Delete(p);
            MyDocService.Commit();

            return RedirectToAction("index");
            
        }
    }
}
