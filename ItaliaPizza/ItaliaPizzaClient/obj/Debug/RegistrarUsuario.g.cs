﻿#pragma checksum "..\..\RegistrarUsuario.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F101E068D39B50FEC4AE222CBA3CA82166F0D380D4EDD3E4C8D5E56E1C0A9B6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ItaliaPizzaClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ItaliaPizzaClient {
    
    
    /// <summary>
    /// RegistrarUsuario
    /// </summary>
    public partial class RegistrarUsuario : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxNombre;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxTelefono;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxApellidoP;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxCorreo;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxApellidoM;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxTipo;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\RegistrarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/registrarusuario.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegistrarUsuario.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\RegistrarUsuario.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.BtnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\RegistrarUsuario.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbxNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbxTelefono = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\RegistrarUsuario.xaml"
            this.tbxTelefono.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tbxTelefono_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 44 "..\..\RegistrarUsuario.xaml"
            this.tbxTelefono.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbxTelefono_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbxApellidoP = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbxCorreo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbxApellidoM = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.cbxTipo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\RegistrarUsuario.xaml"
            this.cbxTipo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbxTipo_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 53 "..\..\RegistrarUsuario.xaml"
            this.cbxTipo.Loaded += new System.Windows.RoutedEventHandler(this.CbxTipo_Loaded);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

