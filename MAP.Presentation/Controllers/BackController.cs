using MailKit.Net.Smtp;
using MAP.Domain.Entities;
using MAP.Presentation.Models;
using MAP.Service;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class BackController : Controller
    {
        IUserService MyUserService;

        UserService us = new UserService();

        public BackController()
        {
            MyUserService = new UserService();
        }
        // GET: Back
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Details(int Id)
        {
            return View(us.GetById(Id));

        }


        public ActionResult Edit(int Id)
        {

            return View(us.GetById(Id));

        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formvalues)
        {
            User U = null;
            U = us.GetById(id);
            U.Email = formvalues["Email"];
            U.UserName = formvalues["UserName"];
            U.FirstName = formvalues["FirstName"];
            U.LastName = formvalues["LastName"];
            U.status = int.Parse(formvalues["status"]);
            us.Update(U);
            us.Commit();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Activation Compte", "Management"));
            message.To.Add(new MailboxAddress(U.FirstName, U.Email));
            message.Subject = "Activation Compte";
            String statusName = "";
            if (U.status == 2)
            {
                statusName = "Manager";
            }
            else if (U.status == 3)
            {
                statusName = "Team Leader";
            }
            else if (U.status == 4)
            {
                statusName = "Member";
            }
            message.Body = new TextPart("plain")
            {

                Text = "Bonjour  " + U.FirstName + " " + "votre compte est activé et vous êtes affecté en tant que " + statusName
            };


            using (var resource = new SmtpClient())
            {
                resource.Connect("smtp.gmail.com", 587, false);
                resource.Authenticate("youthvision.soukmedina@gmail.com", "SOUK2018");
                resource.Send(message);
                resource.Disconnect(true);
            }
            return View();
        }

        public ActionResult Projet()
        {
            return View();

        }



        public ActionResult Client()
        {
            var Users = MyUserService.GetMany(m => !m.status.Equals(1));
            var UserMdel = new List<MAP.Presentation.Models.UserModel>();
            foreach (MAP.Domain.Entities.User U in Users)

            {
                MAP.Presentation.Models.UserModel usermodel = new UserModel
                {
                    Id = U.Id,
                    Email = U.Email,
                    UserName = U.UserName,
                    status = U.status,
                    FirstName = U.FirstName,
                    LastName = U.LastName


                };
                UserMdel.Add(usermodel);


            }
            return View(UserMdel);

        }




        public ActionResult Delete(int Id)
        {


            return View(us.GetById(Id));
        }


        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            User r = us.GetById(Id);

            us.Delete(r);
            us.Commit();



            return View();

        }



    }
}