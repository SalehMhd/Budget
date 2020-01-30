using Budget.Model;
using Budget.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Budget.ViewModels
{
    public class PeriodBudgetViewModel
    {
        public ObservableCollection<PeriodBudget> PeriodBudgets { get; set; }

        public DateTime NewStart { get; set; }
        public DateTime NewEnd { get; set; }
        public double NewAmount { get; set; }

        public Command LoadPeriodBudgetsCommand { get; set; }
        public ICommand AddPeriodBudgetCommand { get; set; }
        public Action PeriodBudgetAdded { get; set; }
        public PeriodBudgetViewModel()
        {
            PeriodBudgets = new ObservableCollection<PeriodBudget>();

            NewStart = DateTime.Now;
            NewEnd = DateTime.Now;

            LoadPeriodBudgetsCommand = new Command(async () =>
            {
                var providor = DependencyService.Get<IDataProvidorService>();
                var budgetsList = await providor.GetBudgets();
                PeriodBudgets.Clear();
                foreach (var budget in budgetsList)
                {
                    PeriodBudgets.Add(budget);
                }
            });

            AddPeriodBudgetCommand = new Command(async () =>
            {
                var budget = new PeriodBudget
                {
                    Start = this.NewStart,
                    End = this.NewEnd,
                    Amount = this.NewAmount
                };
                var providor = DependencyService.Get<IDataProvidorService>();
                await providor.AddBudget(budget);
                LoadPeriodBudgetsCommand.Execute(null);
                PeriodBudgetAdded.Invoke();
            });
        }
    }
}
