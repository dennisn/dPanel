using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace dPanel.dTray
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationContext notifyTrayContext = new NotificationTrayContext();
            Application.Run(notifyTrayContext);
        }
    }
}
