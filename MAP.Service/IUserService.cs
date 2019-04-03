using MAP.Domain.Entities;
using MAP.Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
   public interface IUserService : IService<User>
    {
        void updateUser(int id, User user);
    }
}
