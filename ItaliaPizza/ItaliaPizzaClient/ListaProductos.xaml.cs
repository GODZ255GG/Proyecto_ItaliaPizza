using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using System;
using Logs;
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
        private static readonly ILog Log = Logger.GetLogger();
        public ListaProductos()
        {
            InitializeComponent();
            MostrarInformacionPedidos();
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrar = new RegistrarProducto();
            registrar.Closed += ActualizarTablaProductos;
            registrar.Show();
            
        }

        private void ActualizarTablaProductos(object sender, EventArgs e)
        {
            MostrarInformacionPedidos();
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void MostrarInformacionPedidos()
        {
            try
            {
                Productos[] productopArray = productoServer.ObtenerListaProductos();
                List<Productos> productos = productopArray.ToList();

                dgListaProductos.ItemsSource = productos;

            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void DgListaProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgListaProductos.SelectedItem != null)
                {
                    Productos productoSeleccionado = dgListaProductos.SelectedItem as Productos;
                    ConsultarProducto consulta = new ConsultarProducto(productoSeleccionado);
                    consulta.Closed += ActualizarTablaProductos;
                    consulta.Show();
                }

            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Productos[] productopArray = productoServer.ObtenerListaProductos();
                List<Productos> productos = productopArray.ToList();
                var tbx = sender as TextBox;
                if (tbx != null)
                {
                    var searchText = tbx.Text.ToLower();
                    var filtrarLista = productos.Where(x => x.Nombre.ToLower().Contains(searchText)).ToList();
                    dgListaProductos.ItemsSource = null;
                    dgListaProductos.ItemsSource = filtrarLista;
                }
                else
                {
                    dgListaProductos.ItemsSource = productos;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }
    }
}
