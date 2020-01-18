using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budget.Model
{
    public class PeriodBudget
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Amount { get; set; }
    }
}
