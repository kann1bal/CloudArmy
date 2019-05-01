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
        public string x;
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
        [Authorize]
        public ActionResult Index(string searchString)
        {
            var Meetings = new List<MeetingVM>();

            foreach (Meeting f in MyMeetingService.SearchMeetingsByName(searchString))
            {
                Meetings.Add(new MeetingVM()
                {
                    MeetingId =f.MeetingId,
                    Title = f.Title,
                    Date = f.Date,
                    ProjectName = MyProjectService.GetById((int)f.ProjectId).Title,



                    Details = f.Details,


                });
            }
            

            return View(Meetings);
            
        }
        [Authorize]
        public ActionResult IndexOthers(string searchString)
        {
            var Meetings = new List<MeetingVM>();

            foreach (Meeting f in MyMeetingService.SearchMeetingsByName(searchString))
            {
                Meetings.Add(new MeetingVM()
                {
                    MeetingId = f.MeetingId,
                    Title = f.Title,
                    Date = f.Date,

                    ProjectName = MyMeetingService.GetById((int)f.ProjectId).Title,
                    Details = f.Details,


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
            return View(Meetings);

        }


        // GET: Meeting/Create
        [Authorize]
        public ActionResult Create()
        {

            
            var MyProjects = MyProjectService.GetMany();


           
            ViewBag.ListProjects = new SelectList(MyProjects, "ProjectId", "Title");


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
                Id = AccountController.CurrentUserId,
                ProjectId = MeetingVM.ProjectId,
                Details = MeetingVM.Details,

            };

            MyMeetingService.Add(meetingdomain);
            MyMeetingService.Commit();

            return RedirectToAction("Index");
        }

        // GET: Meeting/Delete/id
        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult IndexCalendar()
        {
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
        [Authorize]
        public ActionResult Statistic()
        {

            return View();
        }
        public ActionResult GetNumber()
        {

            int tocome = MyMeetingService.GetNbMeetingAfter(DateTime.Now);
            int done = MyMeetingService.GetNbMeetingBefore(DateTime.Now);
            int today = MyMeetingService.GetNbMeeting(DateTime.Now);


            int total = today + tocome + done;


            countNumber obj = new countNumber();


            obj.today = today;
            obj.tocome = tocome;
            obj.done = done;
            obj.total = total;

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public class countNumber
        {
            public int today { get; set; }
            public int tocome { get; set; }
            public int done { get; set; }
            public int total { get; set; }

        }

        public JsonResult GetMeetings()
        {
            {
                var data = GetUsersHugeData();
                return Json(data, JsonRequestBehavior.AllowGet);
                //   return new JsonResult { Data = List, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public class CustomEvent
        {
            public int id { get; set; }
            public String title { get; set; }
            public String Detail { get; set; }
            public String start { get; set; }

        }

        private List<CustomEvent> GetUsersHugeData()
        {
            var list = new List<CustomEvent>();

            foreach (Meeting m in MyMeetingService.GetMany())
            {
                CustomEvent e = new CustomEvent();
                e.id = m.MeetingId;
                e.title = m.Title;
                e.Detail = m.Details;
                e.start = m.Date.ToString("yyyy-MM-dd");
                list.Add(e);
            }


            return list;
        }


    }
}