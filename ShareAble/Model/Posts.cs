using System;
using SQLite;

namespace ShareAble.Model
{
    public class Posts
    {
        [PrimaryKey, AutoIncrement]
        public int PictureId { get; set; }
        public int UserId { get; set; }
        public int PartnerId { get; set; }
        public byte[] PictureData { get; set; }
        public string Caption { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}

