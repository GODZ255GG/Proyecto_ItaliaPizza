using System;
using System.Collections.Generic;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {
        private int telefono = 0;

        public RegistrarUsuario()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
        }
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Gerente",
                "Cajero",
                "Cocinero",
                "Cliente"
            };
            cbxTipo.ItemsSource = items;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = tbxNombre.Text;
            string apellidoPaterno = tbxApellidoP.Text;
            string apellidoMaterno = tbxApellidoM.Text;
            string correo = tbxCorreo.Text;
            string contraseña = txtPassword.Password;
            string rol = cbxTipo.Text;
            ItaliaPizzaServer.UserManagerClient user = new ItaliaPizzaServer.UserManagerClient();

            ItaliaPizzaServer.Empleados nuevoUsuario = new ItaliaPizzaServer.Empleados()
            {

                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Correo = correo,
                Contraseña = contraseña,
                Rol = rol


            };

            var result = false;


            if (!user.UsuarioYaRegistrado(correo))
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Este usuario ya se encuentra registrado en el sistema, intente con otro", "Usuario duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (result)
            {
                var aux = user.RegistrarEmpleado(nuevoUsuario);
                if (aux)
                {
                    MessageBox.Show("El usuario se ha registrado exitosamente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo registrar el usuario", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


    }
}

