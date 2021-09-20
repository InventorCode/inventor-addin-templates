public static class Globals
{
    // Inventor application object.
    public static Inventor.Application invApp;

    // The unique ID for this add-in.  If this add-in is copied to create a new add-in
    // you need to update this ID along with the ID in the .manifest file, the .addin file
    // and create a new ID for the typelib GUID in AssemblyInfo.vb
    public const string g_simpleAddInClientID = "05D13420-B80E-4021-97B8-A3A8934A5731";

    public const string g_addInClientID = "{" + g_simpleAddInClientID + "}";
}