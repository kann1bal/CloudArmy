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
    }
}
