using MAP.Data;
using MAP.Domain.Entities;
using MAP.Service.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MAP.Presentation.WebAPi
{
    public class TaskssController : ApiController
    {
        private ProjectContext db = new ProjectContext();
        public static TasksService cs;

    public TaskssController() {
            cs = new TasksService(); }


        public IEnumerable<Tasks> GetTests()
        {
            var tasks = cs.GetMany();
            List<Tasks> lpm = new List<Tasks>();
            foreach (var item in tasks)
                lpm.Add(new Tasks
                {
                    taskId = item.taskId,
                    Description = item.Description,
                    deadline = item.deadline,
                    estimation = item.estimation,
                    IsDone= item.IsDone,
                    SpentTime=item.SpentTime,
                    startDate=item.startDate,
                    status=item.status,
                    complexity=item.complexity,
                    progress=item.progress


                });
            return lpm;

        }

        // GET: api/Tests/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult GetTest(int id)
        {
            var List = cs.getTasksbyIdSuggested(id,2);



            if (List == null)
            {
                return NotFound();
            }

            return Ok(List);
        }
        [System.Web.Http.HttpPost]

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("api/Create")]
        public IHttpActionResult Create(Tasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.Entry(task).State = EntityState.Modified;

            try
            {
                cs.Add(task);
                cs.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!TasksExists(task.taskId))
                //{
                return NotFound();
                //}
                //else
                //{
                //   throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Tasks))]
        public IHttpActionResult DeleteTest(int id)
        {
            Tasks tasks = cs.GetById(id);

            if (tasks == null)
            {
                return NotFound();
            }

            cs.Delete(tasks);
            cs.Commit();

            return Ok(tasks);
        }

    }


}
