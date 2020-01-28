using Budget.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.ViewModels
{
    public class CurrentPeriodBudgetViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PeriodBudget CurrentPeriodBudget { get; set; }
        public double AmountRest { get; set; }
        public CurrentPeriodBudgetViewModel()
        { }

        public async Task LoadCurrentPeriodBudget(System.Collections.ObjectModel.ObservableCollection<ExpenseItem> expenses)
        {
            var budgets = await App.Database.GetPeriodBudgetsAsync();
            CurrentPeriodBudget = budgets.FirstOrDefault(b => b.Start < DateTime.Now && b.End > DateTime.Now);
            AmountRest = CurrentPeriodBudget.Amount - expenses.Sum(e => e.Amount);

            OnPropertyChanged("CurrentPeriodBudget");
            OnPropertyChanged("AmountRest");
        }
    }
}
