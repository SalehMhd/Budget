using Budget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.ViewModels
{
    public class PeriodBudgetViewModel
    {
        public ObservableCollection<PeriodBudget> PeriodBudgets { get; set; }
        public PeriodBudgetViewModel()
        {
            Task.Run(async () =>
            {
                PeriodBudgets = new ObservableCollection<PeriodBudget>(await App.Database.GetPeriodBudgetsAsync());
            });
        }
    }
}
