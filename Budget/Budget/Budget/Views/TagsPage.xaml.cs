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
    public partial class TagsPage : ContentPage
    {
        public TagsViewModel TagsViewModel { get; set; }
        public TagsPage()
        {
            InitializeComponent();
            TagsListView.BindingContext = TagsViewModel = new TagsViewModel();
            TagsViewModel.TagAdded = OnTagAdded;
        }

        private async void OnTagAdded()
        {
            await Navigation.PopModalAsync();
        }

        private async void AddTag_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddTagPage(this.TagsViewModel)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TagsViewModel.LoadTagsCommand.Execute(null);
        }

    }
}