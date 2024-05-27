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
    /// Interaction logic for ListaProveedores.xaml
    /// </summary>
    public partial class ListaProveedores : UserControl
    {
        SupplierManagerClient proveedorServer = new SupplierManagerClient();

        public ListaProveedores()
        {
            InitializeComponent();
            MostrarInformacionProveedores();

        }

        private void ActualizarTablaProveedores(object sender, EventArgs e)
        {
            MostrarInformacionProveedores();
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Finanzas();
        }

        private void BtnRegistrarProveedor_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProveedor registrarProveedor = new RegistrarProveedor();
            registrarProveedor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registrarProveedor.Closed += ActualizarTablaProveedores;
            registrarProveedor.ShowDialog();
        }

        private void DgListaProveedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgListaProveedores.SelectedItem != null)
            {
                Proveedor proveedorSeleccionado = dgListaProveedores.SelectedItem as Proveedor;
                ConsultarProveedor consultarProveedor = new ConsultarProveedor(proveedorSeleccionado);
                consultarProveedor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                consultarProveedor.Closed += ActualizarTablaProveedores;
                consultarProveedor.Show();
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Proveedor[] proveedorArray = proveedorServer.ObtenerListaProveedor();
            List<Proveedor> proveedores = proveedorArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = proveedores.Where(x => x.NombreContacto.Contains(tbx.Text)).ToList();
                dgListaProveedores.ItemsSource = null;
                dgListaProveedores.ItemsSource = filtrarLista;
            }
            else
            {
                dgListaProveedores.ItemsSource = proveedores;
            }
        }

        private void MostrarInformacionProveedores()
        {
            try
            {
                Proveedor[] proveedorArray = proveedorServer.ObtenerListaProveedor();
                List<Proveedor> proveedor = proveedorArray.ToList();

                dgListaProveedores.ItemsSource = proveedor;

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

        private void dgListaProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
