using Inventor;
using System;
using System.Runtime.InteropServices;

namespace addin00
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("c15f4e90-64fc-468e-8dcb-34f5cbcc4bfa")]
    public class StandardAddInServer : ApplicationAddInServer
    {
        // Inventor application object.
        private Application m_inventorApplication;


        public void Activate(ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            m_inventorApplication = addInSiteObject.Application;
        }

        public void Deactivate()
        {
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID) { }

        public object Automation { get => null; }
    }
}