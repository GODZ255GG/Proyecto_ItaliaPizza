﻿#pragma checksum "..\..\DetallesPedido.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3ECC8EEA8E3DFC6215333201A33FCADC853934E2388179B90AFD78511F27B14B"
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
    /// DetallesPedido
    /// </summary>
    public partial class DetallesPedido : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxDetallesPedido;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbDomicilio_Title;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbDomicilio;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblEstadoPedido;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnModificarPedido;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegresar;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\DetallesPedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnModificarEstado;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/detallespedido.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DetallesPedido.xaml"
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
            this.tbxDetallesPedido = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.lbDomicilio_Title = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txbDomicilio = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lblEstadoPedido = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnModificarPedido = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\DetallesPedido.xaml"
            this.btnModificarPedido.Click += new System.Windows.RoutedEventHandler(this.BtnModificarPedido_Clic);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnRegresar = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\DetallesPedido.xaml"
            this.btnRegresar.Click += new System.Windows.RoutedEventHandler(this.BtnRegresar_Clic);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnModificarEstado = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\DetallesPedido.xaml"
            this.btnModificarEstado.Click += new System.Windows.RoutedEventHandler(this.btnModificarEstado_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
