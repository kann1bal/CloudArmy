﻿using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Rotativa.MVC;
using MAP.Presentation.Controllers;


namespace MAP.Presentation.Controllers
{
    public class DocumentController : Controller
    {
       static  int Id4Dowload;
        public string x;
        public enum Choix { Name, Project };


        IDocumentService MyDocService;
        IProjectService MyProjectService;
        IProjectService MyeditProjectService;


        public DocumentController()
        {
            MyDocService = new DocumentService();

            MyProjectService = new ProjectService();
            MyeditProjectService= new ProjectService();
        }
        // GET: Documentt
        [Authorize]
        public ActionResult Index(string searchString)
        {

            var documents = new List<DocumentVM>();


            foreach (Documentt f in MyDocService.SearchDocumentByName(searchString))
            {

                documents.Add(new DocumentVM()
                {
                    DocumentId = f.DocumentId,
                    DateDoc = f.DateDoc,
                    Name = f.Name,
                    Size = f.Size,
                    TypeVm = (TypeVm)f.FileType,
                    ImageUrl = f.ImageUrl,
                    ProjectId = f.ProjectId,
                    ProjectNames = MyProjectService.GetById((int)f.ProjectId).Title,
                    Extension = Path.GetExtension(f.ImageUrl)
            });

                if (AccountController.CurrentUserStatus == 2)
                {
                    x = "Manager";
                }
                else if (AccountController.CurrentUserStatus == 3)
                {
                    x = "Team Leader";
                }
                else
                { x = "Membre"; }
                ViewBag.CurrentUserStatus = x;
            }


           
            return View(documents);
        }
        public ActionResult DocumentByProject(int ProjectId)
        {
            ViewBag.id = ProjectId;

            var List = MyDocService.GetDocumentbyIdProject(ProjectId);
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;
            return View(List);
        }

        [Authorize]
        public ActionResult IndexOthers(string searchString)
        {

            var documents = new List<DocumentVM>();


            foreach (Documentt f in MyDocService.SearchDocumentByName(searchString))
            {

                documents.Add(new DocumentVM()
                {
                    DocumentId = f.DocumentId,
                    DateDoc = f.DateDoc,
                    Name = f.Name,
                    Size = f.Size,
                    TypeVm = (TypeVm)f.FileType,
                    ImageUrl = f.ImageUrl,
                    ProjectId = f.ProjectId,
                    ProjectNames = MyProjectService.GetById((int)f.ProjectId).Title,
                    Extension = Path.GetExtension(f.ImageUrl)
                });

                
            }
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;
            return View(documents);
        }

            /// index vue partielle 
            public ActionResult Indexpartielle(string searchString)
        {

            var documents = new List<DocumentVM>();


            foreach (Documentt f in MyDocService.SearchDocumentByName(searchString))
            {

                documents.Add(new DocumentVM()
                {
                    DocumentId = f.DocumentId,
                    DateDoc = f.DateDoc,
                    Name = f.Name,
                    Size = f.Size,
                    TypeVm = (TypeVm)f.FileType,
                    ImageUrl = f.ImageUrl,
                    ProjectId = f.ProjectId,
                    ProjectNames = MyProjectService.GetById((int)f.ProjectId).Title,
                    Extension = Path.GetExtension(f.ImageUrl)
                });



            }

            return View(documents);
        }
        [Authorize]
        // GET: Documentt/Details/5
        public ActionResult Details(int id)
        {
            var p = MyDocService.GetById(id);
            DocumentVM Docvm = new DocumentVM();


            Docvm.DateDoc = p.DateDoc;
            Docvm.Name = p.Name;
            Docvm.ImageUrl = p.ImageUrl;
            Docvm.Size = p.Size;
            Docvm.TypeVm = (TypeVm)p.FileType;
            //Docvm.P = MyProjectService.GetById(p.ProjectId).Name;
            Docvm.ProjectId = p.ProjectId;
            Docvm.ProjectNames = MyProjectService.GetById((int)p.ProjectId).Title;
            Docvm.Extension = Path.GetExtension(p.ImageUrl);
            Id4Dowload = p.DocumentId;
            System.Diagnostics.Debug.WriteLine("////** this is me  " + Id4Dowload);




            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;

            return View(Docvm);

        }


        // GET: Documentt/Create
        [Authorize]
        public ActionResult Create()
        {

            var MyProjects = MyProjectService.GetMany();

            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Title");
            //viewbag :variable pour tronsporter les données du controller lil vue 
            return View();
        }

        // POST: Documentt/Create
        [HttpPost]
        public ActionResult Create(DocumentVM DocVM, HttpPostedFileBase Image)
        {

            Documentt t1 = new Documentt();

            t1.Name = DocVM.Name + "  " + DateTime.Now.ToString();
            t1.DateDoc = DateTime.Now;
            t1.Size = DocVM.Size;
            t1.FileType = (FileType)DocVM.TypeVm;
            t1.ImageUrl = Image.FileName;
            t1.ProjectId = DocVM.ProjectId;
            t1.FileType = (FileType)DocVM.TypeVm;

            MyDocService.Add(t1);
            MyDocService.Commit();
            var path = Path.Combine(Server.MapPath("~/Content/Upload/"), Image.FileName);
            //ajout de l'image dans un dossier Upload
            Image.SaveAs(path);
            MailMessage mail = new MailMessage("zied.ue@gmail.com", "zied.ue@gmail.com");
            mail.Subject = "documet cree";
            mail.Body = "test document envoiyer";

            mail.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("Smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new System.Net.NetworkCredential("zied.ue@gmail.com", "123456aze");
            smtpClient.Send(mail);
            /// https://www.google.com/settings/security/lesssecureapps go to link and alllow 
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;
            return RedirectToAction("Index");
        }

        // GET: Documentt/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            System.Diagnostics.Debug.WriteLine("********* this is mee ");
            
            //viewbag :variable pour tronsporter les données du controller lil vue 

            var p = MyDocService.GetById(id);

            DocumentVM DocVm = new DocumentVM();
            string a = p.Name.Substring(0, p.Name.IndexOf(' '));
            System.Diagnostics.Debug.WriteLine("*******" + a);
            DocVm.Name = a;
            DocVm.Size = p.Size;
            DocVm.TypeVm = (TypeVm)p.FileType;
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;
            return View(DocVm);
        }
        

        // POST: Documentt/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DocumentVM DocVm, HttpPostedFileBase Image)
        {
            var p = MyDocService.GetById(id);



            p.Name = DocVm.Name + " " + DateTime.Now.ToString() + "(last Edit)";
            p.Size = DocVm.Size;
            p.FileType = (FileType)DocVm.TypeVm;
            MyDocService.Update(p);
            MyDocService.Commit();
          
            return View();

        }


        // GET: Documentt/Delete/5
        public ActionResult Delete(int id)
        {
            var p = MyDocService.GetById(id);
            DocumentVM Docvm = new DocumentVM();

            Docvm.DateDoc = p.DateDoc;
            Docvm.Name = p.Name;
            Docvm.ImageUrl = p.ImageUrl;
            Docvm.Size = p.Size;
            Docvm.TypeVm = (TypeVm)p.FileType;
            //Docvm.P = MyProjectService.GetById(p.ProjectId).Name;
            Docvm.ProjectId = p.ProjectId;
            Docvm.Extension = Path.GetExtension(p.ImageUrl);
            if (AccountController.CurrentUserStatus == 2)
            {
                x = "Manager";
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                x = "Team Leader";
            }
            else
            { x = "Membre"; }
            ViewBag.CurrentUserStatus = x;


            return View(Docvm);
        }

        // POST: Documentt/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DocumentVM DocVm)
        {

            var p = MyDocService.GetById(id);
            p.DateDoc = DocVm.DateDoc;
            
            p.Name = DocVm.Name;
            p.ImageUrl = DocVm.ImageUrl;
            p.Size = DocVm.Size;
            p.FileType = (FileType)DocVm.TypeVm;
            MyDocService.Delete(p);
            MyDocService.Commit();
            
            return RedirectToAction("index");

        }
        public ActionResult ExportePdf ()
        {
            ActionAsPdf result = new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/Upload/invoice.pdf")

            };
            return result;
        }
        public FileResult Download()
        {
            ///stattic 7ata inchouf ac niya a7kaytou ach bih mouch 9a3ed yethhazzz mil view detail 
            var p = MyDocService.GetById(Id4Dowload);
            System.Diagnostics.Debug.WriteLine("////** this is me again  " + Id4Dowload);

            string path = Server.MapPath("~/Content/Upload/");
             string FileName = Path.GetFileName(p.ImageUrl);
            string extension = Path.GetExtension(p.ImageUrl);
            System.Diagnostics.Debug.WriteLine("////** this is me  "+extension);

            string fullpath = Path.Combine(path,FileName);
            if (extension == ".jpg")
            {
                return File(fullpath, "image/jpg", p.ImageUrl);
            }
            else if (extension == ".pdf")
            {
                return File(fullpath, "file/pdf", p.ImageUrl);
            }else
                return File(fullpath, "file/Docx", p.ImageUrl);

        }
        public ActionResult DetailsAjax(int id)
        {
            List<DocumentVM> lists = new List<DocumentVM>();
            var p = MyDocService.GetById(id);
            DocumentVM Docvm = new DocumentVM();

                
                Docvm.Name = p.Name;
                Docvm.Size = p.Size;
                Docvm.TypeVm =(TypeVm) p.FileType;
                Docvm.ImageUrl = p.ImageUrl;
                Docvm.Extension = Path.GetExtension(p.ImageUrl);
            lists.Add(Docvm);
                
            
            return View(lists);
        }



    }
}
    