using Budget.Model;
using Budget.Services;
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
            var dataProvidor = DependencyService.Get<IDataProvidorService>();

            var periodBudgets = await dataProvidor.GetBudgets();
            var cpb = periodBudgets.FirstOrDefault(bd => bd.Start < DateTime.Now && bd.End > DateTime.Now);
            if (cpb != null)
            {
                CurrentPeriodBudget.Start = cpb.Start;
                CurrentPeriodBudget.End = cpb.End;
                CurrentPeriodBudget.Amount = cpb.Amount;
                OnPropertyChanged("CurrentPeriodBudget");
            }

            var expenses = await dataProvidor.GetExpenses();
            AmountRest = CurrentPeriodBudget.Amount - expenses.Sum(e => e.Amount);
            OnPropertyChanged("AmountRest");

            Expenses.Clear();

            foreach (var item in expenses)
            {
                Expenses.Add(new ExpenseItem
                {
                    ID = item.ID,
                    Amount = item.Amount,
                    Date = item.Date,
                    Tags = item.Tags
                });
            }
        }


    }
}
