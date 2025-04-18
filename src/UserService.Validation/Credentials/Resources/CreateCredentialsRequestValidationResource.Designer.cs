﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversityHelper.UserService.Validation.Credentials.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CreateCredentialsRequestValidationResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CreateCredentialsRequestValidationResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UniversityHelper.UserService.Validation.Credentials.Resources.CreateCredentialsRe" +
                            "questValidationResource", typeof(CreateCredentialsRequestValidationResource).Assembly);
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
        ///   Looks up a localized string similar to The credentials already exist.
        /// </summary>
        internal static string CredentialsExist {
            get {
                return ResourceManager.GetString("CredentialsExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login already in use.
        /// </summary>
        internal static string LoginExist {
            get {
                return ResourceManager.GetString("LoginExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login is too long.
        /// </summary>
        internal static string LoginLong {
            get {
                return ResourceManager.GetString("LoginLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login must contain only Latin letters and digits or only Latin letters.
        /// </summary>
        internal static string LoginMatch {
            get {
                return ResourceManager.GetString("LoginMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login is too short.
        /// </summary>
        internal static string LoginShort {
            get {
                return ResourceManager.GetString("LoginShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong password.
        /// </summary>
        internal static string PasswordWrong {
            get {
                return ResourceManager.GetString("PasswordWrong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User Id can&apos;t be empty.
        /// </summary>
        internal static string UserId {
            get {
                return ResourceManager.GetString("UserId", resourceCulture);
            }
        }
    }
}
