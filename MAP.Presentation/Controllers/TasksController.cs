using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using MAP.Domain.Entities;
using MAP.Service;

using MAP.Service.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Controllers
{
    public class TasksController : Controller
    {
        public string x;
        public TasksService ts = new TasksService();
        public UserService us = new UserService();
        public static int? ProjectId;
        // GET: Tasks
        [Authorize]
        public ActionResult Index(int ProjectId)
        {
            ViewBag.id = ProjectId;
            var List = ts.getTasksbyIdProject(ProjectId,AccountController.CurrentUserId);
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

        // GET: Tasks/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Tasks t = ts.GetById(id);
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

            return View(t);
        }
        public ActionResult EditDone(int id)
        {
            Tasks t = ts.GetById(id);
            User u = us.GetById((int)t.Id);
            TasksController.ProjectId = t.ProjectId;
            t.IsDone = IsDone.Done;
            t.SpentTime = (DateTime.Now - t.startDate).TotalDays;
            t.rate = ts.CalculRate(id);
            ts.Update(t);
            SendMail(u.Email);
            ts.Commit();
            
            return RedirectToAction("Details", new { id=t.taskId });
        }
        public ActionResult EditProgress1(int id)
        {
            Tasks t = ts.GetById(id);
            TasksController.ProjectId = t.ProjectId;
            if (t.progress != Progress.level4)
            {
                if (t.progress == Progress.level0)
                    t.progress = Progress.level1;
                else if (t.progress == Progress.level1)
                    t.progress = Progress.level2;
                else if (t.progress == Progress.level2)
                    t.progress = Progress.level3;
                else if (t.progress == Progress.level3)
                    t.progress = Progress.level4;
                ts.Update(t);
                ts.Commit();
            }
            return RedirectToAction("Details", new { id = t.taskId });
        }
        public ActionResult EditProgress2(int id)
        {
            Tasks t = ts.GetById(id);
            TasksController.ProjectId = t.ProjectId;
            if (t.progress != Progress.level0)
            {
                if (t.progress == Progress.level4)
                    t.progress = Progress.level3;
                else if (t.progress == Progress.level3)
                    t.progress = Progress.level2;
                else if (t.progress == Progress.level2)
                    t.progress = Progress.level1;
                else if (t.progress == Progress.level1)
                    t.progress = Progress.level0;
                ts.Update(t);
                ts.Commit();
            }
            return RedirectToAction("Details", new { id = t.taskId });
        }

        // GET: Tasks/Create
        public ActionResult Create(int ProjectId)
        {
            TasksController.ProjectId = ProjectId;
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

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(Tasks t)
        {
            try
            {
                // TODO: Add insert logic here
                t.ProjectId = TasksController.ProjectId;
                t.Id = AccountController.CurrentUserId;
                t.status = Statuss.suggested;
                t.progress = Progress.level0;
                t.IsDone = IsDone.NotDone;
                t.rate = -1;
                ts.Add(t);
                ts.Commit();
                return RedirectToAction("AfficherSuggestion", new { ProjectId = t.ProjectId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            Tasks t = ts.GetById(id);
            int? projectId = t.ProjectId;
            ts.Delete(t);
            ts.Commit();
            return RedirectToAction("AfficherSuggestion", new { ProjectId = projectId });
        }


        //************************Pie chart************************

        public ActionResult PieChart()
        {
            long pourcentageDone=0, pourcentageNotDone=0;
            int total = (int)ts.CountAllTasks(AccountController.CurrentUserId);
            if (total != 0)
            {
                pourcentageDone = (long)((ts.CountAllTasksDone(AccountController.CurrentUserId) * 100) / total);
                pourcentageNotDone = (long)((ts.CountAllTasksNotDone(AccountController.CurrentUserId) * 100) / total);
            }
           

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "The progress of your tasks " })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            //Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Browser share",
                    Data = new DotNet.Highcharts.Helpers.Data(new object[]
                    {
                        new object[] { "Done", pourcentageDone },
                        new object[] { "NotDone", pourcentageNotDone }
                    }
                    )
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
            return View(chart);
        }


        //*************************Bar chart*********


        public ActionResult BarChart(int projectId)
        {
            Highcharts chart = new Highcharts("chart")
             .InitChart(new Chart { Type = ChartTypes.Column, Margin = new[] { 50, 50, 100, 80 } })
             .SetTitle(new Title { Text = "Your progress in this project " })
             .SetXAxis(new XAxis
             {
                 Categories = new[]
                                {
                        "Not Done", "Done"
                                },
                 Labels = new XAxisLabels
                 {
                     Rotation = -45,
                     Align = HorizontalAligns.Right,
                     Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif'"
                 }
             })
                            .SetYAxis(new YAxis
                            {
                                Min = 0,
                                Title = new YAxisTitle { Text = "Number of tasks" }
                            })
                            .SetLegend(new Legend { Enabled = false })
                            .SetTooltip(new Tooltip { Formatter = "TooltipFormatter" })
                            .SetPlotOptions(new PlotOptions
                            {
                                Column = new PlotOptionsColumn
                                {
                                    DataLabels = new PlotOptionsColumnDataLabels
                                    {
                                        Enabled = true,
                                        Rotation = -90,
                                        Color = ColorTranslator.FromHtml("#FFFFFF"),
                                        Align = HorizontalAligns.Right,
                                        X = 4,
                                        Y = 10,
                                        Formatter = "function() { return this.y; }",
                                        Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif',textShadow: '0 0 3px black'"
                                    }
                                }
                            })
                            .SetSeries(new Series
                            {
                                Name = "Population",
                                Data = new DotNet.Highcharts.Helpers.Data(new object[]
                                {
                        ts.CountAllProjectNotDone(AccountController.CurrentUserId,projectId), ts.CountAllProjectsDone(AccountController.CurrentUserId,projectId)
                                }),
                            });

            return View(chart);
        }

        //******************************* Mail*************************************************
        public void SendMail(string mail)
        {
            
            
            String date = DateTime.Now.ToString();

            MailMessage mailMessage = new MailMessage("cloud.army2019@gmail.com", mail);
            mailMessage.Subject = "Mail ";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = (string)System.IO.File.ReadAllText(Server.MapPath("~/Mail/mail.html"));
            SmtpClient smtpClient = new SmtpClient();
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "cloud.army2019@gmail.com",
                Password = "Cloudarmy19."
            };
            //smtpClient.UseDefaultCredentials = false;
            smtpClient.Send(mailMessage);


        }
        //************************************ Suggestion***************************
        public ActionResult AfficherSuggestion(int ProjectId)
        {
            ViewBag.id = ProjectId;
            
            var List = ts.getTasksbyIdSuggested(ProjectId, AccountController.CurrentUserId);
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


        public ActionResult EditSugg(int id)
        {
            Tasks t = ts.GetById(id);
            TasksController.ProjectId = t.ProjectId;
            return View(ts.GetById(id));
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public ActionResult EditSugg(int id, Tasks t)
        {
            try
            {
                Tasks ke = ts.GetById(id);
                TasksController.ProjectId = ke.ProjectId;
                ke.Description = t.Description;
                ke.deadline = t.deadline;
                ke.complexity = t.complexity;
                ke.estimation = t.estimation;
                ts.Update(ke);
                ts.Commit();
                return RedirectToAction("AfficherSuggestion", new { ProjectId = ke.ProjectId });
            }
            catch
            {
                return View();
            }
        }



    }
}
