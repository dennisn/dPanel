using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace dPanel.PanelPlugins
{
    /// <summary>
    /// For each plugins that want to be integrage with the dPanel, 
    /// it has to contains at least one class implement this interface.
    /// This interface will serve as the main entry point/communication API
    /// between dPanel and its containing plugins
    /// </summary>
    public interface IPanelPlugin
    {
        string Name { get; }
        string ShortName { get; }
        string Description { get; }
        Image Icon { get; }

        /// <summary>
        /// The list of publish menu item and its associated event handler
        /// 
        /// * If there is only 1 item in the list, it will be added directly 
        /// to the main ToolStringItemsCollection
        /// * If there are more than 1 items, a new ToolStripMenuItem will 
        /// be added using this plugin ShortName and Icon. The published menu
        /// items are then added to this menu item
        /// </summary>
        /// <returns></returns>
        Dictionary<string, EventHandler> GetMenuItems();
    }
}
