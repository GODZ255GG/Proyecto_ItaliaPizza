using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<int> idsProductosSeleccionados = new List<int>();
        private List<ItaliaPizzaServer.Productos> productosSeleccionados = new List<ItaliaPizzaServer.Productos>();

        String TIPO_GLOBAL = "Pizza";

        public RegistrarPedidos()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionProductos(TIPO_GLOBAL);
                dgProductos.ItemsSource = pedidosDesdeBD;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los pedidos: " + ex.Message);
            }
        }

        private void BtnCancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAceptarRegistro_Click(object sender, RoutedEventArgs e)
        {
            int idEmpleado = Domain.Empleados.EmpleadosClient.IdEmpleados;
            string nombreCliente = tbxNombreCliente.Text;
            string domicilio = rbPedidoDomicilio.IsChecked == true ? tbxDomicilio.Text : "";
            string tipoPedido = rbPedidoLocal.IsChecked == true ? "Local" : "Domicilio";

            if (string.IsNullOrEmpty(lbTotalPedido.Content?.ToString()))
            {
                MessageBox.Show("El total del pedido no puede estar vacío.");
                return;
            }

            double total = double.Parse(lbTotalPedido.Content.ToString());
            int[] idProductosArray = idsProductosSeleccionados.ToArray();

            try
            {
                if (rbPedidoDomicilio.IsChecked == true && string.IsNullOrEmpty(tbxDomicilio.Text))
                {
                    MessageBox.Show("El campo de domicilio no puede estar vacío para un pedido a domicilio.");
                    return;
                }

                if (rbPedidoDomicilio.IsChecked == false && rbPedidoLocal.IsChecked == false)
                {
                    MessageBox.Show("Se debe seleccionar un tipo de pedido");
                }
                else if (String.IsNullOrEmpty(nombreCliente) || idProductosArray.Length == 0)
                {
                    Console.WriteLine(idProductosArray.Length);
                    MessageBox.Show("No puede haber campos vacíos");
                }
                else
                {
                    pedidosServer.RegistrarNuevoPedido(nombreCliente, idEmpleado, domicilio, tipoPedido, (decimal)total, idProductosArray);
                    MessageBox.Show("Pedido registrado exitosamente.");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el pedido: " + ex.Message);
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
    }
}
