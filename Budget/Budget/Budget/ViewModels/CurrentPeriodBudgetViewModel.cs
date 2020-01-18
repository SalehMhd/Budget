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
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PeriodBudget CurrentPeriodBudget { get; set; }
        public CurrentPeriodBudgetViewModel()
        { }

        public async Task LoadCurrentPeriodBudget()
        {
            var budgets = await App.Database.GetPeriodBudgetsAsync();
            CurrentPeriodBudget = budgets.FirstOrDefault(b => b.Start < DateTime.Now && b.End > DateTime.Now);
            OnPropertyChanged("CurrentPeriodBudget");
        }
    }
}
