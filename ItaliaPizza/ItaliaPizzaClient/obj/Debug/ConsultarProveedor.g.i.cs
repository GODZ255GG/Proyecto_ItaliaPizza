﻿#pragma checksum "..\..\ConsultarProveedor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E10631D63FE8F6529ABD39E4B428266CA656396A0D96EC91F612FDFE2132F5A8"
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
    /// ConsultarProveedor
    /// </summary>
    public partial class ConsultarProveedor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgRegresar;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbModificarProveedor_Title;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEliminar;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnModificar;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxNombreContacto;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxTelefono;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxNombreCompañia;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxDireccion;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\ConsultarProveedor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxCiudad;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/consultarproveedor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConsultarProveedor.xaml"
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
            this.imgRegresar = ((System.Windows.Controls.Image)(target));
            
            #line 40 "..\..\ConsultarProveedor.xaml"
            this.imgRegresar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgRegresar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbModificarProveedor_Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\ConsultarProveedor.xaml"
            this.btnEliminar.Click += new System.Windows.RoutedEventHandler(this.BtnEliminar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnModificar = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\ConsultarProveedor.xaml"
            this.btnModificar.Click += new System.Windows.RoutedEventHandler(this.BtnModificar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\ConsultarProveedor.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.BtnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\ConsultarProveedor.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.BtnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbxNombreContacto = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbxTelefono = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbxNombreCompañia = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.tbxDireccion = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.cbxCiudad = ((System.Windows.Controls.ComboBox)(target));
            
            #line 67 "..\..\ConsultarProveedor.xaml"
            this.cbxCiudad.Loaded += new System.Windows.RoutedEventHandler(this.CbxTipo_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

