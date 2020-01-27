using System;
using System.Collections.Generic;
using System.Text;

namespace Budget.Model
{
    public class ExpenseItem
    {
        public int ID { get; set; } //it was int before
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
