using Microsoft.VisualStudio.Project.Automation;
using System.Runtime.InteropServices;

namespace Vectria.MeteorRide.Project
{
    [ComVisible(true)]
    public class OAMeteorProject : OAProject
    {
        #region Constructors
        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="project">Custom project.</param>
        public OAMeteorProject(MeteorProjectNode project)
            : base(project)
        {
        }
        #endregion
    }

    [ComVisible(true)]
    //[Guid("D7EDB436-6F5A-4EF4-9E3F-67C15C2FA301")]
    [Guid("FE3025F1-D100-4655-B61E-21B71B7B04DB")]
    public class OAMeteorProjectFileItem : OAFileItem
    {
        #region Constructors
        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="project">Automation project.</param>
        /// <param name="node">Custom file node.</param>
        public OAMeteorProjectFileItem(OAProject project, MeteorFileNode node)
            : base(project, node)
        {
        }
        #endregion
    }
}
