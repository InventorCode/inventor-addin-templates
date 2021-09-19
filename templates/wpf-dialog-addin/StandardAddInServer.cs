using Inventor;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InvAddin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [ProgId("InvAddin.StandardAddinServer")]
    [GuidAttribute(Globals.g_simpleAddInClientID)]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        // *********************************************************************************
        // * The two declarations below are related to adding buttons to Inventor's UI.
        // * They can be deleted if this add-in doesn't have a UI and only runs in the
        // * background handling events.
        // *********************************************************************************

        // Declaration of the object for the UserInterfaceEvents to be able to handle
        // if the user resets the ribbon so the button can be added back in.
        private UserInterfaceEvents _m_uiEvents;

        public UserInterfaceEvents m_uiEvents
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _m_uiEvents;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_m_uiEvents != null)
                {
                    _m_uiEvents.OnResetRibbonInterface -= m_uiEvents_OnResetRibbonInterface;
                }

                _m_uiEvents = value;
                if (_m_uiEvents != null)
                {
                    _m_uiEvents.OnResetRibbonInterface += m_uiEvents_OnResetRibbonInterface;
                }
            }
        }

        // Declaration of the button definition with events to handle the click event.
        // For additional commands this declaration along with other sections of code
        // that apply to the button can be duplicated from this example.
        public class UI_Button
        {
            private ButtonDefinition _bd;

            public ButtonDefinition bd
            {
                [MethodImpl(MethodImplOptions.Synchronized)]
                get
                {
                    return this._bd;
                }

                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if (this._bd != null)
                    {
                        this._bd.OnExecute -= bd_OnExecute;
                    }

                    this._bd = value;
                    if (this._bd != null)
                    {
                        this._bd.OnExecute += bd_OnExecute;
                    }
                }
            }

            private void bd_OnExecute(NameValueMap Context)
            {
                // Link button clicks to their respective commands.
                switch (bd.InternalName)
                {
                    case "dw_NewWithPathFromPart":
                        MessageBox.Show(Globals.invApp.ActiveDocument.DisplayName);
                        return;

                    default:
                        return;
                }
            }
        }

        public delegate ButtonDefinition CreateButton(string display_text, string internal_name, string icon_path);

        public ButtonDefinition button_template(string display_text, string internal_name, string icon_path)
        {
            UI_Button MyButton = new UI_Button();
            MyButton.bd = AddinUtilities.CreateButtonDefinition(display_text, internal_name, "", icon_path);
            return MyButton.bd;
        }

        // Declare all buttons here
        private ButtonDefinition Button01;

        // This method is called by Inventor when it loads the AddIn. The AddInSiteObject provides access
        // to the Inventor Application object. The FirstTime flag indicates if the AddIn is loaded for
        // the first time. However, with the introduction of the ribbon this argument is always true.
        public void Activate(ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            try
            {
                // Initialize AddIn members.
                Globals.invApp = addInSiteObject.Application;

                // Connect to the user-interface events to handle a ribbon reset.
                m_uiEvents = Globals.invApp.UserInterfaceManager.UserInterfaceEvents;

                // *********************************************************************************
                // * The remaining code in this Sub is all for adding the add-in into Inventor's UI.
                // * It can be deleted if this add-in doesn't have a UI and only runs in the
                // * background handling events.
                // *********************************************************************************

                // ButtonName = create_button(display_text, internal_name, icon_path)
                CreateButton create_button = new CreateButton(button_template);

                Button01 = create_button("This is Display Text", "internal_name", @"Resources\Buttons\SampleIcon");

                // Add to the user interface, if it's the first time.
                // If this add-in doesn't have a UI but runs in the background listening
                // to events, you can delete this.
                if (firstTime)
                {
                    AddToUserInterface();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected failure in the activation of the add-in... " + System.Environment.NewLine + System.Environment.NewLine + ex.Message);
            }
        }

        // This method is called by Inventor when the AddIn is unloaded. The AddIn will be
        // unloaded either manually by the user or when the Inventor session is terminated.
        public void Deactivate()
        {
            // Release objects.
            Button01 = null;

            m_uiEvents = null;
            Globals.invApp = null;

            //MessageBox.Show("Addin unloaded");

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // This property is provided to allow the AddIn to expose an API of its own to other
        // programs. Typically, this  would be done by implementing the AddIn's API
        // interface in a class and returning that class object through this property.
        // Typically it's not used, like in this case, and returns Nothing.
        public object Automation
        {
            get
            {
                return null;
            }
        }

        // Note:this method is now obsolete, you should use the
        // ControlDefinition functionality for implementing commands.
        public void ExecuteCommand(int commandID)
        {
        }

        // Adds whatever is needed by this add-in to the user-interface.  This is
        // called when the add-in loaded and also if the user interface is reset.
        private void AddToUserInterface()
        {
            #region Ribbon, Tabs, Panel setup

            // Get the ribbon. (more buttons can be added to various ribbons within this single addin)
            // Ribbons:
            // ZeroDoc
            // Part
            // Assembly
            // Drawing
            // Presentation
            // iFeatures
            // UnknownDocument
            //Ribbon asmRibbon = Globals.invApp.UserInterfaceManager.Ribbons["Assembly"];
            Ribbon partRibbon = Globals.invApp.UserInterfaceManager.Ribbons["Part"];

            // Set up Tabs.
            // tab = setup_panel(display_name, internal_name, inv_ribbon)
            RibbonTab MyTab_part;
            MyTab_part = setup_tab("Tools", "id_TabTools", partRibbon);

            // Set up Panels.
            // panel = setup_panel(display_name, internal_name, ribbon_tab)

            RibbonPanel MyPanel_part;
            MyPanel_part = setup_panel("Sample AddIn", "id_TabTools", MyTab_part);

            #endregion Ribbon, Tabs, Panel setup

            // Part panel buttons
            if (!(Button01 == null))
            {
                MyPanel_part.CommandControls.AddButton(Button01, false);
            }
        }

        private RibbonTab setup_tab(string display_name, string internal_name, Ribbon inv_ribbon)
        {
            RibbonTab setup_tabRet = default(RibbonTab);
            RibbonTab ribbon_tab = null;
            try
            {
                ribbon_tab = inv_ribbon.RibbonTabs[internal_name];
            }
            catch (Exception)
            {
                //MessageBox.Show("Couldn't set up tab: " + ex.Message);
            }

            if (ribbon_tab == null)
            {
                ribbon_tab = inv_ribbon.RibbonTabs.Add(display_name, internal_name, Globals.g_addInClientID);
            }

            setup_tabRet = ribbon_tab;
            return setup_tabRet;
        }

        private RibbonPanel setup_panel(string display_name, string internal_name, RibbonTab ribbon_tab)
        {
            RibbonPanel setup_panelRet = default(RibbonPanel);
            RibbonPanel ribbon_panel = null;
            try
            {
                ribbon_panel = ribbon_tab.RibbonPanels[internal_name];
            }
            catch (Exception)
            {
                //MessageBox.Show("Couldn't set up panel: " + ex.Message);
            }

            if (ribbon_panel == null)
            {
                ribbon_panel = ribbon_tab.RibbonPanels.Add(display_name, internal_name, Globals.g_addInClientID);
            }

            setup_panelRet = ribbon_panel;
            return setup_panelRet;
        }

        private void m_uiEvents_OnResetRibbonInterface(NameValueMap Context)
        {
            // The ribbon was reset, so add back the add-ins user-interface.
            AddToUserInterface();
        }
    }
}