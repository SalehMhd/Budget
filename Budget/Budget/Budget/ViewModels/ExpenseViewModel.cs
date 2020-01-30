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
    public class ExpenseViewModel
    {
        public ObservableCollection<Tag> Tags { get; set; }
        public ObservableCollection<Tag> AllTags { get; set; }

        public ICommand AddSpentEntryCommand { get; set; }
        public Command LoadTagsCommand { get; set; }

        public ExpenseViewModel()
        {
            this.Tags = new ObservableCollection<Tag>();
            AllTags = new ObservableCollection<Tag>();

            this.Date = DateTime.Now;
            AddSpentEntryCommand = new Command(async () =>
            {
                var expense = new ExpenseItem
                {
                    Amount = double.Parse(this.Amount),
                    Date = this.Date,
                    Tags = this.Tags.ToList()
                };

                var providor = DependencyService.Get<IDataProvidorService>();
                await providor.AddExpense(expense);

                this.Amount = "";
                this.Date = DateTime.Now;
                this.Tags.Clear();

                AddedExpense.Invoke();
            });
            LoadTagsCommand = new Command(async () => await ExecuteLoadTagsCommand());

        }

        async Task ExecuteLoadTagsCommand()
        {
            var providor = DependencyService.Get<IDataProvidorService>();
            var tags = await providor.GetTags();
            AllTags.Clear();
            foreach (var tag in tags)
            {
                AllTags.Add(tag);
            }
        }


        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public Action AddedExpense { get; internal set; }
    }
}
