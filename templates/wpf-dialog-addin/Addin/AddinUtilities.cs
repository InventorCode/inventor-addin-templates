using Inventor;
using System;
using System.Windows.Forms;

namespace InvAddin.Addin
{
    public static class AddinUtilities
    {
        /// <summary>
        /// Function to simplify the creation of a button definition.  The big advantage
        /// to using this function is that you don't have to deal with converting images
        /// but instead just reference a folder on disk where this routine reads the images.
        /// </summary>
        /// <param name="DisplayName">
        /// The name of the command as it will be displayed on the button.
        /// </param>
        /// <param name="InternalName">
        /// The internal name of the command. This needs to be unique with respect to ALL other
        /// commands. It's best to incorporate a  to help with uniqueness.
        /// </param>
        /// <param name="ToolTip">
        /// The tooltip that will be used for the command.
        ///
        /// This is optional and the display name will be used as the
        /// tooltip if no tooltip is specified. Like in the DisplayName argument, you can use
        /// returns to force line breaks.
        /// </param>
        /// <param name="IconFolder">
        /// The folder that contains the icon files. This can be a full path or a path that is
        /// relative to the location of the add-in dll. The folder should contain the files
        /// 16x16.png and 32x32.png. Each command will have its own folder so they can have
        /// their own icons.
        ///
        /// This is optional and if no icon is specified then no icon will be displayed on the
        /// button and it will be only text.
        /// </param>
        /// <returns>
        /// Returns the newly created button definition or Nothing in case of failure.
        /// </returns>
        public static Inventor.ButtonDefinition CreateButtonDefinition(string DisplayName,
                                                                       string InternalName,
                                                                       string ToolTip = "",
                                                                       string IconFolder = "")
        {
            // Check to see if a command already exists is the specified internal name.
            Inventor.ButtonDefinition testDef = null;
            try
            {
                testDef = (Inventor.ButtonDefinition)Globals.invApp.CommandManager.ControlDefinitions[InternalName];
            }
            catch
            {
                // This needs to fail silently.
            }

            if (!(testDef == null))
            {
                MessageBox.Show("Error when loading the add-in \"INVENTOR_DrawingFiller\". A command already exists with the same internal name. Each add-in must have a unique internal name. Change the internal name in the call to CreateButtonDefinition.", "Title Block Filler Inventor Add-In");
                return null;
            }

            // Check to see if the provided folder is a full or relative path.
            if (!string.IsNullOrEmpty(IconFolder))
            {
                if (!System.IO.Directory.Exists(IconFolder))
                {
                    // The folder provided doesn't exist, so assume it is a relative path and
                    // build up the full path.
                    string dllPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    IconFolder = System.IO.Path.Combine(dllPath, IconFolder);
                }
            }

            // Get the images from the specified icon folder.
            stdole.IPictureDisp iPicDisp16x16 = null;
            stdole.IPictureDisp iPicDisp32x32 = null;
            if (!string.IsNullOrEmpty(IconFolder))
            {
                if (System.IO.Directory.Exists(IconFolder))
                {
                    string filename16x16 = System.IO.Path.Combine(IconFolder, "16x16.png");
                    string filename32x32 = System.IO.Path.Combine(IconFolder, "32x32.png");

                    if (System.IO.File.Exists(filename16x16))
                    {
                        try
                        {
                            System.Drawing.Bitmap image16x16 = new System.Drawing.Bitmap(filename16x16);
                            iPicDisp16x16 = ConvertImage.ConvertImageToIPictureDisp(image16x16);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to load the 16x16.png image from \"" + IconFolder + "\"." + System.Environment.NewLine + "No small icon will be used.", "Error Loading Icon" + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("The icon for the small button does not exist: \"" + filename16x16 + "\"." + System.Environment.NewLine + "No small icon will be used.", "Error Loading Icon");

                    if (System.IO.File.Exists(filename32x32))
                    {
                        try
                        {
                            System.Drawing.Bitmap image32x32 = new System.Drawing.Bitmap(filename32x32);
                            iPicDisp32x32 = ConvertImage.ConvertImageToIPictureDisp(image32x32);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to load the 32x32.png image from \"" + IconFolder + "\"." + System.Environment.NewLine + "No large icon will be used.", "Error Loading Icon" + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("The icon for the large button does not exist: \"" + filename32x32 + "\"." + System.Environment.NewLine + "No large icon will be used.", "Error Loading Icon");
                }
            }

            try
            {
                // Get the ControlDefinitions collection.
                ControlDefinitions controlDefs = Globals.invApp.CommandManager.ControlDefinitions;

                // Create the command defintion.
                ButtonDefinition btnDef = controlDefs.AddButtonDefinition(DisplayName, InternalName, Inventor.CommandTypesEnum.kShapeEditCmdType, Globals.g_addInClientID, "", ToolTip, iPicDisp16x16, iPicDisp32x32);

                return btnDef;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't set up btnDef: " + ex.Message);
                return null;
            }
        }

        public static Inventor.ComboBoxDefinition CreateComboBoxDefinition(string DisplayName,
                                                                           string InternalName,
                                                                           int comboWidth = 100,
                                                                           string ToolTip = "",
                                                                           string IconFolder = "")
        {
            // Check to see if a command already exists is the specified internal name.
            Inventor.ButtonDefinition testDef = null;
            try
            {
                testDef = (Inventor.ButtonDefinition)Globals.invApp.CommandManager.ControlDefinitions[InternalName];
            }
            catch
            {
                // This needs to fail silently.
            }

            if (!(testDef == null))
            {
                MessageBox.Show("Error when loading the add-in \"INVENTOR_DrawingFiller\". A command already exists with the same internal name. Each add-in must have a unique internal name. Change the internal name in the call to CreateButtonDefinition.", "Title Block Filler Inventor Add-In");
                return null;
            }

            // Check to see if the provided folder is a full or relative path.
            if (!string.IsNullOrEmpty(IconFolder))
            {
                if (!System.IO.Directory.Exists(IconFolder))
                {
                    // The folder provided doesn't exist, so assume it is a relative path and
                    // build up the full path.
                    string dllPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    IconFolder = System.IO.Path.Combine(dllPath, IconFolder);
                }
            }

            // Get the images from the specified icon folder.
            stdole.IPictureDisp iPicDisp16x16 = null;
            stdole.IPictureDisp iPicDisp32x32 = null;
            if (!string.IsNullOrEmpty(IconFolder))
            {
                if (System.IO.Directory.Exists(IconFolder))
                {
                    string filename16x16 = System.IO.Path.Combine(IconFolder, "16x16.png");
                    string filename32x32 = System.IO.Path.Combine(IconFolder, "32x32.png");

                    if (System.IO.File.Exists(filename16x16))
                    {
                        try
                        {
                            System.Drawing.Bitmap image16x16 = new System.Drawing.Bitmap(filename16x16);
                            iPicDisp16x16 = ConvertImage.ConvertImageToIPictureDisp(image16x16);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to load the 16x16.png image from \"" + IconFolder + "\"." + System.Environment.NewLine + "No small icon will be used.", "Error Loading Icon" + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("The icon for the small button does not exist: \"" + filename16x16 + "\"." + System.Environment.NewLine + "No small icon will be used.", "Error Loading Icon");

                    if (System.IO.File.Exists(filename32x32))
                    {
                        try
                        {
                            System.Drawing.Bitmap image32x32 = new System.Drawing.Bitmap(filename32x32);
                            iPicDisp32x32 = ConvertImage.ConvertImageToIPictureDisp(image32x32);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to load the 32x32.png image from \"" + IconFolder + "\"." + System.Environment.NewLine + "No large icon will be used.", "Error Loading Icon" + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("The icon for the large button does not exist: \"" + filename32x32 + "\"." + System.Environment.NewLine + "No large icon will be used.", "Error Loading Icon");
                }
            }

            try
            {
                // Get the ControlDefinitions collection.
                ControlDefinitions controlDefs = Globals.invApp.CommandManager.ControlDefinitions;

                // Create the command defintion.
                ComboBoxDefinition comboDef = controlDefs.AddComboBoxDefinition(DisplayName,
                                                                                InternalName,
                                                                                Inventor.CommandTypesEnum.kShapeEditCmdType,
                                                                                comboWidth,
                                                                                Globals.g_addInClientID,
                                                                                "",
                                                                                ToolTip,
                                                                                iPicDisp16x16,
                                                                                iPicDisp32x32);

                return comboDef;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't set up combodef: " + ex.Message);
                return null;
            }
        }

        // This class is used to wrap a Win32 hWnd as a .Net IWind32Window class.
        // This is primarily used for parenting a dialog to the Inventor window.
        // This provides the expected behavior when the Inventor window is collapsed
        // and activated.
        //
        // For example:
        // myForm.Show(New WindowWrapper(invApp.MainFrameHWND))
        //
        public class WindowWrapper : System.Windows.Forms.IWin32Window
        {
            public WindowWrapper(IntPtr handle)
            {
                _hwnd = handle;
            }

            public IntPtr Handle
            {
                get
                {
                    return _hwnd;
                }
            }

            private IntPtr _hwnd;
        }

        // Class used to convert bitmaps and icons between their .Net native types
        // and an IPictureDisp object which is what the Inventor API requires.

        [System.Security.Permissions.PermissionSet
        (System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public class ConvertImage : System.Windows.Forms.AxHost
        {
            public ConvertImage()
                : base("59EE46BA-677D-4d20-BF10-8D8067CB8B32")
            {
            }

            public static stdole.IPictureDisp ConvertImageToIPictureDisp(System.Drawing.Image Image)
            {
                try
                {
                    return (stdole.IPictureDisp)GetIPictureFromPicture(Image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't ConvertImageToIPictureDisp: " + ex.Message);
                    return null;
                }
            }

            public static System.Drawing.Image ConvertIPictureDispToImage(stdole.IPictureDisp IPict)
            {
                try
                {
                    return GetPictureFromIPictureDisp(IPict);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't set up ConvertIPictureDispToImage: " + ex.Message);
                    return null;
                }
            }
        }
    }
}