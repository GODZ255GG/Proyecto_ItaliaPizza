﻿#pragma checksum "..\..\RegistrarPedidos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5DDD1181C93184E25EE87418E2CADF7B3E055199E9DA33DB5412C097C9C73854"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
    /// RegistrarPedidos
    /// </summary>
    public partial class RegistrarPedidos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelarRegistro;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptarRegistro;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackProductos;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTotalPedido;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbPedidoLocal;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbPedidoDomicilio;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLimpiarPedido;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPizzas;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPastas;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPostres;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBebidas;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgProductos;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbDomicilio;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxDomicilio;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxNombreCliente;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\RegistrarPedidos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxNombreCliente;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/registrarpedidos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegistrarPedidos.xaml"
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
            this.btnCancelarRegistro = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\RegistrarPedidos.xaml"
            this.btnCancelarRegistro.Click += new System.Windows.RoutedEventHandler(this.BtnCancelarRegistro_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAceptarRegistro = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\RegistrarPedidos.xaml"
            this.btnAceptarRegistro.Click += new System.Windows.RoutedEventHandler(this.BtnAceptarRegistro_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.stackProductos = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.lbTotalPedido = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.rbPedidoLocal = ((System.Windows.Controls.RadioButton)(target));
            
            #line 33 "..\..\RegistrarPedidos.xaml"
            this.rbPedidoLocal.Checked += new System.Windows.RoutedEventHandler(this.rbPedidoLocal_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.rbPedidoDomicilio = ((System.Windows.Controls.RadioButton)(target));
            
            #line 34 "..\..\RegistrarPedidos.xaml"
            this.rbPedidoDomicilio.Checked += new System.Windows.RoutedEventHandler(this.rbPedidoDomicilio_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnLimpiarPedido = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\RegistrarPedidos.xaml"
            this.btnLimpiarPedido.Click += new System.Windows.RoutedEventHandler(this.btnLimpiarPedido_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnPizzas = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\RegistrarPedidos.xaml"
            this.btnPizzas.Click += new System.Windows.RoutedEventHandler(this.BtnPizzas_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnPastas = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\RegistrarPedidos.xaml"
            this.btnPastas.Click += new System.Windows.RoutedEventHandler(this.BtnPastas_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnPostres = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\RegistrarPedidos.xaml"
            this.btnPostres.Click += new System.Windows.RoutedEventHandler(this.BtnPostres_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnBebidas = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\RegistrarPedidos.xaml"
            this.btnBebidas.Click += new System.Windows.RoutedEventHandler(this.BtnBebidas_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.dgProductos = ((System.Windows.Controls.DataGrid)(target));
            
            #line 42 "..\..\RegistrarPedidos.xaml"
            this.dgProductos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DgProductos_MouseDobleClic);
            
            #line default
            #line hidden
            return;
            case 13:
            this.lbDomicilio = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.tbxDomicilio = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.tbxNombreCliente = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\RegistrarPedidos.xaml"
            this.tbxNombreCliente.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TbxNombreCliente_TextChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.cbxNombreCliente = ((System.Windows.Controls.ComboBox)(target));
            
            #line 56 "..\..\RegistrarPedidos.xaml"
            this.cbxNombreCliente.Loaded += new System.Windows.RoutedEventHandler(this.CbxNombreCliente_Loaded);
            
            #line default
            #line hidden
            
            #line 56 "..\..\RegistrarPedidos.xaml"
            this.cbxNombreCliente.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CbxNombreCliente_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 57 "..\..\RegistrarPedidos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnLimpiarComboBoxCliente_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

