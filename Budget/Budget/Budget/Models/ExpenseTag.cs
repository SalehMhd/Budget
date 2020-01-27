using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budget.Model
{
    public class ExpenseTag
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //it was int before

        public int ExpenseID { get; set; }
        public int TagID { get; set; }
    }
}
