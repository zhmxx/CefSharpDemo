using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Diagnostics;

namespace Test.Application.Dialogs
{
    public partial class CefBrowserViewer : Form
    {
        public CefBrowserViewer()
        {
            InitializeComponent();
        }

        public ChromiumWebBrowser browser;

        public void InitBrowser()
        {
            try
            {
                if (!Cef.IsInitialized)
                {
                    string applicationDirectory = System.IO.Directory.GetCurrentDirectory();
                    CefSettings settings = new CefSettings
                    {
                        //BrowserSubprocessPath = applicationDirectory + "CefSharp.BrowserSubprocess.exe",
                        //ResourcesDirPath = applicationDirectory,
                        PackLoadingDisabled = false
                    };
                    settings.CefCommandLineArgs.Add("disable-features", "CrossSiteDocumentBlockingAlways,CrossSiteDocumentBlockingIfIsolating");
                    settings.CefCommandLineArgs.Add("disable-gpu", "1"); // Disable GPU acceleration
                    settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1"); //Disable GPU vsync
                    settings.RemoteDebuggingPort = 8088;
                    CefSharpSettings.ShutdownOnExit = false;
                    //Perform dependency check is to make sure all relevant resources are in our output directory.
                    Cef.Initialize(settings, false, null);
                }
                browser = new ChromiumWebBrowser("https://www.google.com/");
                this.Controls.Add(browser);
            }
            catch(Exception ex)
            {
            }
            
        }//Declare the event method to be called
       
    }
}
