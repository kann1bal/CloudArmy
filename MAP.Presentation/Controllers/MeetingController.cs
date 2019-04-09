using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MAP.Presentation.Controllers
{


    public class MeetingController : Controller
    {

        IMeetingService MyMeetingService;
        IUserService MyUserService;
        IProjectService MyProjectService;
        

        MeetingService rs = new MeetingService();

        public MeetingController()
        {
            MyMeetingService = new MeetingService();
            MyUserService = new UserService();
            MyProjectService = new ProjectService();
        }

        // GET: Meeting
        public ActionResult Index(string searchString)
        {
            var Meetings = new List<MeetingVM>();

            foreach (Meeting f in MyMeetingService.SearchMeetingsByName(searchString))
            {
                Meetings.Add(new MeetingVM()
                {
                    Id = f.MeetingId,
                    Title = f.Title,
                    Date = f.Date,
                    //ProjectName = f.Project.Name,
                    Details = f.Details,


                });
            }
            return View(Meetings);
            
        }


        // GET: Meeting/Create
        public ActionResult Create()
        {

            
            var MyProjects = MyProjectService.GetMany();


           
            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Title");


            //viewbag :variable pour tronsporter les données du controller lil vue 
            return View();
        }

        // POST: Meeting/Create
        //ajout à la base
        [HttpPost]
        public ActionResult Create(MeetingVM MeetingVM)
        {
            /*string userId = User.Identity.GetUserId();
            var user = (new ApplicationDbContext()).Users.FirstOrDefault(s => s.Id == userId);*/

            Meeting meetingdomain = new Meeting()
            {

                MeetingId = MeetingVM.MeetingId,
                Title = MeetingVM.Title,
                Date = MeetingVM.Date,
                Id = 1,
                ProjectId = MeetingVM.ProjectId,
                Details = MeetingVM.Details,

            };

            MyMeetingService.Add(meetingdomain);
            MyMeetingService.Commit();

            return RedirectToAction("Index");
        }

        // GET: Meeting/Delete/id
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Meeting M = MyMeetingService.GetMeetingById(Id);
            
            MeetingVM MVM = new MeetingVM();
            MVM.MeetingId = M.MeetingId;
            MVM.Title = M.Title;
            MVM.Date = M.Date;
            MVM.ProjectId = M.ProjectId;
            MVM.Id = M.Id;
            MVM.Details = M.Details;


            return View(MVM);
        }

        /// POST: Meeting/Delete/id
        [HttpPost]
        public ActionResult Delete(int Id, MeetingVM VM)
        {
            try
            {
                // TODO: Add delete logic here
                Meeting M = MyMeetingService.GetMeetingById(Id);
                MyMeetingService.Delete(M);
                MyMeetingService.Commit();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        // GET: Meeting/Edit/id
        public ActionResult Edit(int id)
        {
            var MyProjects = MyProjectService.GetMany();



            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Title");

            Meeting M = MyMeetingService.GetMeetingById(id);
            MeetingVM MVM = new MeetingVM();
            MVM.Title = M.Title;
            MVM.Date = M.Date;
            MVM.Details = M.Details;
            MVM.ProjectId = M.ProjectId;
            return View(MVM);
        }

        // POST: Meeting/Edit/id

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MeetingVM MVM)
        {


            try
            {
                // TODO: Add update logic here
                Meeting M = MyMeetingService.GetMeetingById(id);
                M.Title = MVM.Title;
                M.Date = MVM.Date;
                M.Details = MVM.Details;
                MyMeetingService.Update(M);
                MyMeetingService.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(MVM);
            }
        }


    }
}