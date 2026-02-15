using LearningApp.Application.Services;
using LearningApp.Data.DB;
using LearningApp.Data.Interfaces;
using LearningApp.Data.Repositories;
using LearningApp.WinForms.RequestServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.WinForms
{
    public static class Bootstrapper
    {

        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var appDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Learning App"
    );

            Directory.CreateDirectory(appDataPath);

            var dbPath = Path.Combine(appDataPath, "learning.db");

            services.AddSingleton(new SQLiteConnectionFactory($"Data Source={dbPath}"));

            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddScoped<IChapterRepository, ChapterRepository>();

            services.AddScoped<ILearningServices , LearningServices> ();

            services.AddSingleton<DatabaseInitializer>();

            services.AddSingleton<MainForm>();

           

            services.AddSingleton<WebViewMessageDispatcher>();

            return services.BuildServiceProvider();
        }

    }
}
