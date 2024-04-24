using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ListaProductos.xaml
    /// </summary>
    public partial class ListaProductos : UserControl
    {
        ProductManagerClient productoServer = new ProductManagerClient();
        public ListaProductos()
        {
            InitializeComponent();
            MostrarInformacionProductos();
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrar = new RegistrarProducto();
            registrar.Closed += ActualizarTablaProductos;
            registrar.Show();
            
        }

        private void ActualizarTablaProductos(object sender, EventArgs e)
        {
            MostrarInformacionProductos();
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void MostrarInformacionProductos()
        {
            try
            {
                Productos[] productopArray = productoServer.ObtenerListaProductos();
                List<Productos> productos = productopArray.ToList();

                dgListaProductos.ItemsSource = productos;

            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgListaProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgListaProductos.SelectedItem != null)
            {
                Productos productoSeleccionado = dgListaProductos.SelectedItem as Productos;
                ConsultarProducto consulta = new ConsultarProducto(productoSeleccionado);
                consulta.Closed += ActualizarTablaProductos;
                consulta.Show();
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Productos[] productopArray = productoServer.ObtenerListaProductos();
            List<Productos> productos = productopArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = productos.Where(x => x.Nombre.Contains(tbx.Text)).ToList();
                dgListaProductos.ItemsSource = null;
                dgListaProductos.ItemsSource = filtrarLista;
            }
            else
            {
                dgListaProductos.ItemsSource = productos;
            }
        }
    }
}
