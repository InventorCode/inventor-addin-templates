using Inventor;
using InventorCode.Plugin;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Forms;

namespace InvAddinPlugin
{
    /// <summary>
    /// This is a plugin that we'll create to work with the IPlugin Demo.  This is effectively
    /// a mini-addin that knows nothing about the "host-addin" that will be loading it.
    /// All button creation, command creation, etc relevant to this mini-addin will be contained
    /// here.
    /// </summary>

    //This attribute is required!  It is what PluginHost uses to find this plugin.
    [Export(typeof(IPlugin))]
    public class Main : IPlugin
    {
        // Implement the IPlugin Interface

        private Inventor.Application _inventorApplication;
        private string _clientId;

        #region IPlugin Members

        public void Activate(Inventor.Application InventorApplication, string ClientId, bool firstTime = true)
        {
            _inventorApplication = InventorApplication;
            _clientId = ClientId;
            MessageBox.Show("Empty Plugin Demo Loaded.");
        }

        public void Deactivate()
        {
            _inventorApplication = null;
        }

        public void Execute()
        {
        }

        public CommandControl ExecuteSettings { get; set; }

        public string Name { get => Assembly.GetExecutingAssembly().GetName().Name; }

        public string Version { get => Assembly.GetExecutingAssembly().GetName().Version.ToString(); }

        #endregion IPlugin Members
    }
}