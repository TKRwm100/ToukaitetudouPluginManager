using AtsEx.Extensions.ContextMenuHacker;
using AtsEx.PluginHost;
using AtsEx.PluginHost.Plugins;
using AtsEx.PluginHost.Plugins.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Toukaitetudou.AtsEx.ToukaitetudouPluginManager
{
    [Plugin(PluginType.Extension)]
    [ExtensionMainDisplayType(typeof(IToukaitetudouPluginsManager))]
    internal class PluginMain : AssemblyPluginBase,IToukaitetudouPluginsManager
    {
        private static readonly string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
        public static readonly string BaseDirectory = Path.GetDirectoryName(AssemblyLocation);

        public PluginsForm Form;
        private ToolStripMenuItem MenuItem;
        private Dictionary<string,TabPage> tabsMng = new Dictionary<string,TabPage>();
        int index = 0;
        public static readonly float ver = 1.0f;
        public PluginMain(PluginBuilder builder) : base(builder)
        {
            
            InstanceStore.ManagerInstance=this;
            InstanceStore.Native=Native;
            InstanceStore.BveHacker=BveHacker;
            BveHacker.ScenarioCreated += OnScenarioCreated;
            
            //Config.Structures[0].ToString();
            Extensions.AllExtensionsLoaded+=ExtentionInit;
        }

        private void ExtentionInit(object sender, EventArgs e)
        {
            MenuItem = Extensions.GetExtension<IContextMenuHacker>().AddCheckableMenuItem("管理ウィンドウを表示", MenuItemCheckedChanged, ContextMenuItemType.Plugins);

            MenuItem.Checked = false;

            Form = new PluginsForm();
            Form.FormClosing += FormClosing;
            Form.WindowState = FormWindowState.Normal;
            AddPage(this.Identifier, "TPM", Form.panel1);
            MenuItem.Checked = true;
            BveHacker.MainFormSource.Focus(); 
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            System.Net.WebClient wc = new System.Net.WebClient();
            string filepath = Directory.GetParent(Location)+"\\Versions.xml";
            wc.DownloadFile("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/Versions.xml", filepath);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/Versions.xml");
            int i = 0;
            foreach (XmlElement xml in xmlDocument.GetElementsByTagName("current"))
            {
                if (float.Parse(xml.GetAttribute("ver"))>ver)
                {
                    filepath = Directory.GetParent(Location)+"\\PluginUpdater.exe";
                    wc.DownloadFile("https://github.com/TKRwm100/ToukaitetudouPluginManager/raw/main/PluginUpdater/bin/Debug/PluginUpdater.exe", filepath);
                    Process.Start(filepath,App.Instance.BveAssembly.Location);
                    App.Instance.Process.CloseMainWindow();
                }
            }
            
            
            wc.Dispose();
        }

        public override void Dispose()
        {
            BveHacker.ScenarioCreated -= OnScenarioCreated;
        }

        private void OnScenarioCreated(ScenarioCreatedEventArgs e)
        {
            
            return;
        }

        public override TickResult Tick(TimeSpan elapsed)
        {
            ExtensionTickResult extensionTickResult = new ExtensionTickResult();

            return extensionTickResult;
        }
            private void MenuItemCheckedChanged(object sender, EventArgs e)
        {
            if (MenuItem.Checked)
            {
                Form.Show(BveHacker.MainFormSource);
            }
            else
            {
                Form.Hide();
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            MenuItem.Checked = false;
        }


        public void AddPlugin(string id, IToukaitetudouPlugin instance)
        {
            InstanceStore.pluginsInstance.Add(id, instance);
        }


        public void AddPage(string id, string pagename, Panel page)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name=pagename;
            tabPage.Controls.Add(page);;
            tabPage.Name=pagename;;
            tabPage.Text=tabPage.Name;
            Form.tabControl1.Controls.Add(tabPage);
            tabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            tabsMng.Add(id,tabPage);
        }

        public void RemovePlugin(string id)
        {
            if (InstanceStore.pluginsInstance.Remove(id))
            {
                Form.tabControl1.Controls.Remove(tabsMng[id]);
                tabsMng.Remove(id);
            }

            return;

        }
    }
}
