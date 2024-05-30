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
    public partial class Stock : UserControl
    {
        ProductManagerClient productoServer = new ProductManagerClient();
        InsumoManagerClient InsumoManagerClient = new InsumoManagerClient();
        private static readonly ILog Log = Logger.GetLogger();
        public Stock()
        {
            InitializeComponent();
            MostrarInformacionPedidos();
            MostrarInformacionInsumos();
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
                    RegistrarStockProducto consulta = new RegistrarStockProducto(productoSeleccionado);
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

        private void MostrarInformacionInsumos()
        {
            try
            {
                Insumos[] insumoArray = InsumoManagerClient.ObtenerListaInsumos();
                List<Insumos> productos = insumoArray.ToList();

                dgListaInsumos.ItemsSource = productos;

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



        private void BtnRegistrarInsumo_Click(object sender, RoutedEventArgs e)
        {
            RegistrarInsumo registrar = new RegistrarInsumo();
            registrar.Closed += ActualizarTablaInsumos;
            registrar.Show();
        }

        private void ActualizarTablaInsumos(object sender, EventArgs e)
        {
            MostrarInformacionInsumos();
        }

        private void DgListaInsumos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgListaInsumos.SelectedItem != null)
                {
                    Insumos insumoSeleccionado = dgListaInsumos.SelectedItem as Insumos;
                    RegistrarStockInsumo consulta = new RegistrarStockInsumo(insumoSeleccionado);
                    consulta.Closed += ActualizarTablaInsumos;
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
    }
}
