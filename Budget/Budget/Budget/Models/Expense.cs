using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budget.Model
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //it was int before
        public double Amount { get; set; }
        public string Article { get; set; }
        public DateTime Date { get; set; }
    }
}
