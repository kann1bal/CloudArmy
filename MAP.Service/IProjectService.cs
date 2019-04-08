﻿using MAP.Domain.Entities;
using MAP.Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
   public interface IProjectService:IService<Project>
    {
        IEnumerable<Project> SearchProjectsByName(string searchString);
        void AddProject(Project p);
        void EditProject(Project p);
        Project GetProjectById(int id);
        Project GetProjectById1(string currentUser);
        IEnumerable<Project> GetPRojectAscending();
        List<Project> GetProjectById1();
        //void CommitAddProject();
    }
}
