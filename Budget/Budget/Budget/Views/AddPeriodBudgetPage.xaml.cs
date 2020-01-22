using Budget.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Budget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPeriodBudgetPage : ContentPage
    {
        public PeriodBudgetViewModel PeriodBudgetViewModel { get; set; }
        public AddPeriodBudgetPage(PeriodBudgetViewModel periodBudgetViewModel)
        {
            InitializeComponent();
            this.PeriodBudgetViewModel = periodBudgetViewModel;
            BudgetStack.BindingContext = this.PeriodBudgetViewModel;
        }
    }
}