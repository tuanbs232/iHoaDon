﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iHoaDon.Resources.Admin {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AccountModelResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AccountModelResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("iHoaDon.Resources.Admin.AccountModelResource", typeof(AccountModelResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email không được lớn hơn 250 ký tự!.
        /// </summary>
        public static string EmailMaxLength {
            get {
                return ResourceManager.GetString("EmailMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email sai định dạng!.
        /// </summary>
        public static string EmailRegularExpression {
            get {
                return ResourceManager.GetString("EmailRegularExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bạn phải nhập Email!.
        /// </summary>
        public static string EmailRequied {
            get {
                return ResourceManager.GetString("EmailRequied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Họ và tên.
        /// </summary>
        public static string FullName {
            get {
                return ResourceManager.GetString("FullName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Họ và tên không được lớn hơn 250 ký tự!.
        /// </summary>
        public static string FullNameMaxLength {
            get {
                return ResourceManager.GetString("FullNameMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên đăng nhập.
        /// </summary>
        public static string LoginName {
            get {
                return ResourceManager.GetString("LoginName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên đăng nhập không được lớn hơn 250 ký tự!.
        /// </summary>
        public static string LoginNameMaxLength {
            get {
                return ResourceManager.GetString("LoginNameMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bạn phải nhập tên đăng nhập!.
        /// </summary>
        public static string LoginNameRequied {
            get {
                return ResourceManager.GetString("LoginNameRequied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mật khẩu.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Quyền.
        /// </summary>
        public static string Role {
            get {
                return ResourceManager.GetString("Role", resourceCulture);
            }
        }
    }
}