﻿#pragma checksum "..\..\ModificarEstadoDePedido.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "78801551536AEC2F526D52D5D2E24CC3C73F2B4EDBACA7619601E3784AEC3223"
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
    /// ModificarEstadoDePedido
    /// </summary>
    public partial class ModificarEstadoDePedido : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblEstadoPedido;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbEnPreparacion;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbListoParaEntrega;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbEntregado;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbCancelado;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbEnRutaDeEntrega;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnModificarPedido;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ModificarEstadoDePedido.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegresar;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/modificarestadodepedido.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ModificarEstadoDePedido.xaml"
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
            this.lblEstadoPedido = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.rbEnPreparacion = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.rbListoParaEntrega = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.rbEntregado = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.rbCancelado = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.rbEnRutaDeEntrega = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.btnModificarPedido = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ModificarEstadoDePedido.xaml"
            this.btnModificarPedido.Click += new System.Windows.RoutedEventHandler(this.BtnAceptarEstado_Clic);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnRegresar = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\ModificarEstadoDePedido.xaml"
            this.btnRegresar.Click += new System.Windows.RoutedEventHandler(this.BtnCancelarEstado_Clic);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

