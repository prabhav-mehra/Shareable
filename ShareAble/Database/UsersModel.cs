using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareAble.Database
{
    public class UsersModel
    {
        public string Name { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long ContactNumber { get; set; }
        public string DOB { get; set; }
        public bool HasPartner { get; set; }
        public string PartnerName { get; set; }
        public long PartnerContactNumber { get; set; }



    }
}