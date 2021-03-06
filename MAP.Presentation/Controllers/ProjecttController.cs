﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class ProjecttController : Controller
    {
        public string x;
        User u = new User();

        IProjectService MyProjectService;
        IUserService MyUserService;

        public ProjecttController()
        {
            MyProjectService = new ProjectService();
            MyUserService = new UserService();
        }


        // GET: Project
        //affichage
        [Authorize]
        public ActionResult Index(string searchString)
        {
            var Projects = new List<ProjectVM>();

            foreach (Project f in MyProjectService.SearchProjectsByName(searchString))
            {
                Projects.Add(new ProjectVM()
                {
                    ProjectId = f.ProjectId,
                    Title = f.Title,
                    Description = f.Description,
                    Branche = (BrancheVM)f.Branche,
                    ImageUrl = f.ImageUrl,
                    OutDate = f.OutDate,
                    Id = f.Id
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
            return View(Projects);
        }
        [Authorize]
        public ActionResult Indexothers(string searchString)
        {
            var  Projects = new List<ProjectVM>();

            foreach (Project f in MyProjectService.SearchProjectsByName(searchString))
            {
                Projects.Add(new ProjectVM()
                {
                    ProjectId = f.ProjectId,
                    Title = f.Title,
                    Description = f.Description,
                    Branche = (BrancheVM)f.Branche,
                    ImageUrl = f.ImageUrl,
                    OutDate = f.OutDate,
                    Id = f.Id
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
            return View(Projects);
        }
        public ActionResult Details(int id)
        {
            Project p = MyProjectService.GetProjectById(id);
            //}
            ProjectVM VM = new ProjectVM();
            VM.Title = p.Title;
            VM.Description = p.Description;
            VM.Branche = (BrancheVM)p.Branche;
            VM.ImageUrl = p.ImageUrl;
            VM.OutDate = p.OutDate;
            VM.Id = p.Id;
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

            return View(VM);

        }


        // GET: Project/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var MyUsers = MyUserService.GetMany();
            ViewBag.ListUsers = new SelectList(MyUsers, "Id", "FirstName");
            //viewbag :variable pour tronsporter les données du controller lil vue 
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
            return View();
        }

        // POST: Project/Create
        //ajout base
        [HttpPost]
        public ActionResult Create(ProjectVM ProjectVM, HttpPostedFileBase Image)
        {
            Project ProjectsDomain = new Project()
            {

                Description = ProjectVM.Description,
                Branche = (Branche)ProjectVM.Branche,
                ImageUrl = Image.FileName,
                OutDate = ProjectVM.OutDate,
                Title = ProjectVM.Title,
                Id = ProjectVM.Id
            };
            MyProjectService.Add(ProjectsDomain);
            MyProjectService.Commit();
            //ajout de l'image dans un dossier Upload
            var path = Path.Combine(Server.MapPath("~/Content/Upload/"), Image.FileName);
            Image.SaveAs(path);
            if (AccountController.CurrentUserStatus == 2)
            {
                return RedirectToAction("Indexothers");
            }
            else if (AccountController.CurrentUserStatus == 3)
            {
                return RedirectToAction("Indexothers");

            }
            else return RedirectToAction("Index");


            

            
        }
        [HttpGet]


        // GET: Project/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Project p = MyProjectService.GetProjectById(id);
            ProjectVM VM = new ProjectVM();

            //if (p == null)
            // {
            //return HttpNotFound();
            //   return View();
            //  }


            VM.Title = p.Title;
            VM.Description = p.Description;
            VM.Branche = (BrancheVM)p.Branche;
            VM.ImageUrl = p.ImageUrl;
            VM.OutDate = p.OutDate;
            VM.Id = p.Id;

            return View(VM);
        }



        // POST: Project/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectVM VM)
        {


            try
            {
                // TODO: Add update logic here
                Project p1 = MyProjectService.GetProjectById(id);

                p1.Title = VM.Title;
                p1.Description = VM.Description;
                //(BrancheVM)p1.Branche = VM.Branche;
                p1.ImageUrl = VM.ImageUrl;
                p1.OutDate = VM.OutDate;
                p1.Id = VM.Id;
                MyProjectService.Update(p1);
                MyProjectService.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(VM);
            }
        }

        [HttpGet]
        // GET: Project/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Project p = MyProjectService.GetProjectById(id);
            //}
            ProjectVM VM = new ProjectVM();
            VM.ProjectId = p.ProjectId;
            VM.Title = p.Title;
            VM.Description = p.Description;
            VM.Branche = (BrancheVM)p.Branche;
            VM.ImageUrl = p.ImageUrl;
            VM.OutDate = p.OutDate;
            VM.Id = p.Id;

            return View(VM);
        }



        [HttpPost]
        // POST: Project/Delete/5
        public ActionResult Delete(int id, ProjectVM VM)
        {
            try
            {
                // TODO: Add delete logic here
                Project p = MyProjectService.GetProjectById(id);
                MyProjectService.Delete(p);
                MyProjectService.Commit();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void pdf()
        {
            foreach (Project p in MyProjectService.GetProjectById1())
            {
                Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                using (MemoryStream stream = new MemoryStream())
                {
                    pdfDoc.NewPage();

                    Paragraph para = new Paragraph("Le titre de ce projet est:    " + p.Title);
                    Paragraph para1 = new Paragraph("         \n                                                                   ");
                    Paragraph para2 = new Paragraph("La Description est:    " + p.Description);
                    Paragraph para3 = new Paragraph("            \n                                                                 ");
                    Paragraph para4 = new Paragraph("La branche du projet est:   " + (BrancheVM)p.Branche);
                    Paragraph para5 = new Paragraph("                \n                                                            ");
                    Paragraph para6 = new Paragraph("ce projet est mis à notre disposition a la date: " + p.OutDate);


                    pdfDoc.Add(para);
                    pdfDoc.Add(para1);
                    pdfDoc.Add(para2);
                    pdfDoc.Add(para3);
                    pdfDoc.Add(para4);
                    pdfDoc.Add(para5);
                    pdfDoc.Add(para6);
                   



                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                   
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }
}