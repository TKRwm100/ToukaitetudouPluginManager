using AtsEx.PluginHost.Plugins.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toukaitetudou.AtsEx.ToukaitetudouPluginManager
{
    public interface IToukaitetudouPlugin
    {

    }
    public interface IToukaitetudouPluginsManager : IExtension
    {
        void AddPlugin(string id, IToukaitetudouPlugin instance);
        void AddPage(string id, string pagename, Panel page);

        void RemovePlugin(string id);
    }
}
