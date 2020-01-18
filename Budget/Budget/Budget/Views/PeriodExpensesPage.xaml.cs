using Budget.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Budget.Views
{
    public class AmountToTxtColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var amount = double.Parse(value.ToString());

            if (amount > 0)
            {
                return Color.Green;
            }
            else
            {
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeriodExpensesPage : ContentPage
    {
        public PeriodExpensesViewModel ExpensesViewModel { get; set; }
        public CurrentPeriodBudgetViewModel BudgetViewModel { get; set; }

        public PeriodExpensesPage()
        {
            InitializeComponent();
            ExpensesGrid.BindingContext = ExpensesViewModel = new PeriodExpensesViewModel();
            CurrentPeriodStack.BindingContext = BudgetViewModel = new CurrentPeriodBudgetViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await BudgetViewModel.LoadCurrentPeriodBudget();

            //if (ExpensesViewModel.Expenses == null)
            //{
            ExpensesViewModel.LoadExpensesCommand.Execute(null);
            //}
        }

        private async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddExpensePage(this.ExpensesViewModel)));
        }
    }
}