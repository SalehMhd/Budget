using Budget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Budget.ViewModels
{
    public class PeriodExpensesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExpenseItem> Expenses { get; set; }

        public PeriodBudget CurrentPeriodBudget { get; set; }

        public double AmountRest { get; set; }

        public Command LoadExpensesCommand { get; set; }

        public PeriodExpensesViewModel()
        {
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());

            Expenses = new ObservableCollection<ExpenseItem>();
            CurrentPeriodBudget = new PeriodBudget();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async Task ExecuteLoadExpensesCommand()
        {
            var periodBudgets = await App.Database.GetPeriodBudgetsAsync();
            var cpb = periodBudgets.FirstOrDefault(bd => bd.Start < DateTime.Now && bd.End > DateTime.Now);
            if (cpb != null)
            {
                CurrentPeriodBudget.Start = cpb.Start;
                CurrentPeriodBudget.End = cpb.End;
                CurrentPeriodBudget.Amount = cpb.Amount;
                OnPropertyChanged("CurrentPeriodBudget");
            }

            var allExpenses = await App.Database.GetExpensesAsync();
            AmountRest = CurrentPeriodBudget.Amount - allExpenses.Sum(e => e.Amount);
            OnPropertyChanged("AmountRest");
            //Expenses = new ObservableCollection<Expense>(allExpenses.Where(e => e.Date > currentPeriod.Start && e.Date < currentPeriod.End));

            Expenses.Clear();
            var expenseTags = await App.Database.GetExpenseTagsAsync();
            var allTags = await App.Database.GetTagsAsync();

            foreach (var item in allExpenses)
            {
                var tagsIDs = expenseTags.Where(et => et.ExpenseID == item.ID).Select(t => t.TagID); ;
                var tags = new List<Tag>();
                foreach(var tID in tagsIDs)
                {
                    var tag = allTags.FirstOrDefault(t => t.ID == tID);
                    tags.Add(tag);
                }

                Expenses.Add(new ExpenseItem
                {
                    ID = item.ID,
                    Amount = item.Amount,
                    Date = item.Date,
                    Tags = tags
                });
            }
        }


    }
}
