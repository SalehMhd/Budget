using Budget.Model;
using Budget.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ExpenseStack.BindingContext = ExpenseViewModel = new ExpenseViewModel();
            TagsListView.ItemsSource = ExpenseViewModel.AllTags;
            ExpenseViewModel.AddedExpense = OnAddedExpense;
        }
        public async void OnAddedExpense()
        {
            PeriodExpensesViewModel.LoadExpensesCommand.Execute(null);
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.ExpenseViewModel.LoadTagsCommand.Execute(null);
        }

        private void AllTagsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ExpenseViewModel.Tags.Add(e.SelectedItem as Tag);
            ExpenseViewModel.AllTags.Remove(e.SelectedItem as Tag);
        }

        private void TagsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ExpenseViewModel.Tags.Remove(e.SelectedItem as Tag);
            ExpenseViewModel.AllTags.Add(e.SelectedItem as Tag);
        }
    }
}