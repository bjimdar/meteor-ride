using Microsoft.VisualStudio.Shell;

namespace Vectria.MeteorRide
{
	class OptionsPage : DialogPage
	{
		/// <summary>
		/// The local path to the users meteor install directory
		/// </summary>
		public string MeteorPath { get; set; }
	}
}
