using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;
using System;
using System.ComponentModel.Design;

namespace MeteorRide_IntegrationTests
{

	[TestClass()]
	public class ShellWindowTest
	{
		private delegate void ThreadInvoker();

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		/// <summary>
		///A test for showing the ShellWindow
		///</summary>
		[TestMethod()]
		[HostType("VS IDE")]
		public void ShowShellWindow()
		{
			UIThreadInvoker.Invoke((ThreadInvoker)delegate ()
			{
				CommandID shellWindowCmd = new CommandID(Vectria.MeteorRide.GuidList.guidMeteorRideCmdSet, (int)Vectria.MeteorRide.PkgCmdIDList.cmdIdMeteorShell);

				TestUtils testUtils = new TestUtils();
				testUtils.ExecuteCommand(shellWindowCmd);

				Assert.IsTrue(testUtils.CanFindToolwindow(new Guid(Vectria.MeteorRide.GuidList.guidShellWindowPersistanceString)));

			});
		}

	}
}
