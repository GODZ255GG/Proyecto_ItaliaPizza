﻿using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for ModificarPedido.xaml
    /// </summary>
    public partial class ModificarPedido : Window
    {
        private Dictionary<int, int> productosContados = new Dictionary<int, int>();

        ItaliaPizzaServer.OrderManagerClient pedidosServer = new ItaliaPizzaServer.OrderManagerClient();
        private List<int> idsProductosSeleccionados = new List<int>();
        private List<ItaliaPizzaServer.Productos> productosSeleccionados = new List<ItaliaPizzaServer.Productos>();
        String TIPO_GLOBAL = "Pizza";

        public Pedidos Pedido { get; set; }

        public ModificarPedido(Pedidos pedido)
        {
            InitializeComponent();
            CargarProductos();
            CargarProductosConcatenados(pedido);
            ActualizarVisibilidadDomicilio(pedido.TipoDePedido);


            Pedido = pedido;
            DataContext = Pedido;

        }

        private void CargarProductos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionProductos(TIPO_GLOBAL);
                dgProductos.ItemsSource = pedidosDesdeBD;
            }
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void BtnCancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAceptarRegistro_Click(object sender, RoutedEventArgs e)
        {
            int idPedido = Pedido.IdPedidos;
            string nombreCliente = tbxNombreCliente.Text;
            string tipoPedido = Pedido.TipoDePedido;
            string domicilio = tbxDomicilio.Text;


            if (string.IsNullOrEmpty(lbTotalPedido.Content?.ToString()))
            {
                Utilidades.Utilidades.MostrarMensaje("El pedido no tiene productos.", "Pedido sin productos", MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrEmpty(tbxDomicilio.Text))
            {
                Utilidades.Utilidades.MostrarMensaje("El campo de domicilio no puede estar vacío.", "Domicilio faltante", MessageBoxImage.Warning);

                return;
            }

            double total = double.Parse(lbTotalPedido.Content.ToString());
            int[] idProductosArray = idsProductosSeleccionados.ToArray();

            try
            {
                if (String.IsNullOrEmpty(nombreCliente) || idProductosArray.Length == 0)
                {
                    MessageBox.Show("No puede haber campos vacíos");
                    return;
                }

                pedidosServer.ActualizarPedido(idPedido, nombreCliente, tipoPedido, domicilio, (decimal)total, idProductosArray);
                Utilidades.Utilidades.MostrarMensaje("Pedido actualizado exitosamente.", "Actualización exitosa", MessageBoxImage.Information);
                this.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void ActualizarVisibilidadDomicilio(string tipoPedido)
        {
            bool esDomicilio = tipoPedido.Equals("Domicilio", StringComparison.OrdinalIgnoreCase);
            lbDomicilio.Visibility = esDomicilio ? Visibility.Visible : Visibility.Hidden;
            tbxDomicilio.Visibility = esDomicilio ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnPizzas_Click(object sender, RoutedEventArgs e)
        {
            TIPO_GLOBAL = "Pizza";
            CargarProductos();
        }

        private void BtnPastas_Click(object sender, RoutedEventArgs e)
        {
            TIPO_GLOBAL = "Pasta";
            CargarProductos();
        }

        private void BtnPostres_Click(object sender, RoutedEventArgs e)
        {
            TIPO_GLOBAL = "Postre";
            CargarProductos();
        }

        private void BtnBebidas_Click(object sender, RoutedEventArgs e)
        {
            TIPO_GLOBAL = "Bebida";
            CargarProductos();
        }

        private void DgProductos_MouseDobleClic(object sender, MouseButtonEventArgs e)
        {
            if (dgProductos.SelectedItem != null && dgProductos.SelectedItem is ItaliaPizzaServer.Productos producto)
            {
                productosSeleccionados.Add(producto);

                idsProductosSeleccionados.Add(producto.IdProductos);

                MostrarProductosSeleccionados();

                CalcularYMostrarSumaTotal();
            }
        }

        private void MostrarProductosSeleccionados()
        {
            stackProductos.Children.Clear(); 

            productosContados.Clear();

            foreach (var producto in productosSeleccionados)
            {
                if (productosContados.ContainsKey(producto.IdProductos))
                {
                    productosContados[producto.IdProductos]++;
                }
                else
                {
                    productosContados.Add(producto.IdProductos, 1);
                }
            }

            foreach (var kvp in productosContados)
            {
                var producto = productosSeleccionados.FirstOrDefault(p => p.IdProductos == kvp.Key);
                if (producto != null)
                {
                    CrearItemProducto(producto, kvp.Value);
                }
            }
        }

        private void CrearItemProducto(ItaliaPizzaServer.Productos producto, int cantidad)
        {
            StackPanel itemPanel = new StackPanel();
            itemPanel.Orientation = Orientation.Horizontal;

            TextBlock productoTextBlock = new TextBlock();
            productoTextBlock.Text = $"{producto.Nombre} - ${producto.Precio} (x{cantidad})";
            productoTextBlock.FontSize = 16;
            itemPanel.Children.Add(productoTextBlock);

            Button eliminarButton = new Button();
            eliminarButton.Content = "Eliminar";
            eliminarButton.Tag = producto;
            eliminarButton.Click += EliminarProducto_Click;
            itemPanel.Children.Add(eliminarButton);

            stackProductos.Children.Add(itemPanel);
        }

        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = sender as Button;
            if (eliminarButton != null)
            {
                var producto = eliminarButton.Tag as ItaliaPizzaServer.Productos;
                if (producto != null)
                {
                    productosSeleccionados.Remove(producto);
                    idsProductosSeleccionados.Remove(producto.IdProductos);

                    MostrarProductosSeleccionados();
                    CalcularYMostrarSumaTotal();
                }
            }
        }

        private void CalcularYMostrarSumaTotal()
        {
            float sumaTotal = 0;

            foreach (var producto in productosSeleccionados)
            {
                sumaTotal += (float)(producto.Precio);
            }

            lbTotalPedido.Content = sumaTotal.ToString();
        }

        private void rbPedidoDomicilio_Checked(object sender, RoutedEventArgs e)
        {
            lbDomicilio.Visibility = Visibility.Visible;
            tbxDomicilio.Visibility = Visibility.Visible;
        }

        private void rbPedidoLocal_Checked(object sender, RoutedEventArgs e)
        {
            lbDomicilio.Visibility = Visibility.Hidden;
            tbxDomicilio.Visibility = Visibility.Hidden;
        }

        private void btnLimpiarPedido_Click(object sender, RoutedEventArgs e)
        {
            productosSeleccionados.Clear();
            stackProductos.Children.Clear();
            lbTotalPedido.Content = string.Empty;
        }

        private void CargarProductosConcatenados(Pedidos pedido)
        {
            int idPedido = pedido.IdPedidos;

            int[] idsProductosArray = pedidosServer.RecuperarIdsProductosDePedido(idPedido);

            productosContados.Clear();

            productosSeleccionados.Clear();
            idsProductosSeleccionados.Clear();

            foreach (int idProducto in idsProductosArray)
            {
                if (productosContados.ContainsKey(idProducto))
                {
                    productosContados[idProducto]++;
                }
                else
                {
                    productosContados.Add(idProducto, 1);
                }

                var producto = pedidosServer.RecuperarProductoPorId(idProducto);
                productosSeleccionados.Add(producto);

                idsProductosSeleccionados.Add(idProducto);
            }

            MostrarProductosSeleccionados();
            CalcularYMostrarSumaTotal();
        }


    }
}
