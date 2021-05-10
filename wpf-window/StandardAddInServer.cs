using Inventor;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace WindowSample
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("91ba0b06-1c68-4f6d-bc31-34bd69d4c3e3")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;
        private Inventor.ButtonDefinition testButtonDefinition;

        //public StandardAddInServer()
        //{
        //}

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.


            //  create a simple button to trigger our wpf window...
            var commandManager = m_inventorApplication.CommandManager;
            testButtonDefinition = commandManager.ControlDefinitions.AddButtonDefinition(
                "ShowWpfDialog", "ShowWpfDialog", CommandTypesEnum.kQueryOnlyCmdType);

            // define a button handler...
            testButtonDefinition.OnExecute += testButtonDefinition_OnExecute;
            testButtonDefinition.AutoAddToGUI();
        }

        void testButtonDefinition_OnExecute(NameValueMap Context)
        {
            var mainWindow = new Views.MainWindow();

            //could be a good idea to set the owner for this window, esp. if it's used in a modeless way...
            var helper = new WindowInteropHelper(mainWindow);
            helper.Owner = new IntPtr(m_inventorApplication.MainFrameHWND);

            mainWindow.ShowDialog();
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;

            testButtonDefinition.OnExecute -= testButtonDefinition_OnExecute;
            testButtonDefinition = null;

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

            get => null;
        }
        #endregion

    }
}
