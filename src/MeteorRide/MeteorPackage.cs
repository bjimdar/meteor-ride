using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Vectria.MeteorRide.Project;
using Vectria.MeteorRide.Settings;

namespace Vectria.MeteorRide
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
    /// 
    /// <para>A Visual Studio component can be registered under different registry roots; for instance
    /// when you debug your package you want to register it in the experimental hive. This
    /// attribute specifies the registry root to use if no one is provided to regpkg.exe with
    /// the /root switch.</para>
	///
	/// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) 
    /// that this class is a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	///
    /// This attribute is used to register the information needed to show this package
    /// in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	///
    /// Notify loader that this package exposes some menus.
    //[ProvideMenuResource("Menus.ctmenu", 1)]
    ///
    /// Define the default registry root for  registering the package. 
    /// We are currently using the experimental hive.
    //[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\12.0")]
    //[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\10.0")]
    ///
	/// Register a tool windows exposed by this package.
    //[ProvideToolWindow(typeof(MeteorShellWindow))]
	//[ProvideToolWindow(typeof(ExplorerWindow))]
    ///
    /// Add an options section and page in the Tools > Options dialog
	[ProvideOptionPage(typeof(OptionsPage), "Meteor Ride", "General", 101, 106, true, new[] { "Meteor Ride" })]
    ///
    /// Declare that this package provides creatable objects.
    //[ProvideObject(typeof(GeneralSettingsPage))]
    ///
    /// Declare that a package provides a project factory.
    //[ProvideProjectFactory(
    //    typeof(MeteorProjectFactory), 
    //    "Meteor Ride", 
    //    "Meteor Ride Files (*.meteorride);*.meteorride", 
    //    "meteorride", 
    //    "meteorride",
    //    @"..\..\Templates\Projects\MeteorProject"
    //    , LanguageVsTemplate = "meteorproject", 
    //    NewProjectRequireNewFolderVsTemplate = false
    //    )]
    /// 
    /// Declare that the package provides a project item.
    /// [ProvideProjectItem(typeof(MeteorProjectFactory), "Meteor Items", @"..\..\Templates\ProjectItems\MeteorProject", 500)]
    
    //[PackageRegistration(UseManagedResourcesOnly = true)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\10.0")]
    [ProvideObject(typeof(GeneralSettingsPage))]
    [ProvideProjectFactory(typeof(MeteorProjectFactory), "Meteor-Project", "Meteor Project Files (*.meteorride);*.meteorride", "meteorride", "meteorride", @"..\..\Templates\Projects", LanguageVsTemplate = "MeteorProject", NewProjectRequireNewFolderVsTemplate = false)]
    [ProvideProjectItem(typeof(MeteorProjectFactory), "My Items", @"..\..\Templates\ProjectItems\MeteorProject", 500)]
    [Guid(GuidList.guidMeteorRidePkgString)]
    public sealed class MeteorPackage : ProjectPackage
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public MeteorPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, 
				"Entering constructor for: {0}", this.ToString()));
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void ShowMeteorShell(object sender, EventArgs e)
        {
			// Get the instance number 0 of this tool window. This window is single instance so this instance
			// is actually the only one.
			// The last flag is set to true so that if the tool window does not exists it will be created.
			ToolWindowPane window = this.FindToolWindow(typeof(MeteorShellWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, 
				"Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();
            this.RegisterProjectFactory(new MeteorProjectFactory(this));

            // Add our command handlers for menu (commands must exist in the .vsct file)
            //OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            //if ( null != mcs )
            //{
            //     Create the command for the 'Show Meteor Explorer' menu item
            //    CommandID meteorExplorerCmdId = new CommandID(GuidList.guidMeteorRideCmdSet, (int)PkgCmdIDList.cmdIdMeteorExplorer);
            //    MenuCommand meteorExplorerMenuItem = new MenuCommand(MeteorExplorerMenuCallback, meteorExplorerCmdId);
            //    mcs.AddCommand(meteorExplorerMenuItem);

            //     Create the command for the Meteor Shell tool window
            //    CommandID toolwndCommandID = new CommandID(GuidList.guidMeteorRideCmdSet, (int)PkgCmdIDList.cmdIdMeteorShell);
            //    MenuCommand menuToolWin = new MenuCommand(ShowMeteorShell, toolwndCommandID);
            //    mcs.AddCommand( menuToolWin );
            //}
        }

        public override string ProductUserContext
        {
            get { return "MeteorProj"; }
        }

        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the Meteor Explorer menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MeteorExplorerMenuCallback(object sender, EventArgs e)
        {

			// Get the instance number 0 of this tool window. This window is single instance so this instance
			// is actually the only one.
			// The last flag is set to true so that if the tool window does not exists it will be created.
			ToolWindowPane window = this.FindToolWindow(typeof(ExplorerWindow), 0, true);
			if ((null == window) || (null == window.Frame))
			{
				throw new NotSupportedException(Resources.CanNotCreateWindow);
			}
			IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());


			//// Show a Message Box to prove we were here
			//IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
   //         Guid clsid = Guid.Empty;
   //         int result;
   //         Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
   //                    0,
   //                    ref clsid,
   //                    "Meteor Tools for Visual Studio",
   //                    string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
   //                    string.Empty,
   //                    0,
   //                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
   //                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
   //                    OLEMSGICON.OLEMSGICON_INFO,
   //                    0,        // false
   //                    out result));
        }

    }
}
