﻿#pragma checksum "..\..\VentanaPrincipal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "23C277EDE0C5747ECA866EB8C40863453E522586E94140E8FB6328C6B18E1B6F"
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
    /// VentanaPrincipal
    /// </summary>
    public partial class VentanaPrincipal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUsuarios;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbNombreUsuario;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPedidos;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInventario;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRecetas;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinanzas;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrarSesion;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\VentanaPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl contentControl;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/ventanaprincipal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\VentanaPrincipal.xaml"
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
            this.btnUsuarios = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\VentanaPrincipal.xaml"
            this.btnUsuarios.Click += new System.Windows.RoutedEventHandler(this.BtnUsuarios_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbNombreUsuario = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnPedidos = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\VentanaPrincipal.xaml"
            this.btnPedidos.Click += new System.Windows.RoutedEventHandler(this.btnPedidos_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnInventario = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\VentanaPrincipal.xaml"
            this.btnInventario.Click += new System.Windows.RoutedEventHandler(this.BtnInventario_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRecetas = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.btnFinanzas = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\VentanaPrincipal.xaml"
            this.btnFinanzas.Click += new System.Windows.RoutedEventHandler(this.btnFinanzas_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCerrarSesion = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\VentanaPrincipal.xaml"
            this.btnCerrarSesion.Click += new System.Windows.RoutedEventHandler(this.BtnCerrarSesion_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.contentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

