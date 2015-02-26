using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Vectria.MeteorRide
{
	/// <summary>
	/// Interaction logic for MeteorShellControl.xaml
	/// </summary>
	public partial class MeteorShellControl : UserControl
    {
        public MeteorShellControl()
        {
            InitializeComponent();
        }

        [SuppressMessage("Microsoft.Globalization", 
			"CA1300:SpecifyMessageBoxOptions")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(
				System.Globalization.CultureInfo.CurrentUICulture, 
				"We are inside {0}.button1_Click()", 
				this.ToString()), "Meteor Shell");

        }
    }
}