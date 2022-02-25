using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.AtlasAuthenticationProvider.Configuration
{
    /// <summary>
    /// Plugin configuration.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
        /// </summary>
        public PluginConfiguration()
        {
            ConnectionURI = string.Empty;
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionURI { get; set; }
    }
}
