using Budget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Budget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public PeriodExpensesViewModel PeriodExpensesViewModel { get; set; }
        public ExpenseViewModel ExpenseViewModel { get; set; }
        public AddExpensePage(PeriodExpensesViewModel periodExpensesViewModel)
        {
            InitializeComponent();
            this.PeriodExpensesViewModel = periodExpensesViewModel;
            ExpenseStack.BindingContext = ExpenseViewModel = new ExpenseViewModel(periodExpensesViewModel.Expenses);
            ExpenseViewModel.AddedExpense = OnAddedExpense;
        }
        public async void OnAddedExpense()
        {
            await Navigation.PopModalAsync();
        }
    }
}