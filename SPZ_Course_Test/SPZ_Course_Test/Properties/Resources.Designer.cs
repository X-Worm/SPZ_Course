﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPZ_Course_Test.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SPZ_Course_Test.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to ;===Procedure ltAnd====================
        ///ltAnd PROC
        ///	push ax
        ///	push dx
        ///	pushf
        ///
        ///	fistp lb1
        ///	fist lb2
        ///	mov ax,lb1
        ///	cmp ax,0
        ///
        ///	jnz true_and1
        ///	jz false_and
        ///	true_and1:
        ///
        ///	mov ax,lb2
        ///	cmp ax,0
        ///	jnz true_and
        ///
        ///	false_and:
        ///	fldz 
        ///	jmp l_and
        ///
        ///	true_and:
        ///	fld1
        ///	l_and: 
        ///	popf
        ///	tpop dx
        ///	pop ax
        ///	ret
        ///ltAnd ENDP
        ///
        ///	.
        /// </summary>
        internal static string SPZ_PrintAND {
            get {
                return ResourceManager.GetString("SPZ_PrintAND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure eq_======================
        ///eq_ PROC
        ///	push ax
        ///	push dx
        ///	pushf
        ///	fistp lb1
        ///	fistp lb2
        ///	mov ax,lb1
        ///	mov dx,lb2
        ///	cmp ax,dx
        ///	jne not_eq
        ///	fld1
        ///	jmp l_eq
        ///not_eq: fldz
        ///l_eq:	popf
        ///	pop dx
        ///	pop ax
        ///ret
        ///eq_ ENDP.
        /// </summary>
        internal static string SPZ_PrintEQ {
            get {
                return ResourceManager.GetString("SPZ_PrintEQ", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure ltGreate======================
        ///ltGreate PROC
        ///	push ax
        ///	push dx
        ///	pushf
        ///	fistp lb1
        ///	fistp lb2
        ///	mov ax,lb1
        ///	mov dx,lb2
        ///	cmp dx,ax
        ///	jl lov
        ///	fld1
        ///	jmp l_ge
        ///lov:	fldz
        ///l_ge:	popf
        ///	pop dx
        ///	pop ax
        ///	ret
        ///ltGreate ENDP.
        /// </summary>
        internal static string SPZ_PrintGE {
            get {
                return ResourceManager.GetString("SPZ_PrintGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure ltLess======================
        ///ltLess PROC
        ///	push ax
        ///	push dx
        ///	pushf
        ///	fistp lb1
        ///	fistp lb2
        ///	mov ax, lb1
        ///	mov dx, lb2
        ///	cmp dx,ax
        ///	jg gr
        ///lo:
        ///	fld1
        ///	jmp l_le
        ///gr:
        ///	fldz
        ///	l_le:
        ///	popf
        ///	pop dx
        ///	pop ax
        ///	ret
        ///ltLess ENDP
        ///.
        /// </summary>
        internal static string SPZ_PrintLE {
            get {
                return ResourceManager.GetString("SPZ_PrintLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure mod_====================
        ///	mod_ PROC
        ///	fistp lb1
        ///	fistp lb2
        ///	fild lb1
        ///	fild lb2
        ///	fprem
        ///	ret
        ///mod_ ENDP.
        /// </summary>
        internal static string SPZ_PrintMOD {
            get {
                return ResourceManager.GetString("SPZ_PrintMOD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure ltNot====================
        ///ltNot PROC
        ///	push ax
        ///	pushf
        ///	fistp lb1
        ///	mov ax,lb1
        ///	cmp ax,0
        ///	jne is_true
        ///	fld1
        ///	jmp l_not
        ///is_true:
        ///	fldz
        ///l_not:
        ///	popf
        ///	pop ax
        ///	ret
        ///ltNot ENDP.
        /// </summary>
        internal static string SPZ_PrintNOT {
            get {
                return ResourceManager.GetString("SPZ_PrintNOT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;===Procedure ltOr======================
        ///ltOr PROC
        ///	push ax
        ///	push dx
        ///	pushf
        ///	fistp lb1
        ///	fist lb2
        ///	mov ax,lb1
        ///	cmp ax,0
        ///	jnz true_or
        ///	mov ax,lb2
        ///	cmp ax,0
        ///	jnz true_or
        ///	fldz
        ///	jmp l_or
        ///
        ///	true_or:
        ///	fld1
        ///
        ///	l_or:
        ///	popf
        ///	pop dx
        ///	pop ax
        ///	ret
        ///ltOr ENDP.
        /// </summary>
        internal static string SPZ_PrintOR {
            get {
                return ResourceManager.GetString("SPZ_PrintOR", resourceCulture);
            }
        }
    }
}
