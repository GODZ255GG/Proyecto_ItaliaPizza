using log4net;
using System;
using Logs;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ItaliaPizzaServer.UserManagerClient client = new ItaliaPizzaServer.UserManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            var correo = tbxCorreo.Text;
            var contraseña = pbxContraseña.Password;

            if (pbxContraseña.Visibility == Visibility.Collapsed)
            {
                contraseña = tbxContraseña.Text;
            }

            if (!CamposVacios())
            {
                if (StringValidos(correo, contraseña))
                {
                    try
                    {
                        IniciarSesionAction(correo, contraseña);
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
                else
                {
                    Utilidades.Utilidades.MostrarMensaje("El correo o la contraseña no son validos. Verifique sus datos", 
                        "Datos Invalidos", MessageBoxImage.Warning);
                }
            }
            else
            {
                Utilidades.Utilidades.MostrarMensajeCamposVacios();
            }
        }

        private void IniciarSesionAction(string correo, string contraseña)
        {
            var usuarioInicioSesion = client.IniciarSesion(correo, contraseña);
            if (usuarioInicioSesion != null && usuarioInicioSesion.Status)
            {
                Domain.Empleados.EmpleadosClient = new Domain.Empleados()
                {
                    IdEmpleados = usuarioInicioSesion.IdEmpleados,
                    Nombre = usuarioInicioSesion.Nombre,
                    ApellidoPaterno = usuarioInicioSesion.ApellidoPaterno,
                    ApellidoMaterno = usuarioInicioSesion.ApellidoMaterno,
                    Telefono = usuarioInicioSesion.Telefono,
                    Correo = usuarioInicioSesion.Correo,
                    Foto = usuarioInicioSesion.Foto,
                    Rol = usuarioInicioSesion.Rol
                };

                string nombre = Domain.Empleados.EmpleadosClient.Nombre;
                Utilidades.Utilidades.MostrarMensaje("Bienvenido " + nombre, "Inicio de Sesión exitoso", MessageBoxImage.Information);

                VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
                ventanaPrincipal.Show();
                this.Close();
            }
            else
            {
                Utilidades.Utilidades.MostrarMensaje("No se puede iniciar sesión. Verifique sus credenciales.", "Error", MessageBoxImage.Error);
            }
        }

        private void ImgVerContraseña_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (pbxContraseña.Visibility == Visibility.Visible)
            {
                tbxContraseña.Visibility = Visibility.Visible;
                pbxContraseña.Visibility = Visibility.Collapsed;
                imgVerContraseña.Visibility= Visibility.Visible;
                imgOcultarContraseña.Visibility = Visibility.Collapsed;
                tbxContraseña.Text = pbxContraseña.Password;
            }
            else
            {
                tbxContraseña.Visibility = Visibility.Collapsed;
                pbxContraseña.Visibility = Visibility.Visible;
                imgVerContraseña.Visibility = Visibility.Collapsed;
                imgOcultarContraseña.Visibility = Visibility.Visible;
                pbxContraseña.Password = tbxContraseña.Text;
            }
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (string.IsNullOrEmpty(tbxCorreo.Text) || string.IsNullOrEmpty(pbxContraseña.Password))
            {
                return true;
            }
            return false;
        }

        private bool StringValidos(string correo, string contraseña)
        {
            if (!Regex.IsMatch(correo, "^[a-zA-Z0-9._%+-]+@(hotmail|outlook|gmail)\\.com$") ||
                !Regex.IsMatch(contraseña, "^[a-zA-Z0-9]*$"))
            {
                return false;
            }

            if (correo.Length > 50 || contraseña.Length > 13)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
