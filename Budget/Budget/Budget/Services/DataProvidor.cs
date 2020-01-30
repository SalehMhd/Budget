using Budget.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services
{
    public class DataProvidor : IDataProvidorService
    {
        public DataProvidor()
        {

        }

        private List<ExpenseItem> expenses;
        public async Task<List<ExpenseItem>> GetExpenses() { 
            if(expenses == null)
            {
                expenses = new List<ExpenseItem>();

                var allExpenses = await App.Database.GetExpensesAsync();

                var expenseTags = await App.Database.GetExpenseTagsAsync();

                var allTags = await this.GetTags();

                foreach (var item in allExpenses)
                {
                    var tagsIDs = expenseTags.Where(et => et.ExpenseID == item.ID).Select(t => t.TagID); ;
                    var tags = new List<Tag>();
                    foreach (var tID in tagsIDs)
                    {
                        var tag = allTags.FirstOrDefault(t => t.ID == tID);
                        tags.Add(tag);
                    }

                    expenses.Add(new ExpenseItem
                    {
                        ID = item.ID,
                        Amount = item.Amount,
                        Date = item.Date,
                        Tags = tags
                    });
                }
            }
            return expenses;
        }

        private List<PeriodBudget> budgets;
        public async Task<List<PeriodBudget>> GetBudgets()
        {
            if(budgets == null)
            {
                budgets = await App.Database.GetPeriodBudgetsAsync();
            }
            return budgets;
        }

        private List<Tag> tags;
        public async Task<List<Tag>> GetTags() 
        {
            if(tags == null)
            {
                tags = await App.Database.GetTagsAsync();
            }
            return tags;
        }

        public async Task AddExpense(ExpenseItem item)
        {
            var expense = new Expense
            {
                Amount = item.Amount,
                Date = item.Date
            };

            await App.Database.SaveExpenseAsync(expense);

            var dbExpenses = await App.Database.GetExpensesAsync();
            var id = dbExpenses.Max(e => e.ID);

            foreach (var tag in item.Tags)
            {
                var etag = new ExpenseTag
                {
                    ExpenseID = id,
                    TagID = tag.ID
                };
                await App.Database.SaveExpenseTagAsync(etag);
            }

            expenses.Add(new ExpenseItem
            {
                Amount = expense.Amount,
                Date = expense.Date,
                ID = id,
                Tags = item.Tags
            });
        }
        public async Task AddBudget(PeriodBudget periodBudget)
        {
            var budget = new PeriodBudget
            {
                Start = periodBudget.Start,
                End = periodBudget.End,
                Amount = periodBudget.Amount
            };
            await App.Database.SavePeriodBudgetAsync(budget);
            budgets.Add(budget);
        }
        public async Task AddTag(Tag newTag)
        {
            var tag = new Tag
            {
                Text = newTag.Text
            };
            await App.Database.SaveTagAsync(tag);
            tags.Add(tag);
        }

    }
}
