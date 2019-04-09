using MAP.Data;
using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class TeamLeaderRequestController : Controller
    {

        ProjectContext context = new ProjectContext();
        IRequestService MyRequestService;
        IProjectService MyProjectService;
        IUserService MyDedicatedService;

        RequestService rs = new RequestService();
        public TeamLeaderRequestController()
        {
            MyRequestService = new RequestService();
            MyProjectService = new ProjectService();
            MyDedicatedService = new UserService();
        }
      
        

        public ActionResult Table(string searchString)
        {
            var Requests = new List<RequestVM>();
            foreach (Request f in MyRequestService.SearchRequestsByName(searchString))

            {
                Requests.Add(new RequestVM()
                {
                    RequestId = f.RequestId,


                    Name = f.Name,

                    Kind = (KindVM)f.Kind,

                    Status = (StatusVM)f.Status,

                    Priority = (PriorityVM)f.Priority,

                    Category = (CategoryVM)f.Category,

                    UpdateDate = f.UpdateDate,

                    Subject = f.Subject,

                    Id = f.Id,

                    ProjectId = f.ProjectId,

                    UserCreate = User.Identity.Name,





                    UpdatedBy = f.UpdatedBy,
                });
            }

            return View(Requests);
        }


        // GET: TeamLeader/Details/55
        public ActionResult Details(int id)
        {
           Request R = MyRequestService.GetRequestById(id);

            RequestVM RVM = new RequestVM();

            RVM.RequestId = R.RequestId;
            RVM.Name = R.Name;
            RVM.Kind = (KindVM)R.Kind;
            RVM.Priority = (PriorityVM)R.Priority;
            RVM.Category = (CategoryVM)R.Category;
            RVM.UpdateDate = R.UpdateDate;
            RVM.Subject = R.Subject;
            RVM.Id = R.Id;
            RVM.ProjectId = R.ProjectId;
            RVM.UserCreate = User.Identity.Name;
            RVM.UpdatedBy = R.UpdatedBy;



            return View(RVM);

        }

        // GET: TeamLeader/Create
        public ActionResult Create()
        {


            var MyProjectCs = MyProjectService.GetMany();
            var MyDedicatedTo = MyDedicatedService.GetMany();

            ViewBag.ListProjectCs = new SelectList(MyProjectCs, "ProjectId", "Title");
            ViewBag.ListDedicated = new SelectList(MyDedicatedTo, "Id", "UserName");

            //viewbag :variable pour tronsporter les données du controller lil vue 
            return View();
        }
        // POST: TeamLeader/Create
        [HttpPost]
        public ActionResult Create(RequestVM RequestVM)
        {
            Request requestdomain = new Request()

            {

                Name = RequestVM.Name,
                Kind = (Kind)RequestVM.Kind,
                //Status = (Status)RequestVM.Status,
                Priority = (Priority)RequestVM.Priority,
                Category = (Category)RequestVM.Category,
                UpdateDate = RequestVM.UpdateDate,
                Subject = RequestVM.Subject,
                Id = RequestVM.Id,
                UserCreate = User.Identity.Name,
                ProjectId = RequestVM.ProjectId,
                UpdatedBy = User.Identity.Name,



            };

            MyRequestService.Add(requestdomain);
            MyRequestService.Commit();

            return RedirectToAction("Table");
        }

        // GET: TeamLeader/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Request R = MyRequestService.GetRequestById(id);
            RequestVM RVM = new RequestVM();

            //if (p == null)
            // {
            //return HttpNotFound();
            //   return View();
            //  }


            // RVM.Name = R.Name;
            // RVM.Kind = (KindVM)R.Kind;
            // RVM.Priority = (PriorityVM)R.Priority;  
             RVM.Status = (StatusVM)R.Status;
            // RVM.Category = (CategoryVM)R.Category;
            //    RVM.UpdateDate = R.UpdateDate;
            //  RVM.Subject = R.Subject;
            // RVM.Author = R.Author;
            // RVM.ProjectCId = R.ProjectCId;
            // RVM.UserIId = R.UserIId;




            return View(RVM);
        }

        
       // TeamLeader/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id, Request RVM)
        {
            try
            {
                // TODO: Add update logic here
                Request R = MyRequestService.GetRequestById(id);


                //  R.Name = RVM.Name;
                //  R.Kind = RVM.Kind;
                // R.Priority = RVM.Priority;
                //  R.Category = RVM.Category;
                R.Status = RVM.Status;
                //R.UpdatedBy = User.Identity.Name;
                //  R.UpdateDate = RVM.UpdateDate;
                //  R.Subject = RVM.Subject;
                // R.Author = RVM.Author;

                MyRequestService.Update(R);
                MyRequestService.Commit();
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(R.UserCreate));  // replace with valid value 
                message.From = new MailAddress(User.Identity.Name);  // replace with valid value
                message.Subject = "Request Answer ! ";
                message.Body = string.Format(body, "Request services", User.Identity.Name, "Dear Applicant your request has been " + R.Status);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "bejaouiikramm@gmail.com",  // replace with valid value
                        Password = "Peaceikram123."  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);


                }

                return RedirectToAction("Table");
            }
            catch (Exception)
            {
                return View(RVM);
            }
        }
        // GET: TeamLeader/Delete/5
        public ActionResult Delete(int Id)
        {
            Request R = MyRequestService.GetRequestById(Id);
            RequestVM RVM = new RequestVM();
           // RVM.RequestId = R.RequestId;
            RVM.Name = R.Name;
            RVM.Kind = (KindVM)R.Kind;
            RVM.Priority = (PriorityVM)R.Priority;
            RVM.Category = (CategoryVM)R.Category;
            RVM.UpdateDate = R.UpdateDate;
            RVM.Subject = R.Subject;
            RVM.Id = R.Id;

            RVM.ProjectId = R.ProjectId;


            return View(RVM);
        }


        // POST: TeamLeader/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, RequestVM VM)
        {
            try
            {
                // TODO: Add delete logic here
                Request p = MyRequestService.GetRequestById(Id);
                MyRequestService.Delete(p);
                MyRequestService.Commit();

                return RedirectToAction("Table");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Chart()
        {
            var Requests = new List<RequestVM>();


            return View(Requests);
        }


        public ActionResult GetData()


        {
             int urgent = MyRequestService.GetRequestCount(0, Priority.Urgent);
             int high = MyRequestService.GetRequestCount(1, Priority.High);
             int normal = MyRequestService.GetRequestCount(2, Priority.Normal);
             int low = MyRequestService.GetRequestCount(3, Priority.Low);

            /*
            int urgent = context.Requests.Where(x => x.Priority == Priority.Urgent).Count();
            int high = context.Requests.Where(x => x.Priority == Priority.High).Count();
            int normal = context.Requests.Where(x => x.Priority == Priority.Normal).Count();
            int low = context.Requests.Where(x => x.Priority == Priority.Low).Count();
             */
            PriorityR obj = new PriorityR();

            obj.Urgent = urgent;
            obj.High = high;
            obj.Normal = normal;
            obj.Low = low;

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public class PriorityR
        {

            public int Urgent { get; set; }
            public int High { get; set; }
            public int Normal { get; set; }
            public int Low { get; set; }

        }

        public ActionResult GetNumber()
        {

           
            int data = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 0 AND Status = 0").Count();
            int data1 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 0 AND Status = 1").Count();
            int data2 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 0 AND Status = 2").Count();
            int data0 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 1 AND Status = 0").Count();
            int data11 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 1 AND Status = 1").Count();
            int data22 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 1 AND Status = 2").Count();
            int data00 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 2 AND Status = 0").Count();
            int data111 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 2 AND Status = 1").Count();
            int data222 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 2 AND Status = 2").Count();
            int data3 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 3 AND Status = 0").Count();
            int data4 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 3 AND Status = 1").Count();
            int data5 = context.Requests.SqlQuery("SELECT * FROM Requests where Kind = 3 AND Status = 2").Count();


            /*
           int data = MyRequestService.GetStatistique(Kind.defect, Status.NotTreatedYet);
            int data1 = MyRequestService.GetStatistique(Kind.defect, Status.open);
            int data2 = MyRequestService.GetStatistique(Kind.defect, Status.closed);
            int data0 = MyRequestService.GetStatistique(Kind.feature, Status.NotTreatedYet);
            int data11 = MyRequestService.GetStatistique(Kind.feature, Status.open);
            int data22 = MyRequestService.GetStatistique(Kind.feature, Status.closed);
            int data00 = MyRequestService.GetStatistique(Kind.patch, Status.NotTreatedYet);
            int data111 = MyRequestService.GetStatistique(Kind.patch, Status.open);
            int data222 = MyRequestService.GetStatistique(Kind.patch, Status.closed);
            int data3 = MyRequestService.GetStatistique(Kind.claim, Status.NotTreatedYet);
            int data4 = MyRequestService.GetStatistique(Kind.claim, Status.open);
            int data5 = MyRequestService.GetStatistique(Kind.claim, Status.closed); 
            */

            int totaldef = data + data1 + data2;
            int totalfea = data0 + data11 + data22;
            int totalpatch = data00 + data111 + data222;
            int totalclaim = data3 + data4 + data5;

            countNumber obj = new countNumber();


            obj.data = data;
            obj.data1 = data1;
            obj.data2 = data2;
            obj.data0 = data0;
            obj.data11 = data11;
            obj.data22 = data22;
            obj.data00 = data00;
            obj.data111 = data111;
            obj.data222 = data222;
            obj.data3 = data3;
            obj.data4 = data4;
            obj.data5 = data5;
            obj.totalclaim = totalclaim;
            obj.totaldef = totaldef;
            obj.totalfea = totalfea;
            obj.totalpatch = totalpatch;
            return Json(obj, JsonRequestBehavior.AllowGet);

        }



        public class countNumber
        {
            public int data { get; set; }
            public int data1 { get; set; }
            public int data2 { get; set; }
            public int data0 { get; set; }
            public int data11 { get; set; }
            public int data22 { get; set; }
            public int data00 { get; set; }
            public int data111 { get; set; }
            public int data222 { get; set; }
            public int data3 { get; set; }
            public int data4 { get; set; }
            public int data5 { get; set; }
            public int totalclaim { get; set; }
            public int totaldef { get; set; }
            public int totalfea { get; set; }
            public int totalpatch { get; set; }
        }


    }
}
