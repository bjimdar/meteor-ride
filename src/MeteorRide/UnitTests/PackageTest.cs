using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.UnitTestLibrary;
using Vectria.MeteorRide;

namespace MeteorRide_UnitTests
{
	[TestClass()]
	public class PackageTest
	{
		[TestMethod()]
		public void CreateInstance()
		{
			MeteorPackage package = new MeteorPackage();
		}

		[TestMethod()]
		public void IsIVsPackage()
		{
			MeteorPackage package = new MeteorPackage();
			Assert.IsNotNull(package as IVsPackage, "The object does not implement IVsPackage");
		}

		[TestMethod()]
		public void SetSite()
		{
			// Create the package
			IVsPackage package = new MeteorPackage() as IVsPackage;
			Assert.IsNotNull(package, "The object does not implement IVsPackage");

			// Create a basic service provider
			OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

			// Site the package
			Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

			// Unsite the package
			Assert.AreEqual(0, package.SetSite(null), "SetSite(null) did not return S_OK");
		}
	}
}
