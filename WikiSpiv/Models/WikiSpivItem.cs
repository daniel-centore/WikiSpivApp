using System;
using SQLite;

namespace WikiSpiv.Models
{

    [Table("wikispiv_items")]
    public class WikiSpivItem
    {
        //public const string TABLE_NAME = "wikispiv_items";

        // * Only add to the bottom
        // * Do not reorder
        // * Do not remove

        // NOTE: This will be -1 until updated from the DB
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string GUID { get; set; }
        public long UpdateTime { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public WikiSpivItem(long updateTime, string type, String name, String content): this()
        {
            this.UpdateTime = updateTime;
            this.Type = type;
            this.Name = name;
            this.Content = content;
        }

        public WikiSpivItem()
        {
            GUID = Guid.NewGuid().ToString();
            ID = -1;
        }
    }
}
