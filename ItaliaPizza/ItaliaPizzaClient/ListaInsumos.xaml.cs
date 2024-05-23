using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using Logs;
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
    /// Lógica de interacción para ListaInsumos.xaml
    /// </summary>
    public partial class ListaInsumos : UserControl
    {
        InsumoManagerClient insumoServer = new InsumoManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

        public ListaInsumos()
        {
            InitializeComponent();
            MostrarInformacionInsumos();
        }

        private void MostrarInformacionInsumos()
        {
            try
            {
                Insumos[] insumoArray = insumoServer.ObtenerListaInsumos();
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

        private void ImgRegresar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contentControl.Content = new Inventario();
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
                    ConsultarInsumo consulta = new ConsultarInsumo(insumoSeleccionado);
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

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Insumos[] insumosArray = insumoServer.ObtenerListaInsumos();
                List<Insumos> insumos = insumosArray.ToList();
                var tbx = sender as TextBox;
                if (tbx != null)
                {
                    var searchText = tbx.Text.ToLower();
                    var filtrarLista = insumos.Where(x => x.Nombre.ToLower().Contains(searchText)).ToList();
                    dgListaInsumos.ItemsSource = null;
                    dgListaInsumos.ItemsSource = filtrarLista;
                }
                else
                {
                    dgListaInsumos.ItemsSource = insumos;
                }
            }
            catch(EndpointNotFoundException ex)
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