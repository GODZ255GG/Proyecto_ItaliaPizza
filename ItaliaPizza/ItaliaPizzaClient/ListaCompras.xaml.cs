using ItaliaPizzaClient.ItaliaPizzaServer;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System;
using System.Windows.Controls;
using log4net;
using Logs;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ListaCompras.xaml
    /// </summary>
    public partial class ListaCompras : UserControl
    {
        private static readonly ILog Log = Logger.GetLogger();
        ItaliaPizzaServer.ProcurementManagerClient compra = new ItaliaPizzaServer.ProcurementManagerClient();
        public ListaCompras()
        {
            InitializeComponent();
            CargarCompras();
        }

        private void CargarCompras()
        {
            try
            {
                var comprasBD = compra.RecuperarInformacionCompras();
                dgListaCompras.ItemsSource = comprasBD;

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

        private void ActualizarLista(object sender, EventArgs e)
        {
            CargarCompras();
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contentControl.Content = new Finanzas();
        }

        private void BtnRegistrarCompra_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RegistrarCompraInsumo registrar = new RegistrarCompraInsumo();
            registrar.Closed += ActualizarLista;
            registrar.Show();
        }
    }
}
