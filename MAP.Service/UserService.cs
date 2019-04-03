using MAP.Data.Infrastructure;
using MAP.Domain.Entities;
using MAP.Service.Pattern;
using MAPData.Infrastructure;
using MAP.Service.Pattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
 public   class UserService : Service<User>, IUserService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public UserService() : base(utk)
        {

        }

        public virtual void updateUser(int id , User user)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection s = new SqlConnection(connectionString))
            {
                s.Open();
                using (SqlCommand cmdSelect = new SqlCommand("Update [MapDB].[dbo].[Users] set status=1 where Id=@id;", s))
                {
                    cmdSelect.CommandType = CommandType.Text;
                    cmdSelect.Parameters.AddWithValue("@id", id);

                    cmdSelect.ExecuteNonQuery();
                  
                }
            }
           
        }
      
    }
}
