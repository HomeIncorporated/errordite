//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
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
    internal class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Errors", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to Please select an application..
        /// </summary>
        internal static string ApplicationId_Not_Set {
            get {
                return ResourceManager.GetString("ApplicationId_Not_Set", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a log file to import..
        /// </summary>
        internal static string File_Not_Set {
            get {
                return ResourceManager.GetString("File_Not_Set", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The application you have selected is not active, please either activate it or select a different application..
        /// </summary>
        internal static string ImportErrorLogsStatus_ApplicationNotActive {
            get {
                return ResourceManager.GetString("ImportErrorLogsStatus_ApplicationNotActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find your selected application, please try again..
        /// </summary>
        internal static string ImportErrorLogsStatus_ApplicationNotFound {
            get {
                return ResourceManager.GetString("ImportErrorLogsStatus_ApplicationNotFound", resourceCulture);
            }
        }
    }
}
