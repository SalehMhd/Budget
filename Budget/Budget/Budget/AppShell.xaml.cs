using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Budget.Model;
using Budget.ViewModels;

namespace Budget
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        //public double WeeklyAmount { get; set; }
        //public double Budget { get; set; }
        //public double SpentEntry { get; set; }
        //public string SpentArticle { get; set; }
        //public ICommand AddSpentEntryCommand { get; set; }

        //public PeriodExpensesViewModel ExpensesViewModel { get; set; }
        public AppShell()
        {
            InitializeComponent();
           // ExpensesViewModel = new PeriodExpensesViewModel();
            
           // AddSpentEntryCommand = new Command(() =>
           //{
           //    var expense = new Expense
           //    {
           //        ID = Expenses.Max(e => e.ID) + 1,
           //        Amount = SpentEntry,
           //        Article = SpentArticle,
           //        Date = DateTime.Now
           //    };

           //    Expenses.Add(expense);
           //    Task.Run(async () =>
           //    {
           //        var id = await App.Database.SaveExpenseAsync(expense);
           //        Console.WriteLine(id);
           //    });
               

           //    Budget -= SpentEntry;
           //    SpentEntry = 0;
           //    SpentArticle = "";
           //    OnPropertyChanged("Budget");
           //    OnPropertyChanged("SpentEntry");
           //    OnPropertyChanged("SpentArticle");
           //});



           // Budget = 30.67;


           // BindingContext = this;

        }
    }
}
