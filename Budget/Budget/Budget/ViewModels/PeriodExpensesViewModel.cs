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

        public Command LoadExpensesCommand { get; set; }

        public PeriodExpensesViewModel()
        {
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());

            Expenses = new ObservableCollection<ExpenseItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async Task ExecuteLoadExpensesCommand()
        {
            var periodBudgets = await App.Database.GetPeriodBudgetsAsync();
            var currentPeriod = periodBudgets.FirstOrDefault(bd => bd.Start < DateTime.Now && bd.End > DateTime.Now);

            var allExpenses = await App.Database.GetExpensesAsync();
            //Expenses = new ObservableCollection<Expense>(allExpenses.Where(e => e.Date > currentPeriod.Start && e.Date < currentPeriod.End));
            Expenses.Clear();
            foreach(var item in allExpenses)
            {
                var expenseTags = await App.Database.GetExpenseTagsAsync(item.ID);
                var tagsIDs = expenseTags.Select(t => t.TagID);
                var tags = new List<Tag>();
                foreach(var tID in tagsIDs)
                {
                    var tag = await App.Database.GetTagAsync(tID);
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
