using MAP.Data.Infrastructure;
using MAP.Domain.Entities;
using MAP.Service.Pattern;
using MAPData.Infrastructure;
using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Service
{
    public class MeetingService:Service<Meeting>, IMeetingService
    {
        static IDataBaseFactory Factory = new DataBaseFactory(); //usine de fabrication du contexte
        static IUnitOfWork utk = new UnitOfWork(Factory); //communication avec la base
        public MeetingService():base(utk)
        {

        }

        public Meeting GetMeetingById(int Id)
        {
            return utk.getRepository<Meeting>().GetById(Id);
        }

        public IEnumerable<Meeting> SearchMeetingsByName(string searchString)
        {
            IEnumerable<Meeting> MeetingsDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                MeetingsDomain = GetMany(x => x.Title.Contains(searchString));
            }
            return MeetingsDomain;
        }

        public int GetNbMeetingBefore(DateTime Date)
        {
            return utk.getRepository<Meeting>().GetMany().Where(e => e.Date < Date).Count();
        }

        public int GetNbMeetingAfter(DateTime Date)
        {
            return utk.getRepository<Meeting>().GetMany().Where(e => e.Date > Date).Count();
        }

        public int GetNbMeeting(DateTime Date)
        {
            return utk.getRepository<Meeting>().GetMany().Where(e => e.Date == Date).Count();
        }


        /*ProjectService pservice;
        public String GetProjectNameById(int id)
        {
            
            Project P = pservice.GetById(id);
            return P.Name;

        }*/


    }



    
}
