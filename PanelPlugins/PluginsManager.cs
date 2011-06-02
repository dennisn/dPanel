using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dPanel.PanelPlugins
{
    /// <summary>
    /// The PluginsManager discover and manage access to plugins.
    /// There are two ways for plugins to be discovered/added
    /// * Loading all available assemblies and search for classes satisfied the plugin interface
    /// * Use configuration to specify which assembly, and possibly which class
    /// 
    /// I choose to use the second approached for efficiency. I don't think searching
    /// through all assembly is a good/suitable approach, because of its "underterministic" nature
    /// </summary>
    public class PluginsManager
    {

    }
}
