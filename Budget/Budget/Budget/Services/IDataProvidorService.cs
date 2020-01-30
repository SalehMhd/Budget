using Budget.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services
{
    public interface IDataProvidorService
    {
        Task<List<ExpenseItem>> GetExpenses();
        Task<List<PeriodBudget>> GetBudgets();
        Task<List<Tag>> GetTags();
        Task AddExpense(ExpenseItem item);
        Task AddBudget(PeriodBudget periodBudget);
        Task AddTag(Tag newTag);
    }
}
