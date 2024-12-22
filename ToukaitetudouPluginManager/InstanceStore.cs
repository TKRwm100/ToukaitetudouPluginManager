using BveEx.PluginHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toukaitetudou.BveEx.ToukaitetudouPluginManager
{
    public class InstanceStore
    {
        public static IToukaitetudouPluginsManager ManagerInstance;
        public static IBveHacker BveHacker;
        public static Dictionary<string, IToukaitetudouPlugin> pluginsInstance=new Dictionary<string, IToukaitetudouPlugin>();
    }
}
