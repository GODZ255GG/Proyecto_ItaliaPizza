using Domain;
using Microsoft.VisualBasic.Logging;
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
    /// Interaction logic for RegistrarPedidos.xaml
    /// </summary>
    public partial class RegistrarPedidos : Window
    {
        ItaliaPizzaServer.OrderManagerClient pedidosServer = new ItaliaPizzaServer.OrderManagerClient();
        ItaliaPizzaServer.UserManagerClient usuariosServer = new ItaliaPizzaServer.UserManagerClient();
        private List<int> idsProductosSeleccionados = new List<int>();
        private List<ItaliaPizzaServer.Productos> productosSeleccionados = new List<ItaliaPizzaServer.Productos>();

        String TIPO_GLOBAL = "Pizza";

        public RegistrarPedidos()
        {
            InitializeComponent();
            Loaded += CbxNombreCliente_Loaded;
            CargarProductos();
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
            int idEmpleado = Domain.Empleados.EmpleadosClient.IdEmpleados;
            string nombreCliente = ObtenerNombreCliente();
            string domicilio = rbPedidoDomicilio.IsChecked == true ? tbxDomicilio.Text : "";
            string tipoPedido = rbPedidoLocal.IsChecked == true ? "Local" : "Domicilio";

            if (string.IsNullOrEmpty(lbTotalPedido.Content?.ToString()))
            {
                Utilidades.Utilidades.MostrarMensaje("El pedido no tiene productos.", "Pedido sin productos", MessageBoxImage.Warning);
                return;
            }

            double total = double.Parse(lbTotalPedido.Content.ToString());
            int[] idProductosArray = idsProductosSeleccionados.ToArray();

            try
            {
                if (rbPedidoDomicilio.IsChecked == true && string.IsNullOrEmpty(tbxDomicilio.Text))
                {
                    Utilidades.Utilidades.MostrarMensaje("El campo de domicilio no puede estar vacío.", "Domicilio faltante", MessageBoxImage.Warning);

                    return;
                }
                 
                if (rbPedidoDomicilio.IsChecked == false && rbPedidoLocal.IsChecked == false)
                {
                    Utilidades.Utilidades.MostrarMensaje("Se debe seleccionar un tipo de pedido.", "Sin tipo de pedido", MessageBoxImage.Warning);
                }
                else if (String.IsNullOrEmpty(nombreCliente) || idProductosArray.Length == 0)
                {
                    Utilidades.Utilidades.MostrarMensaje("No puede haber campos vacíos.", "Campos vacíos", MessageBoxImage.Warning);
                }
                else
                {
                    pedidosServer.RegistrarNuevoPedido(nombreCliente, idEmpleado, domicilio, tipoPedido, (decimal)total, idProductosArray);
                    Utilidades.Utilidades.MostrarMensaje("Pedido registrado exitosamente", "Registro exitoso", MessageBoxImage.Information);
                    this.Close();
                }

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
                int cantidadActual = productosSeleccionados.Count(p => p.IdProductos == producto.IdProductos);

                if (cantidadActual >= 30)
                {
                    MessageBox.Show("No puedes añadir más de 30 productos del mismo tipo.", "Límite de productos alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                productosSeleccionados.Add(producto);

                idsProductosSeleccionados.Add(producto.IdProductos);

                MostrarProductosSeleccionados();

                CalcularYMostrarSumaTotal();
            }
        }

        private void MostrarProductosSeleccionados()
        {
            stackProductos.Children.Clear(); // Limpiar los elementos anteriores

            Dictionary<int, int> productosContados = new Dictionary<int, int>();

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

        private void CbxNombreCliente_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ItaliaPizzaServer.Empleados[] empleadosArray = usuariosServer.ObtenerListaUsuarios();

                var nombresClientes = empleadosArray
                                        .Where(usuario => usuario.Rol == "Cliente")
                                        .Select(usuario => $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}")
                                        .ToList();

                cbxNombreCliente.ItemsSource = nombresClientes;
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

        private void BtnLimpiarComboBoxCliente_Click(object sender, RoutedEventArgs e)
        {
            cbxNombreCliente.SelectedItem = null;
        }

        private void TbxNombreCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbxNombreCliente.IsEnabled = string.IsNullOrEmpty(tbxNombreCliente.Text);
        }

        private void CbxNombreCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxNombreCliente.SelectedItem != null)
            {
                tbxNombreCliente.IsEnabled = false;
            }
            else
            {
                tbxNombreCliente.IsEnabled = true;
            }
        }

        private string ObtenerNombreCliente()
        {
            return tbxNombreCliente.IsEnabled ? tbxNombreCliente.Text : cbxNombreCliente.SelectedItem?.ToString();
        }
    }
}
