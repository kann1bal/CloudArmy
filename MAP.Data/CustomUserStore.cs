﻿using MAP.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


 namespace MAP.Data
{
    public class CustomUserStore : UserStore<User, CustomRole, int, CustomLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ProjectContext context) : base(context)
        {
        }
    }
}
