#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PluginUpdater
{
    internal class MainClass
    {
        static System.Net.WebClient wc;
        static void Main(string[] args)
        {
            if(args.Length == 0) 
            {
                Console.WriteLine("args.Length is 0");
                Console.ReadLine();
                return;
            }
#if DEBUG
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(i.ToString()+":"+args[i]);
            }
            Console.ReadLine();
#endif
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            wc = new System.Net.WebClient();

            Download(args[0]);

            wc.Dispose();
            if(args.Length>1)
            {
                Process.Start(args[1]);
            }

        }
        static bool Download(string path)
        {
            try
            {
                wc.DownloadFile("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/ToukaitetudouPluginManager/obj/Release/ToukaitetudouPluginManager.dll", path);
            }
            catch (Exception ex)
            {
                return Download(path);
            }
            return true;
        }
    }
}
