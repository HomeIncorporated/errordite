//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Admin {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Admin() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Admin", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Organisation is already active..
        /// </summary>
        internal static string ActivateOrganisationStatus_AccountAlreadyActivate {
            get {
                return ResourceManager.GetString("ActivateOrganisationStatus_AccountAlreadyActivate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Organisation has been activated successfully..
        /// </summary>
        internal static string ActivateOrganisationStatus_Ok {
            get {
                return ResourceManager.GetString("ActivateOrganisationStatus_Ok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Organisation settings were updated successfully..
        /// </summary>
        internal static string OrganisationSettingsUpdated {
            get {
                return ResourceManager.GetString("OrganisationSettingsUpdated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Other.
        /// </summary>
        internal static string SuspendedReason_Other {
            get {
                return ResourceManager.GetString("SuspendedReason_Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment arrears.
        /// </summary>
        internal static string SuspendedReason_PaymentArrears {
            get {
                return ResourceManager.GetString("SuspendedReason_PaymentArrears", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Requested by user.
        /// </summary>
        internal static string SuspendedReason_RequestedByAccountHolder {
            get {
                return ResourceManager.GetString("SuspendedReason_RequestedByAccountHolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Organisation is already suspended..
        /// </summary>
        internal static string SuspendOrganisationStatus_AccountAlreadySuspended {
            get {
                return ResourceManager.GetString("SuspendOrganisationStatus_AccountAlreadySuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Organisation suspended successfully..
        /// </summary>
        internal static string SuspendOrganisationStatus_Ok {
            get {
                return ResourceManager.GetString("SuspendOrganisationStatus_Ok", resourceCulture);
            }
        }
    }
}
