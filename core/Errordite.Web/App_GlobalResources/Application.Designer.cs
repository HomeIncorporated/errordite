//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Application {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Application() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Application", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to You already have an application with the specified name, pelase enter a different name..
        /// </summary>
        internal static string AddApplicationStatus_ApplicationExists {
            get {
                return ResourceManager.GetString("AddApplicationStatus_ApplicationExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have reached the maximum number of applications allowed under your current billing plan, you can upgrade using the page below..
        /// </summary>
        internal static string AddApplicationStatus_PlanThresholdReached {
            get {
                return ResourceManager.GetString("AddApplicationStatus_PlanThresholdReached", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All Applications.
        /// </summary>
        internal static string AllApplications {
            get {
                return ResourceManager.GetString("AllApplications", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to We could not find an application with the Id specified, please try again..
        /// </summary>
        internal static string DeleteApplicationStatus_ApplicationNotFound {
            get {
                return ResourceManager.GetString("DeleteApplicationStatus_ApplicationNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The application was deleted successfully..
        /// </summary>
        internal static string DeleteApplicationStatus_Ok {
            get {
                return ResourceManager.GetString("DeleteApplicationStatus_Ok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already have an application with the specified name, pelase enter a different name..
        /// </summary>
        internal static string EditApplicationStatus_ApplicationExists {
            get {
                return ResourceManager.GetString("EditApplicationStatus_ApplicationExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to We could not find an application with the Id specified, please try again..
        /// </summary>
        internal static string EditApplicationStatus_ApplicationNotFound {
            get {
                return ResourceManager.GetString("EditApplicationStatus_ApplicationNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; application was updated successfully..
        /// </summary>
        internal static string EditApplicationStatus_Ok {
            get {
                return ResourceManager.GetString("EditApplicationStatus_Ok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter a name for the application..
        /// </summary>
        internal static string Name_Required {
            get {
                return ResourceManager.GetString("Name_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You do not yet have any applications, please use the form below to add your first application..
        /// </summary>
        internal static string No_Applications {
            get {
                return ResourceManager.GetString("No_Applications", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select Application.
        /// </summary>
        internal static string SelectApplication {
            get {
                return ResourceManager.GetString("SelectApplication", resourceCulture);
            }
        }
    }
}
