﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeParkingSystem.Accounts.Contract.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Validations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Validations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FreeParkingSystem.Accounts.Contract.Resources.Validations", typeof(Validations).Assembly);
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
        ///   Looks up a localized string similar to The claim already exists.
        /// </summary>
        public static string Claim_AlreadyExists {
            get {
                return ResourceManager.GetString("Claim_AlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The claim does not exist.
        /// </summary>
        public static string Claim_DoesNotExist {
            get {
                return ResourceManager.GetString("Claim_DoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing context item: User.
        /// </summary>
        public static string MappingContext_MissingUser {
            get {
                return ResourceManager.GetString("MappingContext_MissingUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password is not valid.
        /// </summary>
        public static string Password_GeneralMessage {
            get {
                return ResourceManager.GetString("Password_GeneralMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password cannot be hashed while being encrypted.
        /// </summary>
        public static string PasswordEncryption_EncryptedPasswordCannotBeHashed {
            get {
                return ResourceManager.GetString("PasswordEncryption_EncryptedPasswordCannotBeHashed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password is encrypted and cannot be validated..
        /// </summary>
        public static string PasswordValidation_CannotValidateEncryptedPassword {
            get {
                return ResourceManager.GetString("PasswordValidation_CannotValidateEncryptedPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password needs to have at least one Capital letter..
        /// </summary>
        public static string PasswordValidation_CapitalsRequired {
            get {
                return ResourceManager.GetString("PasswordValidation_CapitalsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password cannot be empty..
        /// </summary>
        public static string PasswordValidation_EmptyPassword {
            get {
                return ResourceManager.GetString("PasswordValidation_EmptyPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password needs to have less than: {0} characters.
        /// </summary>
        public static string PasswordValidation_MaximumCharacterRequired {
            get {
                return ResourceManager.GetString("PasswordValidation_MaximumCharacterRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password needs to have at least: {0} characters.
        /// </summary>
        public static string PasswordValidation_MinimumCharacterRequired {
            get {
                return ResourceManager.GetString("PasswordValidation_MinimumCharacterRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password needs to have at least one number..
        /// </summary>
        public static string PasswordValidation_NumbersRequired {
            get {
                return ResourceManager.GetString("PasswordValidation_NumbersRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password needs to have at least one special character..
        /// </summary>
        public static string PasswordValidation_SpecialsRequired {
            get {
                return ResourceManager.GetString("PasswordValidation_SpecialsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username is unavailable.
        /// </summary>
        public static string User_AlreadyExists {
            get {
                return ResourceManager.GetString("User_AlreadyExists", resourceCulture);
            }
        }
    }
}
