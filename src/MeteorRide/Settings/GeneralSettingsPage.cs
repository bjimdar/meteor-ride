﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Vectria.MeteorRide.Helpers;

namespace Vectria.MeteorRide.Settings
{
    /// <summary>
    /// This class implements general property page for the project type.
    /// </summary>
    [ComVisible(true)]
    //[Guid("5F9F1697-2E61-4c10-9AD2-94FA2A9BAAE8")]
    [Guid("73488167-7637-40ED-8EC4-D1399CD74FA4")]
    public class GeneralSettingsPage : SettingsPage
    {
        #region Fields
        private string assemblyName;
        private OutputType outputType;
        private string defaultNamespace;
        private string startupObject;
        private string applicationIcon;
        private FrameworkName targetFrameworkMoniker;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Explicitly defined default constructor.
        /// </summary>
        public GeneralSettingsPage()
        {
            
            this.Name = Resources.GeneralCaption;
        }
        #endregion

        #region Properties
        [ResourcesCategoryAttribute(ResourceHelper.AssemblyName)]
        [LocDisplayNameAttribute(ResourceHelper.AssemblyName)]
        [ResourcesDescriptionAttribute(ResourceHelper.AssemblyNameDescription)]
        /// <summary>
        /// Gets or sets Assembly Name.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string AssemblyName
        {
            get { return this.assemblyName; }
            set { this.assemblyName = value; this.IsDirty = true; }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Application)]
        [LocDisplayName(ResourceHelper.OutputType)]
        [ResourcesDescriptionAttribute(ResourceHelper.OutputTypeDescription)]
        /// <summary>
        /// Gets or sets OutputType.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public OutputType OutputType
        {
            get { return this.outputType; }
            set { this.outputType = value; this.IsDirty = true; }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Application)]
        [LocDisplayName(ResourceHelper.DefaultNamespace)]
        [ResourcesDescriptionAttribute(ResourceHelper.DefaultNamespaceDescription)]
        /// <summary>
        /// Gets or sets Default Namespace.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string DefaultNamespace
        {
            get { return this.defaultNamespace; }
            set { this.defaultNamespace = value; this.IsDirty = true; }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Application)]
        [LocDisplayName(ResourceHelper.StartupObject)]
        [ResourcesDescriptionAttribute(ResourceHelper.StartupObjectDescription)]
        /// <summary>
        /// Gets or sets Startup Object.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string StartupObject
        {
            get { return this.startupObject; }
            set { this.startupObject = value; this.IsDirty = true; }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Application)]
        [LocDisplayName(ResourceHelper.ApplicationIcon)]
        [ResourcesDescriptionAttribute(ResourceHelper.ApplicationIconDescription)]
        /// <summary>
        /// Gets or sets Application Icon.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string ApplicationIcon
        {
            get { return this.applicationIcon; }
            set { this.applicationIcon = value; this.IsDirty = true; }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Project)]
        [LocDisplayName(ResourceHelper.ProjectFile)]
        [ResourcesDescriptionAttribute(ResourceHelper.ProjectFileDescription)]
        /// <summary>
        /// Gets the path to the project file.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string ProjectFile
        {
            get { return Path.GetFileName(this.ProjectMgr.ProjectFile); }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Project)]
        [LocDisplayName(ResourceHelper.ProjectFolder)]
        [ResourcesDescriptionAttribute(ResourceHelper.ProjectFolderDescription)]
        /// <summary>
        /// Gets the path to the project folder.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string ProjectFolder
        {
            get { return Path.GetDirectoryName(this.ProjectMgr.ProjectFolder); }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Project)]
        [LocDisplayName(ResourceHelper.OutputFile)]
        [ResourcesDescriptionAttribute(ResourceHelper.OutputFileDescription)]
        /// <summary>
        /// Gets the output file name depending on current OutputType.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public string OutputFile
        {
            get
            {
                switch (this.outputType)
                {
                    case OutputType.Exe:
                    case OutputType.WinExe:
                        {
                            return this.assemblyName + ".exe";
                        }

                    default:
                        {
                            return this.assemblyName + ".dll";
                        }
                }
            }
        }

        [ResourcesCategoryAttribute(ResourceHelper.Project)]
        [LocDisplayName(ResourceHelper.TargetFrameworkMoniker)]
        [ResourcesDescriptionAttribute(ResourceHelper.TargetFrameworkMonikerDescription)]
        [PropertyPageTypeConverter(typeof(FrameworkNameConverter))]
        /// <summary>
        /// Gets or sets Target Platform PlatformType.
        /// </summary>
        /// <remarks>IsDirty flag was switched to true.</remarks>
        public FrameworkName TargetFrameworkMoniker
        {
            get { return this.targetFrameworkMoniker; }
            set { this.targetFrameworkMoniker = value; IsDirty = true; }
        }

        #endregion

        #region Overriden Implementation
        /// <summary>
        /// Returns class FullName property value.
        /// </summary>
        public override string GetClassName()
        {
            return this.GetType().FullName;
        }

        /// <summary>
        /// Bind properties.
        /// </summary>
        protected override void BindProperties()
        {
            if (this.ProjectMgr == null)
            {
                return;
            }

            this.assemblyName = this.ProjectMgr.GetProjectProperty("AssemblyName", true);

            string outputType = this.ProjectMgr.GetProjectProperty("OutputType", false);

            if (outputType != null && outputType.Length > 0)
            {
                try
                {
                    this.outputType = (OutputType)Enum.Parse(typeof(OutputType), outputType);
                }
                catch (ArgumentException)
                {
                }
            }

            this.defaultNamespace = this.ProjectMgr.GetProjectProperty("RootNamespace", false);
            this.startupObject = this.ProjectMgr.GetProjectProperty("StartupObject", false);
            this.applicationIcon = this.ProjectMgr.GetProjectProperty("ApplicationIcon", false);

            try
            {
                this.targetFrameworkMoniker = this.ProjectMgr.TargetFrameworkMoniker;
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Apply Changes on project node.
        /// </summary>
        /// <returns>E_INVALIDARG if internal ProjectMgr is null, otherwise applies changes and return S_OK.</returns>
        protected override int ApplyChanges()
        {
            if (this.ProjectMgr == null)
            {
                return VSConstants.E_INVALIDARG;
            }

            IVsPropertyPageFrame propertyPageFrame = (IVsPropertyPageFrame)this.ProjectMgr.Site.GetService((typeof(SVsPropertyPageFrame)));
            bool reloadRequired = this.ProjectMgr.TargetFrameworkMoniker != this.targetFrameworkMoniker;

            this.ProjectMgr.SetProjectProperty("AssemblyName", this.assemblyName);
            this.ProjectMgr.SetProjectProperty("OutputType", this.outputType.ToString());
            this.ProjectMgr.SetProjectProperty("RootNamespace", this.defaultNamespace);
            this.ProjectMgr.SetProjectProperty("StartupObject", this.startupObject);
            this.ProjectMgr.SetProjectProperty("ApplicationIcon", this.applicationIcon);

            if (reloadRequired)
            {
                if (MessageBox.Show(SR.GetString(SR.ReloadPromptOnTargetFxChanged), SR.GetString(SR.ReloadPromptOnTargetFxChangedCaption), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.ProjectMgr.TargetFrameworkMoniker = this.targetFrameworkMoniker;
                }
            }

            this.IsDirty = false;

            if (reloadRequired)
            {
                // This prevents the property page from displaying bad data from the zombied (unloaded) project
                propertyPageFrame.HideFrame();
                propertyPageFrame.ShowFrame(this.GetType().GUID);
            }

            return VSConstants.S_OK;
        }
        #endregion
    }
}
