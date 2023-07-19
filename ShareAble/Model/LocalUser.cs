using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ShareAble.Model
{
    public partial class LocalUser : ObservableObject
    {
        [ObservableProperty]
        public string name;
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ObservableProperty]
        public long contactNumber;
        [ObservableProperty]
        public string dOB;
        [ObservableProperty]
        public string imageSource;
        [ObservableProperty]
        public bool hasPartner;
        [ObservableProperty]
        public int partnerID;
        //public string PartnerName { get; set; }
        //public long PartnerContactNumber { get; set; }
        //public string PartnerImageSource { get; set; }
        //public int PartnetID { get; set; }

    }
}

