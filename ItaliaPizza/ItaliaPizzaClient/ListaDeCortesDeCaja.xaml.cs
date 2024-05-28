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

        public ListaDeCortesDeCaja()
        {
            InitializeComponent();
            CargarcCortesDeCaja();
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