using MAP.Data.Infrastructure;
using MAP.Domain.Entities;
using MAP.Service.Pattern;
using MAPData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAP.Service.Services
{
    public class TasksService : Service<Tasks>
    {

        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public TasksService() : base(ut){ }

        public List<Tasks> getTasksbyIdProject(int ProjectId,int userId)
        {
            List<Tasks> ListTasks = new List<Tasks>();
            ListTasks = GetMany(b=>b.status!=Statuss.suggested).Where(x => x.ProjectId==ProjectId).Where(k=>k.Id==userId).ToList();
            return ListTasks;
        }


        public List<Tasks> getTasksbyIdSuggested(int ProjectId, int userId)
        {

            List<Tasks> ListTasks = new List<Tasks>();
            ListTasks = GetMany(b => b.status == Statuss.suggested).Where(x => x.ProjectId == ProjectId).Where(k => k.Id == userId).ToList();
            return ListTasks;
        }


        public int CountAllTasks(int a)
        {
            int Done = GetMany().Where(x => x.IsDone == IsDone.Done).Where(x => x.Id == a).Count();
            int notDone = GetMany().Where(x => x.IsDone == IsDone.NotDone).Where(x => x.Id == a).Count();
            return Done+notDone;
        }

        public int CountAllTasksDone(int a)
        {
            int Done = GetMany().Where(x => x.IsDone == IsDone.Done).Where(x => x.Id == a).Count();
            return Done ;
        }

        public int CountAllTasksNotDone(int a)
        {
            int notDone = GetMany().Where(x => x.IsDone == IsDone.NotDone).Where(x => x.Id == a).Count();
            return notDone;
        }



        public int CountAllProject(int a,int b )
        {
            int Done = GetMany().Where(x => x.IsDone == IsDone.Done).Where(x => x.ProjectId == a).Where(y=>y.Id==b).Count();
            int notDone = GetMany().Where(x => x.IsDone == IsDone.NotDone).Where(x => x.ProjectId == a).Count();
            return Done + notDone;
        }

        public int CountAllProjectsDone(int a,int b )
        {
            int Done = GetMany().Where(x => x.IsDone == IsDone.Done).Where(x => x.ProjectId == a).Where(y => y.Id == b).Count();
            return Done;
        }

        public int CountAllProjectNotDone(int a, int b)
        {
            int notDone = GetMany().Where(x => x.IsDone == IsDone.NotDone).Where(x => x.ProjectId == a).Where(y => y.Id == b).Count();
            return notDone;
        }
        public int CalculRate(int id)
        {
            int rate=0;
            Tasks t = GetById(id);
            DateTime now = DateTime.Now;
            if (t.complexity == Complexity.easy)
                {
                    if (t.startDate.Date==now.Date)
                    {
                         if ((now-t.startDate).TotalHours == 3) //3 heures
                             {
                                 rate = 5;
                             }
                         else if ((now - t.startDate).TotalHours == 4)
                             {
                                 rate = 4;
                             }
                        else if ((now - t.startDate).TotalHours == 5 )
                             {
                                rate = 3;
                             }
                    }
                    else if ((t.deadline-now).TotalDays==0)
                    {
                         rate = 1;
                    }
                    else if (now>t.deadline)
                    {
                         rate = 0;
                    }
                    else
                    {
                        rate = 2;
                    }
                }
            else if (t.complexity == Complexity.medium)
                {
                    if (t.startDate.Date == now.Date)
                    {
                        if ((now - t.startDate).TotalHours == 6) // 6 heures
                        {
                            rate = 5;
                        }
                        else if ((now - t.startDate).TotalHours == 12) // 12 heures
                        {
                            rate = 4;
                        }
                        else if ((now - t.startDate).TotalHours == 15) // 15 heures
                        {
                             rate = 3;
                        }
                     }
                    else if ((t.deadline - now).TotalDays == 1) //1 day
                    {
                        rate = 1;
                    }
                    else if (t.deadline > now)
                    {
                        rate = 0;
                    }
                    else
                    {
                        rate = 2;
                    }
            }
            else if (t.complexity == Complexity.hard)
            {
                   
                    if ((t.deadline - now).TotalDays >= 2) // 2 days
                    {
                        rate = 5;
                    }
                    else if (t.deadline > now)
                    {
                        rate = 0;
                    }
                    else if ((t.deadline - now).TotalHours == 6) // 6 heures
                    {
                        rate = 2;
                    }
                    else if ((t.deadline - now).TotalHours == 4) // 4 heures
                    {
                        rate = 2;
                    }
                    else if ((t.deadline-now).TotalHours == 2) // 2 heures
                    {
                        rate = 1;
                    }
                    else
                    {
                        rate = 4;
                    }
                 

            }
            return rate;
        } 


    }
}
