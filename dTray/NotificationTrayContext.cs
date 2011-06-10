using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace dPanel.dTray
{
    internal class NotificationTrayContext : ApplicationContext
    {
        /// <summary>
        /// The application tray icon shown in system tray
        /// </summary>
        private NotifyIcon _trayIcon;

        /// <summary>
        /// The window's container that contain the tray icon.
        /// It was used to control the show/hide/dispose of the tray icon
        /// </summary>
        private Container _trayContainer;

        private dPanelAboutBox _aboutBox = new dPanelAboutBox();
        
        #region Starting up

        public NotificationTrayContext()
        {
            Thread splashThread = new Thread(ShowSplash);
            splashThread.Start();

            Initialize();
            
            // TODO: a better job of making a splash screen
            splashThread.Abort();
        }

        private void ShowSplash()
        {
            // show/hide splash screen
            SplashForm splashForm = new SplashForm();
            try
            {
                splashForm.ShowDialog();
            }
            finally
            {
                splashForm.Dispose();
            }
        }

        private void Initialize()
        {
            _trayContainer = new Container();

            _trayIcon = new NotifyIcon(_trayContainer)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon("Resources/TrayIcons.ico"),
                Text = "dPanel",
                Visible = true
            };

            _trayIcon.DoubleClick += new EventHandler(trayIcon_Click);
            _trayIcon.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        #endregion;

        #region Tray Icon event handling

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            PopulateTrayIconContextMenuStrip(_trayIcon.ContextMenuStrip);
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            _trayIcon.ContextMenuStrip.Visible = true;
        }

        private void showAboutBox_Click(object sender, EventArgs e)
        {
            _aboutBox.StartPosition = FormStartPosition.CenterScreen;
            _aboutBox.ShowDialog();
        }

        private void exitSystem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        #endregion;

        #region Shut down

        /// <summary>
        /// When the application context is disposed, dispose things like the notify icon.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _aboutBox.Dispose();

                if (_trayContainer != null)
                    _trayContainer.Dispose();
            }
        }

        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected override void ExitThreadCore()
        {
            // before we exit, running forms should clean themselves up.

            _trayIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

        #endregion;

        #region Context behavior

        private void PopulateTrayIconContextMenuStrip(ContextMenuStrip menuStrip)
        {
            if (menuStrip.Items.Count == 0)
            {
                menuStrip.Items.Add("&About", null, showAboutBox_Click);
                menuStrip.Items.Add(new ToolStripSeparator());

                List<ToolStripItem> items = GetPluginItems();
                menuStrip.Items.AddRange(items.ToArray());

                menuStrip.Items.Add(new ToolStripSeparator());
                menuStrip.Items.Add("&Exit", null, exitSystem_Click);
            }
        }

        private List<ToolStripItem> GetPluginItems()
        {
            List<ToolStripItem> result = new List<ToolStripItem>();

            // Each separated plugin will add additional ToolStripItems, 
            // which could have their own sub-items, or simple show their own form
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            
            // TODO: need a customised configuration section group, with each item represented a separated configs
            // See : ConfigurationElementCollection (http://msdn.microsoft.com/en-us/library/system.configuration.configurationelementcollection%28v=VS.90%29.aspx)

            //// test
            //ToolStripMenuItem test = new ToolStripMenuItem("&test dropdown");
            //test.DropDown = new ToolStripDropDown();
            //test.DropDown.Items.Add("Test 1");
            //test.DropDown.Items.Add("Test 2");
            //test.DropDown.Items.Add("Test 3");
            //result.Add(test);

            return result;
        }



        #endregion;

       
    }

    /// <summary>
    /// Utility to help with the managing of notify icon
    /// </summary>
    internal static class NotifyIconUtils
    {
    }
}
