using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            MostrarInformacionUsuario();
        }

        private void MostrarInformacionUsuario()
        {
            lbNombreUsuario.Content = ("Hola " + Domain.Usuarios.UsuariosClient.Nombre);
        }

        private void ImgConfiguracion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Configuracion confi = new Configuracion();
            confi.Show();
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }
    }
}
