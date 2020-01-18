using Budget.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Budget.ViewModels
{
    public class PeriodExpensesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public Command LoadExpensesCommand { get; set; }

        public PeriodExpensesViewModel()
        {
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());

            Expenses = new ObservableCollection<Expense>();
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
                Expenses.Add(item);
            }
        }


    }
}
