﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Library.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Name,#,Orbits,Distance (000 km),O_Period (days),Incl,Eccen,Discoverer,Date,A.K.A.,Diameter (000 km),Color (hex),R_Period (days),Type
        ///Sun,-,-,-,-,-,-,-,-,Sol,1392,#FFAA11,,Star
        ///Mercury,I,Sun,57910,87.97,7,0.21,-,-,,4.879,#AAAADD,,Planet
        ///Venus,II,Sun,108200,224.7,14305,0.01,-,-,,12.104,#DDDD44,,Planet
        ///Earth,III,Sun,149600,365.26,0,0.02,-,-,,12.756,#0000FF,1,Planet
        ///Mars,IV,Sun,227940,686.98,31048,0.09,-,-,,13.61,#DD4400,,Planet
        ///Jupiter,V,Sun,778330,4332.71,11324,0.05,-,-,,142.984,#AAAA11,,Planet
        ///Saturn,VI,Sun, [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Nineplanets {
            get {
                return ResourceManager.GetString("Nineplanets", resourceCulture);
            }
        }
    }
}
