using Budget.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services.Database
{
    public class BudgetDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public BudgetDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Expense).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, new Type[] { typeof(Expense), typeof(PeriodBudget), typeof(Tag), typeof(ExpenseTag) } ).ConfigureAwait(false);
                    //Trace.WriteLine("\n MYDEBUG___ Create: Expense Table");
                    //await InitExpenseAsync();
                    //await InitPeriodBudgetAsync();
                    //Trace.WriteLine("\n MYDEBUG___ Insert: Expense Table");
                }

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(PeriodBudget).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(PeriodBudget));//.ConfigureAwait(false);
                //    //Trace.WriteLine("\n MYDEBUG___ Create: PeriodBudget Table");
                //    //await InitPeriodBudgetAsync();
                //    //Trace.WriteLine("\n MYDEBUG___ Create: PeriodBudget Table");
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Tag).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(Tag));//.ConfigureAwait(false);
                //    //Trace.WriteLine("\n MYDEBUG___ Create: PeriodBudget Table");
                //    //await InitPeriodBudgetAsync();
                //    //Trace.WriteLine("\n MYDEBUG___ Create: PeriodBudget Table");
                //}

                Trace.Write("\n MYDEBUG___ Existing Expense Table: " + Database.TableMappings.Any(m => m.MappedType.Name == typeof(Expense).Name));
                Trace.Write("\n MYDEBUG___ Existing PeriodBudget Table: " + Database.TableMappings.Any(m => m.MappedType.Name == typeof(PeriodBudget).Name));

                initialized = true;
            }
        }

        public async System.Threading.Tasks.Task InitExpenseAsync()
        {
            var e1 = new Expense
            {
                ID = 0,
                Amount = 2.2,
                Date = new DateTime(2020, 1, 13, 0, 0, 1)
            };

            var e2 = new Expense
            {
                ID = 0,
                Amount = 3.2,
                Date = new DateTime(2020, 1, 14, 0, 0, 1)
            };

            var e3 = new Expense
            {
                ID = 0,
                Amount = 4.2,
                Date = new DateTime(2020, 1, 15, 0, 0, 1)
            };

            var ex1 = await SaveExpenseAsync(e1);
            Trace.WriteLine("\n MYDEBUG___ Insert: Expense ID1 " + ex1);
            var ex2 = await SaveExpenseAsync(e2);
            Trace.WriteLine("\n MYDEBUG___ Insert: Expense ID2 " + ex2);
            var ex3 = await SaveExpenseAsync(e3);
            Trace.WriteLine("\n MYDEBUG___ Insert: Expense ID3 " + ex3);
        }
        public async Task InitPeriodBudgetAsync()
        {
            var periodBudget1 = new PeriodBudget
            {
                ID = 0,
                Start = new DateTime(2020, 1, 5, 0, 0, 1),
                End = new DateTime(2020, 1, 11, 23, 59, 59),
                Amount = 50
            };

            var periodBudget2 = new PeriodBudget
            {
                ID = 0,
                Start = new DateTime(2020, 1, 11, 0, 0, 1),
                End = new DateTime(2020, 1, 18, 23, 59, 59),
                Amount = 50
            };

            var periodBudget3 = new PeriodBudget
            {
                ID = 0,
                Start = new DateTime(2020, 1, 18, 0, 0, 1),
                End = new DateTime(2020, 1, 25, 23, 59, 59),
                Amount = 50
            };
            var pd1 = await SavePeriodBudgetAsync(periodBudget1);
            Trace.WriteLine("\n MYDEBUG___ Insert: PeriodBudget ID1 " + pd1);
            var pd2 = await SavePeriodBudgetAsync(periodBudget2);
            Trace.WriteLine("\n MYDEBUG___ Insert: PeriodBudget ID2 " + pd2);
            var pd3 = await SavePeriodBudgetAsync(periodBudget3);
            Trace.WriteLine("\n MYDEBUG___ Insert: PeriodBudget ID3 " + pd3);
        }

        public Task<List<Expense>> GetExpensesAsync()
        {
            return Database.Table<Expense>().ToListAsync();
        }

        public Task<List<Expense>> GetExpensesNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Expense>("SELECT * FROM [Expense] WHERE [Done] = 0");
        }

        public Task<Expense> GetExpenseAsync(int id)
        {
            return Database.Table<Expense>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveExpenseAsync(Expense expense)
        {
            //if (expense.ID != 0)
            //{
            //    return Database.UpdateAsync(expense);
            //}
            //else
            //{
                return Database.InsertAsync(expense);
            //}
        }

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return Database.DeleteAsync(expense);
        }

        public Task<List<PeriodBudget>> GetPeriodBudgetsAsync()
        {
            return Database.Table<PeriodBudget>().ToListAsync();
        }

        public Task<PeriodBudget> GetPeriodBudgetAsync(int id)
        {
            return Database.Table<PeriodBudget>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePeriodBudgetAsync(PeriodBudget budget)
        {
            if (budget.ID != 0)
            {
                return Database.UpdateAsync(budget);
            }
            else
            {
                return Database.InsertAsync(budget);
            }
        }

        public Task<int> DeletePeriodBudgetAsync(PeriodBudget budget)
        {
            return Database.DeleteAsync(budget);
        }

        public Task<List<Tag>> GetTagsAsync()
        {
            return Database.Table<Tag>().ToListAsync();
        }

        public Task<int> SaveTagAsync(Tag tag)
        {
            if (tag.ID != 0)
            {
                return Database.UpdateAsync(tag);
            }
            else
            {
                return Database.InsertAsync(tag);
            }
        }

        public Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return Database.Table<ExpenseTag>().ToListAsync();
        }

        public Task<int> SaveExpenseTagAsync(ExpenseTag etag)
        {
            return Database.InsertAsync(etag);
        }

    }
}
