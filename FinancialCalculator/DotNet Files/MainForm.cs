using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanCalculator
{
   public class MainForm:Form
    {
        private WebView2 webView;
        private const string InternalHost = "app";
        public MainForm()
        {
            Text = "Financial Calculator";
            Width = 1100;
            Height = 800;
            StartPosition = FormStartPosition.CenterScreen;
            string exeDirIcon = AppDomain.CurrentDomain.BaseDirectory;
            Icon = new Icon(Path.Combine(exeDirIcon, "app.ico"));
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(webView);

            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Financial Calculator",
                "WebView2"
            );

            Directory.CreateDirectory(userDataFolder);

            var env = await CoreWebView2Environment.CreateAsync(
                null,
                userDataFolder
            );

            await webView.EnsureCoreWebView2Async(env);

            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            webView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;

            webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "app",
                exeDir,
                CoreWebView2HostResourceAccessKind.Allow
            );

            webView.Source = new Uri("https://app/index.html");
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            OpenExternalBrowser(e.Uri);
        }

        private void CoreWebView2_NavigationStarting(
                object sender,
                     CoreWebView2NavigationStartingEventArgs e)
        {
            if (IsExternalUrl(e.Uri))
            {
                e.Cancel = true;
                OpenExternalBrowser(e.Uri);
            }
        }


        private bool IsExternalUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return false;

            // כל מה שלא שייך לדומיין הווירטואלי שלנו – חיצוני
            return uri.Host != InternalHost;
        }

        private void OpenExternalBrowser(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
