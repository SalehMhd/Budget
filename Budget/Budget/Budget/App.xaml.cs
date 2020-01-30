using Budget.Model;
using Budget.Services;
using Budget.Services.Database;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Budget
{
    public partial class App : Application
    {
        static BudgetDatabase database;

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            MainPage = new AppShell();
            DependencyService.Register<DataProvidor>();
            //Task.Run(async () =>
            //{
            //    Console.WriteLine("\n MYDEBUG___ ExpensesVM: Init Expenses DB");
            //    await App.Database.InitExpenseAsync();
            //    await App.Database.InitPeriodBudgetAsync();
            //    Console.WriteLine("\n MYDEBUG___ ExpensesVM: Finish Init Expenses DB");
            //});

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static BudgetDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BudgetDatabase();
                }
                return database;
            }
        }
    }
}
