using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budget.Model
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
