using ItaliaPizzaClient.ItaliaPizzaServer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for ListaDeCortesDeCaja.xaml
    /// </summary>
    public partial class ListaDeCortesDeCaja : UserControl
    {
        ItaliaPizzaServer.CashRecordClient cortesDeCajaServer = new ItaliaPizzaServer.CashRecordClient();
        private string rol = Domain.Empleados.EmpleadosClient.Rol;
        

        public ListaDeCortesDeCaja()
        {
            InitializeComponent();
            CargarcCortesDeCaja();

            if (rol == "Gerente")
            {
                btnRegistrarCorteDeCaja.Visibility = Visibility.Hidden;
        }
        }

        private void ActualizarTablaCortesDeCaja(object sender, EventArgs e)
        {
            CargarcCortesDeCaja();
        }

        private void CargarcCortesDeCaja()
        {
            try
            {
                var cortesDesdeBD = cortesDeCajaServer.RecuperarInformacionDeCortesDeCaja();
                dgCortesDeCaja.ItemsSource = cortesDesdeBD;
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

        private void btnRegistrarCorteDeCaja_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCorteDeCaja registrarCorteDeCaja = new RegistrarCorteDeCaja();
            registrarCorteDeCaja.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registrarCorteDeCaja.Closed += ActualizarTablaCortesDeCaja;
            registrarCorteDeCaja.ShowDialog();

        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            CorteDeCaja[] cortesDeCajaArray = cortesDeCajaServer.RecuperarInformacionDeCortesDeCaja();
            List<CorteDeCaja> corteDeCaja = cortesDeCajaArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = corteDeCaja.Where(x => x.FechaCorteDeCaja.ToString("dd/MM/yyyy").Contains(tbx.Text)).ToList();
                dgCortesDeCaja.ItemsSource = null;
                dgCortesDeCaja.ItemsSource = filtrarLista;
            }
            else
            {
                dgCortesDeCaja.ItemsSource = corteDeCaja;
            }
        }
    }
}