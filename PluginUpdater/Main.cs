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
            string path = "";
            if (args is null)
            {
                //Console.WriteLine("null");
            }
            else
            {
                //Console.WriteLine(args.Length.ToString());
                for (int i = 0; i< args.Length; i++)
                {
                    //Console.WriteLine(i.ToString()+":"+args[i]);
                    path += args[i];
                }
            }
            //Console.WriteLine(path);
            //Console.ReadLine();
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            wc = new System.Net.WebClient();

            Download();

            wc.Dispose();
            if(path!="")
            {
                Process.Start(path);
            }

        }
        static bool Download()
        {
            try
            {
                wc.DownloadFile("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/ToukaitetudouPluginManager/obj/Release/ToukaitetudouPluginManager.dll", @"C:\Users\Public\Documents\AtsEx\1.0\Extensions\ToukaitetudouPluginManager.dll"); //AppDomain.CurrentDomain.BaseDirectory+"ToukaitetudouPluginManager.dll");
            }
            catch (Exception ex)
            {
                return Download();
            }
            return true;
        }
    }
}
