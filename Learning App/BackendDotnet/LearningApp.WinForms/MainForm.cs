using LearningApp.Application;
using LearningApp.Application.Services;
using LearningApp.Data.DB;
using LearningApp.WinForms.RequestServices;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LearningApp.WinForms.RequestServices.WebViewMessage;
namespace LearningApp.WinForms
{
    public class MainForm:Form
    {
        private ILearningServices _learningServices;
        private WebView2 _webView2;
        private WebViewMessageDispatcher _dispatcher;

        private readonly WebViewMessageDispatcher _messageDispatcher;
        public MainForm(ILearningServices learningServices,WebViewMessageDispatcher messageDispatcher,WebViewMessageDispatcher dispatcher)
        {
            _learningServices = learningServices;
            this._dispatcher = dispatcher;

            Text = "Learning App";
            Width = 1200;
            Height = 800;
            string exeDirIcon = AppDomain.CurrentDomain.BaseDirectory;
            this.Icon = new System.Drawing.Icon(Path.Combine(exeDirIcon, "iconapp.ico"));

            InitializeWebView();

        }

      

        private async void InitializeWebView()
        {
            _webView2 = new WebView2
            {
                Dock = DockStyle.Fill,
            };

            Controls.Add( _webView2 );

//            var userDataFolder = Path.Combine(
//    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
//    "Learning App",
//    "WebView2"
//);

//            Directory.CreateDirectory(userDataFolder);

//            var environment = await CoreWebView2Environment.CreateAsync(
//                null,
//                userDataFolder
//            );

            //await _webView2.EnsureCoreWebView2Async(environment);
            await _webView2.EnsureCoreWebView2Async();

            _webView2.CoreWebView2.WebMessageReceived += OnWebMessageReceived;

            //var folder = Path.Combine(System.Windows.Forms.Application.StartupPath, "wwwroot");

            //_webView2.CoreWebView2.SetVirtualHostNameToFolderMapping(
            //    "app",
            //    folder,
            //    CoreWebView2HostResourceAccessKind.Allow
            //);

            //_webView2.CoreWebView2.Navigate("https://app/index.html");






            _webView2.CoreWebView2.Navigate("http://localhost:5173/");
            //_webView2.CoreWebView2.Navigate("http://127.0.0.1:5500/");
        }

        private async void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var request = JsonSerializer.Deserialize<WebViewRequest>(e.WebMessageAsJson);

            var response = await _dispatcher.DispatchAsync(request);

            var json = JsonSerializer.Serialize(response);


            


            _webView2.CoreWebView2.PostWebMessageAsJson(json);
        }
    }
}
