//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "12.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Strings", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to Your login name must be at least {2} characters long!.
        /// </summary>
        internal static string ErrorMassage_LoginnameNotValid {
            get {
                return ResourceManager.GetString("ErrorMassage_LoginnameNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please type in your login name..
        /// </summary>
        internal static string ErrorMassage_LoginnameRequired {
            get {
                return ResourceManager.GetString("ErrorMassage_LoginnameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your password must be at least {2} characters long!.
        /// </summary>
        internal static string ErrorMassage_PasswordNotValid {
            get {
                return ResourceManager.GetString("ErrorMassage_PasswordNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please type in your password..
        /// </summary>
        internal static string ErrorMassage_PasswordRequired {
            get {
                return ResourceManager.GetString("ErrorMassage_PasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to !!! Strings !!!.
        /// </summary>
        internal static string GlobalResourceKey {
            get {
                return ResourceManager.GetString("GlobalResourceKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to About.
        /// </summary>
        internal static string MaskLink_About {
            get {
                return ResourceManager.GetString("MaskLink_About", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contact.
        /// </summary>
        internal static string MaskLink_Contact {
            get {
                return ResourceManager.GetString("MaskLink_Contact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AAA.
        /// </summary>
        internal static string MaskText_aaa {
            get {
                return ResourceManager.GetString("MaskText_aaa", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-Mail.
        /// </summary>
        internal static string MaskText_EMail {
            get {
                return ResourceManager.GetString("MaskText_EMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Log in.
        /// </summary>
        internal static string MaskText_Login {
            get {
                return ResourceManager.GetString("MaskText_Login", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login name.
        /// </summary>
        internal static string MaskText_Loginname {
            get {
                return ResourceManager.GetString("MaskText_Loginname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data lost?.
        /// </summary>
        internal static string MaskText_LoginnamePasswordLost {
            get {
                return ResourceManager.GetString("MaskText_LoginnamePasswordLost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password.
        /// </summary>
        internal static string MaskText_Password {
            get {
                return ResourceManager.GetString("MaskText_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remember me?.
        /// </summary>
        internal static string MaskText_RememberMe {
            get {
                return ResourceManager.GetString("MaskText_RememberMe", resourceCulture);
            }
        }
    }
}
