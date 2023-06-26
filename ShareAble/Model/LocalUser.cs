using System;
using SQLite;

namespace ShareAble.Model
{
	public class LocalUser
	{
        public string Name { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long ContactNumber { get; set; }
        public string DOB { get; set; }
        public string ImageSource { get; set; }
        public bool HasPartner { get; set; }
        //public string PartnerName { get; set; }
        //public long PartnerContactNumber { get; set; }
        //public string PartnerImageSource { get; set; }
        //public int PartnetID { get; set; }

    }
}

