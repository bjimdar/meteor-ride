using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Vectria.MeteorRide;
namespace MeteorRide_UnitTests.ShellWindowTests
{
	/// <summary>
	///This is a test class for MyShellWindowTest and is intended
	///to contain all MyShellWindowTest Unit Tests
	///</summary>
	[TestClass()]
	public class ShellWindowTest
	{

		/// <summary>
		///MyShellWindow Constructor test
		///</summary>
		[TestMethod()]
		public void MeteorShellWindowConstructorTest()
		{

			MeteorShellWindow target = new MeteorShellWindow();
			Assert.IsNotNull(target, "Failed to create an instance of MyShellWindow");

			MethodInfo method = target.GetType().GetMethod("get_Content", BindingFlags.Public | BindingFlags.Instance);
			Assert.IsNotNull(method.Invoke(target, null), "MyControl object was not instantiated");

		}

		/// <summary>
		///Verify the Content property is valid.
		///</summary>
		[TestMethod()]
		public void WindowPropertyTest()
		{
			MeteorShellWindow target = new MeteorShellWindow();
			Assert.IsNotNull(target.Content, "Content property was null");
		}

	}
}
