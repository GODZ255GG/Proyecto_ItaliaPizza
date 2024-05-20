using Domain;
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
    /// Interaction logic for ListaPedidos.xaml
    /// </summary>
    public partial class ListaRecetas : UserControl
    {
        ItaliaPizzaServer.RecipeManagerClient recetasServer = new ItaliaPizzaServer.RecipeManagerClient();
        public ListaRecetas()
        {
            InitializeComponent();
            CargarRecetas();
        }

        private void ActualizarTablaRecetas(object sender, EventArgs e)
        {
            CargarRecetas();
        }

        private void CargarRecetas()
        {
            try
            {
                var recetasDesdeBD = recetasServer.ObtenerListaReceta();
                dgRecetas.ItemsSource = recetasDesdeBD;
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

        private void BtnRegistrarRecetas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrarRecetas registrarReceta = new RegistrarRecetas();
                registrarReceta.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                registrarReceta.Closed += ActualizarTablaRecetas;

                this.IsEnabled = false;

                bool? result = registrarReceta.ShowDialog();

                this.IsEnabled = true;
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

        private void DgRecetas_MouseDobleClic(object sender, MouseButtonEventArgs e)
        {
            if (dgRecetas.SelectedItem != null)
            {
                var recetaSeleccionado = (Recetas)dgRecetas.SelectedItem;

                ConsultarReceta detallesReceta = new ConsultarReceta(recetaSeleccionado);
                detallesReceta.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                detallesReceta.Closed += ActualizarTablaRecetas;

                this.IsEnabled = false;

                bool? result = detallesReceta.ShowDialog();

                this.IsEnabled = true;
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recetas[] recetaArray = recetasServer.ObtenerListaReceta();
            List<Recetas> recetas = recetaArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {

                var filtrarLista = recetas.Where(x => x.Nombre.Contains(tbx.Text)).ToList();
                dgRecetas.ItemsSource = null;
                dgRecetas.ItemsSource = filtrarLista;
            }
            else
            {
                dgRecetas.ItemsSource = recetas;
            }
        }
    }
}
