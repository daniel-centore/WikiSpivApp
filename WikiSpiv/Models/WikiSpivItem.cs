using System;
using SQLite;
using WikiSpiv.Data;

namespace WikiSpiv.Models
{

    [Table("wikispiv_items")]
    public class WikiSpivItem : IComparable<WikiSpivItem>
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
        public string Heading { get
            {
                return Name.Substring(0,1);
            }
        }

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

        public int CompareTo(WikiSpivItem other)
        {
            String o1 = Cleanup(this.Name);
            String o2 = Cleanup(other.Name);

            int pos1 = 0;
            int pos2 = 0;
            for (int i = 0; i < Math.Min(o1.Length, o2.Length) && pos1 == pos2; i++)
            {
                pos1 = WikiSpivDatabase.ORDER.IndexOf(o1[i]);
                pos2 = WikiSpivDatabase.ORDER.IndexOf(o2[i]);
            }

            if (pos1 == pos2 && o1.Length != o2.Length)
                return o1.Length - o2.Length;

            return pos1 - pos2;
        }

        private static string Cleanup(String title)
        {
            title = title.ToUpper();
            string result = "";
            foreach (char c in title) {
                if (WikiSpivDatabase.ORDER.Contains(c))
                    result += c;
            }
            return result;
        }
    }
}
