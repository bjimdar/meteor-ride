using Microsoft.VisualStudio.Project;
using System;
using System.Runtime.InteropServices;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;


namespace Vectria.MeteorRide.Project
{
    /// <summary>
    /// Represent the methods for creating projects within the solution.
    /// </summary>
    //[Guid("7C65038C-1B2F-41E1-A629-BED71D161F6F")]
    [Guid("57E2B553-CED1-449D-BC9C-BA2DD3B1F9BC")]
    public class MeteorProjectFactory : ProjectFactory
    {
        #region Fields
        private MeteorPackage package;
        #endregion

        #region Constructors
        /// <summary>
        /// Explicit default constructor.
        /// </summary>
        /// <param name="package">Value of the project package for initialize internal package field.</param>
        public MeteorProjectFactory(MeteorPackage package)
            : base(package)
        {
            this.package = package;
        }
        #endregion

        #region Overriden implementation
        /// <summary>
        /// Creates a new project by cloning an existing template project.
        /// </summary>
        /// <returns></returns>
        protected override ProjectNode CreateProject()
        {
            MeteorProjectNode project = new MeteorProjectNode(this.package);
            project.SetSite((IOleServiceProvider)((IServiceProvider)this.package).GetService(typeof(IOleServiceProvider)));
            return project;
        }
        #endregion
    }

}
