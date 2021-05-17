using Inventor;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace dockable_window
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("865b427e-2bd2-4322-978d-45d873e32c6c")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        private string ClientId = "{865b427e-2bd2-4322-978d-45d873e32c6c}";
        private Application m_inventorApplication;

        private DockableWindow dockableWindow;

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            //Create dock able window
            dockableWindow = m_inventorApplication.UserInterfaceManager.DockableWindows.Add(ClientId,
                "docable_window.StandardAddInServer.dockableWindow", "Example");
            dockableWindow.ShowVisibilityCheckBox = true;

        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion

    }
}
