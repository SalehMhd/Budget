using Budget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                var expense = new Expense
                {
                    ID = 0,
                    Amount = double.Parse(this.Amount),
                    Date = this.Date
                };

                var id = await App.Database.SaveExpenseAsync(expense);
                foreach(var tag in Tags)
                {
                    var etag = new ExpenseTag
                    {
                        ExpenseID = id,
                        TagID = tag.ID
                    };
                    await App.Database.SaveExpenseTagAsync(etag);
                }

                AddedExpense.Invoke();
            });
            LoadTagsCommand = new Command(async () => await ExecuteLoadTagsCommand());

        }

        async Task ExecuteLoadTagsCommand()
        {
            var tags = await App.Database.GetTagsAsync();
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
