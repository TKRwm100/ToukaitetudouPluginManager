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
            if (args is null)
            {
                Console.WriteLine("null");
            }
            else
            {
                for (int i = 0; i< args.Length; i++)
                {
                    Console.WriteLine("{1}:"+args[i], i);
                }
            }
            Console.ReadLine();
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            wc = new System.Net.WebClient();

            Download();

            wc.Dispose();
            if(args.Length!=0)
            {
                Process.Start(args[0]);
            }

        }
        static bool Download()
        {
            try
            {
                wc.DownloadFile("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/ToukaitetudouPluginManager/obj/Debug/ToukaitetudouPluginManager.dll", @"C:\Users\Public\Documents\AtsEx\1.0\Extensions\ToukaitetudouPluginManager.dll"); //AppDomain.CurrentDomain.BaseDirectory+"ToukaitetudouPluginManager.dll");
            }
            catch (Exception ex)
            {
                return Download();
            }
            return true;
        }
    }
}
