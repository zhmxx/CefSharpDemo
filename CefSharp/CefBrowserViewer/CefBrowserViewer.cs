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

        private TextBox textBox1;
        private Button button1;
        public ChromiumWebBrowser browser;
        private const string defaultUrl = "https://www.google.com/";

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
                browser = new ChromiumWebBrowser(defaultUrl);
                this.Controls.Add(browser);
            }
            catch(Exception ex)
            {
            }
            
        }//Declare the event method to be called

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(545, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = defaultUrl;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(624, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CefBrowserViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(872, 645);
            this.ClientSize = new System.Drawing.Size(986, 792);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "CefBrowserViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Load(textBox1.Text);
        }
    }
}
