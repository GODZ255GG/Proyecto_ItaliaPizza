﻿#pragma checksum "..\..\ListaProductos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7FEF8D15598D454B93EE357750B4FDAA86CEA7FD6FE56F1EC23C1348826605CC"
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
    /// ListaProductos
    /// </summary>
    public partial class ListaProductos : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\ListaProductos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl contentControl;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\ListaProductos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegistrarProducto;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\ListaProductos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBuscar;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\ListaProductos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgRegresar;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\ListaProductos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgListaProductos;
        
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
            System.Uri resourceLocater = new System.Uri("/ItaliaPizzaClient;component/listaproductos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ListaProductos.xaml"
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
            this.contentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 2:
            this.btnRegistrarProducto = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\ListaProductos.xaml"
            this.btnRegistrarProducto.Click += new System.Windows.RoutedEventHandler(this.BtnRegistrarProducto_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbBuscar = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\ListaProductos.xaml"
            this.tbBuscar.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TbBuscar_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.imgRegresar = ((System.Windows.Controls.Image)(target));
            
            #line 64 "..\..\ListaProductos.xaml"
            this.imgRegresar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgRegresar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dgListaProductos = ((System.Windows.Controls.DataGrid)(target));
            
            #line 65 "..\..\ListaProductos.xaml"
            this.dgListaProductos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DgListaProductos_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

