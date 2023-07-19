using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ShareAble.Model
{
    public partial class Posts : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int PictureId { get; set; }
        public int UserId { get; set; }
        public int PartnerId { get; set; }

        [ObservableProperty]
        public byte[] pictureData;
        [ObservableProperty]
        public string caption;
        [ObservableProperty]
        public DateTime timestamp;
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string userImage;
        [ObservableProperty]
        public string address;
    }
}

