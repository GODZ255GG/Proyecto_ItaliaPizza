﻿#pragma checksum "..\..\ConsultarInsumo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1649CF45EEE39A8EFC702CC78B6C6D8DC056CCDFC33624D27954BEF39DC2CF1B"
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
    /// ConsultarInsumo
    /// </summary>
    public partial class ConsultarInsumo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEliminar;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnModificar;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxNombre;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxMarca;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxCodigoInsumo;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxCantidad;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxTipo;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitulo;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\ConsultarInsumo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxMedida;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/consultarinsumo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConsultarInsumo.xaml"
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
            this.btnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\ConsultarInsumo.xaml"
            this.btnEliminar.Click += new System.Windows.RoutedEventHandler(this.BtnEliminar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnModificar = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\ConsultarInsumo.xaml"
            this.btnModificar.Click += new System.Windows.RoutedEventHandler(this.BtnModificar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbxNombre = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\ConsultarInsumo.xaml"
            this.tbxNombre.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbxNombre_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbxMarca = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\ConsultarInsumo.xaml"
            this.tbxMarca.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbxMarca_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbxCodigoInsumo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbxCantidad = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\ConsultarInsumo.xaml"
            this.tbxCantidad.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbxCantidad_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cbxTipo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\ConsultarInsumo.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.BtnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\ConsultarInsumo.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.BtnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lbTitulo = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.cbxMedida = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\ConsultarInsumo.xaml"
            this.cbxMedida.Loaded += new System.Windows.RoutedEventHandler(this.CbxMedida_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

