using iTextSharp.text.pdf.qrcode;
using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace MapWeb.Controllers
{
    public class APIController : ApiController
    {
        ProjectService SC = new ProjectService();
        // ProjectService PS = new ProjectService();





        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("API/Affichage")]
        public IHttpActionResult Affichage()
        {
            List<Project> a = new List<Project>();
            foreach (var item in SC.GetMany(null, null))
            {
                Project p = new Project();
                p.Title = item.Title;
                p.Description = item.Description;
                p.OutDate = item.OutDate;
                p.ImageUrl = item.ImageUrl;
                p.Branche = item.Branche;
                p.Id = item.Id;
                a.Add(p);

            }
            return Ok(
                a
                );


        }
      

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("API/Projet")]
        public IHttpActionResult Projet()
        {
            List<Project> pr = new List<Project>();

            foreach (var item in SC.GetMany(null, null))
            {
                Project p = new Project();

                p.Title = item.Title;
                p.Description = item.Description;
                p.OutDate = item.OutDate;
                p.ImageUrl = item.ImageUrl;
                p.Branche = item.Branche;
                p.Id = item.Id;


                Console.WriteLine("objet" + p);
                pr.Add(p);
                Console.WriteLine("listttttttttttttt" + pr);
            }
            Console.WriteLine("listttttttttttttt" + pr);
            return Ok(
                pr
                );
        }




        [Route("api/CreateProject")]
        [HttpPost]
        public void Create(MAP.Presentation.Models.ProjectVM pro)
        {
            MAP.Domain.Entities.Project Project = new MAP.Domain.Entities.Project
            {
                Title = pro.Title,
                Description = pro.Description,
                OutDate = pro.OutDate,
                ImageUrl = pro.ImageUrl,
                Branche = Branche.Web,
                Id = pro.Id,


            };

            SC.Add(Project);
            SC.Commit();


        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/delete")]
        public IHttpActionResult Projetdelete(int id, ProjectVM VM)
        {

            Project p = SC.GetProjectById(id);
            SC.Delete(p);
            SC.Commit();



            return Ok();


        }
        
                [System.Web.Http.HttpPut]
                [System.Web.Http.Route("api/update")]
                public IHttpActionResult ProjetEdit(int id, MAP.Presentation.Models.ProjectVM VM)
                {

                        Project p1 = SC.GetProjectById(id);

                        p1.Title = VM.Title;
                        p1.Description = VM.Description;
                     //(Branche)p1.Branche = VM.Branche;
                        p1.ImageUrl = VM.ImageUrl;
                        p1.OutDate = VM.OutDate;
                        p1.Id = VM.Id;
                        SC.Update(p1);
                        SC.Commit();

                         return Ok();
                  }


/*
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/update")]
        public IHttpActionResult ProjetEdit(int id)
        {
            
            Project p1 = SC.GetProjectById(id);
         
            ProjectVM VM = new ProjectVM();

                p1.Title = VM.Title;
                p1.Description = VM.Description;
                //(Branche)p1.Branche = VM.Branche;
                p1.ImageUrl = VM.ImageUrl;
                p1.OutDate = VM.OutDate;
                p1.Id = VM.Id;
            SC.Update(p1);
            SC.Commit();
            return Ok();

        }

    */

    }
}


    