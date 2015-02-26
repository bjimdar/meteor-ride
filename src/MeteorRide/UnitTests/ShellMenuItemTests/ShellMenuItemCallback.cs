using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.UnitTestLibrary;
using System.ComponentModel.Design;
using System.Reflection;
using Vectria.MeteorRide;

namespace MeteorRide_UnitTests.ShellMenuItemTests
{
	[TestClass()]
	public class ShellMenuItemTest
	{
		//string MeteorExplorerMenu_MethodName = "MeteorExplorerMenuCallback";

		/// <summary>
		/// Verify that a new "Show Meteor Shell" menu command object gets added to the OleMenuCommandService. 
		/// This action takes place In the Initialize method of the Package object
		/// </summary>
		[TestMethod]
		public void InitializeMenuCommand()
		{
			// Create the package
			IVsPackage package = new MeteorPackage() as IVsPackage;
			Assert.IsNotNull(package, "The object does not implement IVsPackage");

			// Create a basic service provider
			OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

			// Site the package
			Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

			//Verify that the menu command can be found
			CommandID shellMenuCommandID = new CommandID(GuidList.guidMeteorRideCmdSet, (int)PkgCmdIDList.cmdIdMeteorExplorer);
			MethodInfo info = typeof(Package).GetMethod("GetService", BindingFlags.Instance | BindingFlags.NonPublic);
			Assert.IsNotNull(info);
			OleMenuCommandService mcs = info.Invoke(package, new object[] { (typeof(IMenuCommandService)) }) as OleMenuCommandService;
			Assert.IsNotNull(mcs.FindCommand(shellMenuCommandID));
		}
		
		// Disabling window-show tests until I can figure how to get them to work on Appveyor CI
		//
		//[TestMethod]
		//public void shellMeteorExplorerMenuCallback()
		//{
		//	// Create the package
		//	IVsPackage package = new MeteorPackage() as IVsPackage;
		//	Assert.IsNotNull(package, "The object does not implement IVsPackage");

		//	// Create a basic service provider
		//	OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

		//	// Create a UIShell service mock and proffer the service so that it can called from the MenuItemCallback method
		//	BaseMock uishellMock = UIShellServiceMock.GetUiShellInstance();
		//	serviceProvider.AddService(typeof(SVsUIShell), uishellMock, true);

		//	// Site the package
		//	Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

		//	//Invoke private method on package class and observe that the method does not throw
		//	MethodInfo info = package.GetType().GetMethod(MeteorExplorerMenu_MethodName,
		//		BindingFlags.Instance | BindingFlags.NonPublic);

		//	Assert.IsNotNull(info, string.Format(
		//		"Failed to get the private method {0} through reflection", 
		//			MeteorExplorerMenu_MethodName));

		//	info.Invoke(package, new object[] { null, null });

		//	//Clean up services
		//	serviceProvider.RemoveService(typeof(SVsUIShell));

		//}
	}
}
