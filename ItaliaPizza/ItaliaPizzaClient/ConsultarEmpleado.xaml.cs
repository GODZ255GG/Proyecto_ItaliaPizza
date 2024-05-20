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
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarEmpleado.xaml
    /// </summary>
    public partial class ConsultarEmpleado : UserControl
    {
        UserManagerClient usuarioServer = new UserManagerClient();
        public ConsultarEmpleado()
        {
            InitializeComponent();
            MostrarInformacionUsuarios();
        }

        private void ActualizarTablaEmpleados(object sender, EventArgs e)
        {
            MostrarInformacionUsuarios();
        }
        private void ImgRegresar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void MostrarInformacionUsuarios()
        {
            try
            {
                Empleados[] usuarioArray = usuarioServer.ObtenerListaUsuarios();
                List<Empleados> usuarios = usuarioArray.ToList();

                dgListaUsuarios.ItemsSource = usuarios;
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

        private void DgListaUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgListaUsuarios.SelectedItem != null)
            {
                Empleados empleadoSeleccionado = dgListaUsuarios.SelectedItem as Empleados;
                ModificarEmpleado modificarEmpleado = new ModificarEmpleado(empleadoSeleccionado);
                modificarEmpleado.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                modificarEmpleado.Closed += ActualizarTablaEmpleados;
                modificarEmpleado.Show();
            }
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario RegistrarUsuario = new RegistrarUsuario();
            RegistrarUsuario.Show();

        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Empleados[] empleadoArray = usuarioServer.ObtenerListaUsuarios();
            List<Empleados> empleados = empleadoArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = empleados.Where(x => x.Nombre.Contains(tbx.Text)).ToList();
                dgListaUsuarios.ItemsSource = null;
                dgListaUsuarios.ItemsSource = filtrarLista;
            }
            else
            {
                dgListaUsuarios.ItemsSource = empleados;
            }
        }


    }
}
