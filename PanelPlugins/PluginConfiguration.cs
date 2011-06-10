using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace dPanel.PanelPlugins
{
    /// <summary>
    /// A configuration collections,
    /// represents a collections (dictionary?) of PluginConfigElement
    /// </summary>
    internal class PluginCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public PluginConfigElement this[int index]
        {
            get
            {
                return (PluginConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new PluginConfigElement this[string pluginClass]
        {
            get
            {
                return (PluginConfigElement)this.BaseGet(pluginClass);
            }
            set
            {
                if (this.BaseGet(pluginClass) != null)
                {
                    this.BaseRemoveAt(this.BaseIndexOf(this.BaseGet(pluginClass)));
                }
                this.BaseAdd(value);
            }
        }


        #region configuration methods

        public void Add(PluginConfigElement element)
        {
            this.BaseAdd(element);
        }

        public void Clear()
        {
            this.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PluginConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PluginConfigElement)element).PluginClass;
        }

        public void Remove(PluginConfigElement element)
        {
            BaseRemove(element.PluginClass);
        }

        public void Remove(string pluginClass)
        {
            BaseRemove(pluginClass);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }


        #endregion;
    }

    /// <summary>
    /// A configuration element, specifies the assembly and the plugin class in that assembly
    /// </summary>
    internal class PluginConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("assemblyPath", IsRequired=true)]
        public string AssemblyPath
        {
            get { return (string)this["assemblyPath"]; }
            set { this["assemblyPath"] = value; }
        }

        [ConfigurationProperty("pluginClass", IsRequired = true)]
        public string PluginClass
        {
            get { return (string)this["pluginClass"]; }
            set { this["pluginClass"] = value; }
        }

        public PluginConfigElement(string assemblyPath, string pluginClass)
        {
            this.AssemblyPath = assemblyPath;
            this.PluginClass = pluginClass;
        }

        public PluginConfigElement()
        {
            this.AssemblyPath = "";
            this.PluginClass = "";
        }
    }

    internal class PluginConfigSection: ConfigurationSection
    {
        [ConfigurationProperty("plugins")]
        [ConfigurationCollection(typeof(PluginConfigElement), AddItemName="addPluginConfig")]
        public PluginCollection Plugins
        {
            get { return (PluginCollection)base["plugins"]; }
        }

    }
}
