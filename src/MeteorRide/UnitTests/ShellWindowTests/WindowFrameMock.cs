using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VsSDK.UnitTestLibrary;
using System;

namespace MeteorRide_UnitTests.ShellWindowTests
{
	class WindowFrameMock
	{
		const string propertiesName = "properties";

		private static GenericMockFactory frameFactory = null;

		/// <summary>
		/// Return a IVsWindowFrame without any special implementation
		/// </summary>
		/// <returns></returns>
		internal static IVsWindowFrame GetBaseFrame()
		{
			if (frameFactory == null)
				frameFactory = new GenericMockFactory("WindowFrame", new Type[] { typeof(IVsWindowFrame), typeof(IVsWindowFrame2) });
			IVsWindowFrame frame = (IVsWindowFrame)frameFactory.GetInstance();
			return frame;
		}
	}
}
