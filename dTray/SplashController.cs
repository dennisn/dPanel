using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace dPanel.dTray
{
    /// <summary>
    /// Provide simplify control of the splash screen, such as start up/shut down,
    /// and possibly progress status
    /// </summary>
    internal class SplashController
    {
        private static Thread _splashThread = null;
        private static SplashForm _splash = null;

        public static void StartSplash()
        {
            // Make sure it is only launched once.
            if (_splash != null)
                return;

            _splashThread = new Thread(new ThreadStart(ShowSplash));
            _splashThread.IsBackground = true;
            _splashThread.SetApartmentState(ApartmentState.STA);
            _splashThread.Start();
            
        }

        public static void EndSplash()
        {
            if (_splash != null && _splash.IsDisposed == false)
            {
                // TODO: debug why still invoke on disposed object
                // when handling "double-click" event
                _splash.Invoke(new System.Windows.Forms.MethodInvoker(DisposeSplash));

                _splashThread.Join();
                _splashThread = null;
            }
        }

        private static void ShowSplash()
        {
            _splash = new SplashForm();
            // This
            _splash.DoubleClick += delegate(object sender, EventArgs e) { EndSplash(); };
            System.Windows.Forms.Application.Run(_splash);
        }

        private static void DisposeSplash()
        {
            if (_splash != null)
            {
                lock (_splash)
                {
                    if (_splash.IsDisposed == false)
                        _splash.Dispose();
                }
            }
        }
    }
}
