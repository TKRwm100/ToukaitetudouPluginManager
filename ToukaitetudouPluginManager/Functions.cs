using AtsEx.PluginHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toukaitetudou.AtsEx.ToukaitetudouPluginManager
{
    public class Functions
    {
        static public BveTypes.ClassWrappers.Section GetSection(double location)
        {
            double loc = 0;
            BveTypes.ClassWrappers.Section lastSection = null;
            foreach (BveTypes.ClassWrappers.Section section in InstanceStore.BveHacker.Scenario.SectionManager.Sections)
            {
                if (loc<location&&location<= section.Location)
                {
                    return lastSection;
                }
                loc=section.Location;
                lastSection=section;
            }
            return null;
        }
    }
}
