using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dPanel.PanelPlugins
{
#if DEBUG
    public class TestPlugin: IPanelPlugin
    {
        public string Name
        {
            get { return "Test Plugin"; }
        }

        public string ShortName
        {
            get { return "Test"; }
        }

        public string Description
        {
            get { return "A test plugin"; }
        }

        public System.Drawing.Image Icon
        {
            get { return null; }
        }
        
        public Dictionary<string, EventHandler> GetMenuItems()
        {
            Dictionary<string, EventHandler> result = new Dictionary<string, EventHandler>();
            result.Add("Test &1", DebugHandler);
            result.Add("Test &2", DebugHandler);
            result.Add("Test &3", DebugHandler);
            result.Add("Test &4", DebugHandler);

            return result;
        }

        public void DebugHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Receive event " + e + " from sender: " + sender);
        }
    }
#endif
}
