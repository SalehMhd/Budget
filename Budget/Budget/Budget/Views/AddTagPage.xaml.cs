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
    public partial class AddTagPage : ContentPage
    {
        public TagsViewModel TagsViewModel { get; set; }
        public AddTagPage(TagsViewModel tagsViewModel)
        {
            InitializeComponent();
            this.TagsViewModel = tagsViewModel;
            TagStack.BindingContext = this.TagsViewModel;
        }
    }
}