/***************************************************************************



***************************************************************************/

using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.UnitTestLibrary;
using System;
using System.Reflection;
using Vectria.MeteorRide;

namespace MeteorRide_UnitTests.ShellWindowTests
{
	[TestClass()]
	public class ShowShellWindowTest
	{
		string ShowShellMenu_MethodName = "ShowMeteorShell";

		[TestMethod()]
		public void ValidateShellWindowShown()
		{
			IVsPackage package = new MeteorPackage() as IVsPackage;

			// Create a basic service provider
			OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

			//Add uishell service that knows how to create a toolwindow
			BaseMock uiShellService = UIShellServiceMock.GetUiShellInstanceCreateToolWin();
			serviceProvider.AddService(typeof(SVsUIShell), uiShellService, false);

			// Site the package
			Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

			MethodInfo method = typeof(MeteorPackage).GetMethod(ShowShellMenu_MethodName, 
				BindingFlags.NonPublic | BindingFlags.Instance);

			object result = method.Invoke(package, new object[] { null, null });
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException), "Did not throw expected exception when windowframe object was null")]
		public void ShowToolwindowNegativeTest()
		{
			IVsPackage package = new MeteorPackage() as IVsPackage;

			// Create a basic service provider
			OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

			//Add uishell service that knows how to create a toolwindow
			BaseMock uiShellService = UIShellServiceMock.GetUiShellInstanceCreateToolWinReturnsNull();
			serviceProvider.AddService(typeof(SVsUIShell), uiShellService, false);

			// Site the package
			Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

			MethodInfo method = typeof(MeteorPackage).GetMethod(ShowShellMenu_MethodName, 
				BindingFlags.NonPublic | BindingFlags.Instance);

			//Invoke thows TargetInvocationException, but we want it's inner Exception thrown by ShowMeteorShell, InvalidOperationException.
			try
			{
				object result = method.Invoke(package, new object[] { null, null });
			}
			catch (Exception e)
			{
				throw e.InnerException;
			}
		}
	}
}
