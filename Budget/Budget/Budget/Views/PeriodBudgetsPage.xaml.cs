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
    public partial class PeriodBudgetsPage : ContentPage
    {
        public PeriodBudgetViewModel PeriodBudgetViewModel { get; set; }
        public PeriodBudgetsPage()
        {
            InitializeComponent();
            BudgetsListView.BindingContext = PeriodBudgetViewModel = new PeriodBudgetViewModel();
            PeriodBudgetViewModel.PeriodBudgetAdded = OnPeriodBudgetAdded;
        }

        private async void OnPeriodBudgetAdded()
        {
            await Navigation.PopModalAsync();
        }

        private async void AddPeriodBudget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddPeriodBudgetPage(this.PeriodBudgetViewModel)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PeriodBudgetViewModel.LoadPeriodBudgetsCommand.Execute(null);
        }
    }
}