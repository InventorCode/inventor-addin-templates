using Inventor;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using InventorCode.Plugin;
using System.Collections.Generic;


namespace InvAddin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [ProgId("InvAddin.StandardAddinServer")]
    [GuidAttribute("B76D3C5A-C5BD-4560-A532-5E047C84E596")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;

        // Create an instance of the PluginHost class in the package.  This contains
        // the code to wire up the plugins dynamically.
        private PluginHost pluginHost = new PluginHost();

        public StandardAddInServer()
        {
            // Here we will actually wire up the plugins by collecting classes with advertised
            // IPlugin interfaces into our PluginHost object.  If you need granular control, you
            // can manually compose these using MEF. See the IPluginHost class to get you started.
            pluginHost.ComposePlugins();
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // Here we'll activate the individual plugins...
            pluginHost.ActivateAll(m_inventorApplication, "B76D3C5A-C5BD-4560-A532-5E047C84E596");
        }

        public void Deactivate()
        {
            // And here we'll deactivate the individual plugins...
            pluginHost.DeactivateAll();

            // Release objects.
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID) { }
        public object Automation { get => null; }

        #endregion

    }
}
