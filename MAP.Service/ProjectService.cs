using MAP.Data.Infrastructure;
using MAP.Domain.Entities;
using MAP.Service.Pattern;
using MAPData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
  public  class ProjectService:Service<Project>, IProjectService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public ProjectService() : base(utk)
        {


        }


        //Recherche
        public IEnumerable<Project> SearchProjectsByName(string searchString)
        {
            IEnumerable<Project> ProjectsDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                ProjectsDomain = GetMany(x => x.Title.Contains(searchString));
            }




            return ProjectsDomain;
        }


        public Project GetProjectById(int id)
        {
            return utk.getRepository<Project>().GetById(id);
        }
        public Project GetProjectById1(string currentUser)
        {
            return utk.getRepository<Project>().GetById(currentUser);
        }
        public void EditProject(Project p)
        {
            utk.getRepository<Project>().Update(p);
            utk.Commit();
        }

        public List<Project> GetProjectById1()
        {
            return utk.getRepository<Project>().GetMany().ToList();
        }

        public void AddProject(Project p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetPRojectAscending()
        {
            throw new NotImplementedException();
        }

        //public void CommitAddProject()
        //{
        //    uow.Commit();
        //}
    }
}
