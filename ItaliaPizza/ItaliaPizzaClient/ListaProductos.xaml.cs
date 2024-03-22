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
    /// Lógica de interacción para ListaProductos.xaml
    /// </summary>
    public partial class ListaProductos : UserControl
    {
        ProductManagerClient productoServer = new ProductManagerClient();
        public ListaProductos()
        {
            InitializeComponent();
            MostrarInformacionPedidos();
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrar = new RegistrarProducto();
            registrar.Show();
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

                lbxProductos.ItemsSource = productos;
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

        private void LbxProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lbxProductos.SelectedItem != null)
            {
                Productos productoSeleccionado = lbxProductos.SelectedItem as Productos;
                ConsultarProducto consulta = new ConsultarProducto(productoSeleccionado);
                consulta.Show();
            }
        }
    }
}
