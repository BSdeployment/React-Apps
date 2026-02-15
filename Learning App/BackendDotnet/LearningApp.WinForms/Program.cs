using LearningApp.Data.DB;
using LearningApp.WinForms.RequestServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningApp.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
       

        [STAThread]
        static void Main()
        {
          System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var serviceProvider = Bootstrapper.ConfigureServices();

            //var init = new DatabaseInitializer(new SQLiteConnectionFactory("Data Source=learning.db"));

            //await init.InitializeAsync();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider
                    .GetRequiredService<DatabaseInitializer>();

                dbInitializer.InitializeAsync()
                   .GetAwaiter().GetResult();
            }




            var mainForm = serviceProvider.GetRequiredService<MainForm>();
          
            System.Windows.Forms.Application.Run(mainForm);
        }
    }
}
