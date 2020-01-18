using Budget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Budget.ViewModels
{
    public class ExpenseViewModel
    {
        private ObservableCollection<Expense> Expenses { get; set; }
        public ICommand AddSpentEntryCommand { get; set; }

        public ExpenseViewModel(ObservableCollection<Expense> expenses)
        {
            this.Expenses = expenses;
            AddSpentEntryCommand = new Command(() =>
            {
                this.Expenses.Add(new Expense
                {
                    ID = 0,
                    Amount = this.Amount,
                    Article = this.Article,
                    Date = this.Date
                });
                AddedExpense.Invoke();
            });
            
        }

        public double Amount { get; set; }
        public string Article { get; set; }
        public DateTime Date { get; set; }
        public Action AddedExpense { get; internal set; }
    }
}
