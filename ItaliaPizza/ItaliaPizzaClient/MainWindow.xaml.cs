using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ItaliaPizzaServer.UserManagerClient client = new ItaliaPizzaServer.UserManagerClient();
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
            var correo = tbCorreo.Text;
            var contraseña = pbContraseña.Password;
            if (!String.IsNullOrWhiteSpace(correo) && !String.IsNullOrWhiteSpace(contraseña))
            {
                if (StringValidos(correo, contraseña))
                {
                    if(StringLargos(correo, contraseña))
                    {
                        try
                        {
                            IniciarSesionAction(correo, contraseña);
                        }
                        catch (EndpointNotFoundException ex)
                        {
                            MessageBox.Show("No se pudo acceder al punto final (endpoint) requerido. Intente de nuevo", "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (CommunicationException ex)
                        {
                            MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (TimeoutException ex)
                        {
                            MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        finally
                        {
                            client.Abort();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El correo o la contraseña no son validos. Verifique sus datos","Datos Invalidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El correo o la contraseña no son validos. Verifique sus datos", "Datos Invalidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingrese la información solicitada para continuar", "Campos Vaciíos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void IniciarSesionAction(string correo, string contraseña)
        {
            var usuarioInicioSesion = client.IniciarSesion(correo, contraseña);
            if (usuarioInicioSesion != null)
            {
                if (usuarioInicioSesion.Status)
                {
                    Domain.Usuarios.UsuariosClient = new Domain.Usuarios()
                    {
                        IdUsuarios = usuarioInicioSesion.IdUsuarios,
                        Nombre = usuarioInicioSesion.Nombre,
                        ApellidoPaterno = usuarioInicioSesion.ApellidoPaterno,
                        ApellidoMaterno = usuarioInicioSesion.ApellidoMaterno,
                        Telefono = usuarioInicioSesion.Telefono,
                        Correo = usuarioInicioSesion.Correo,
                        Foto = usuarioInicioSesion.Foto,
                        Rol = usuarioInicioSesion.Rol
                    };

                    string nombre = Domain.Usuarios.UsuariosClient.Nombre;
                    MessageBox.Show("Bienvenido " + nombre, "Inicio de Sesión exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                    VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
                    ventanaPrincipal.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No se puede iniciar sesion","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Validaciones
        private bool StringValidos(string correo, string contraseña)
        {
            var esValido = false;
            if(Regex.IsMatch(correo, "^[a-zA-Z0-9._%+-]+@(hotmail|outlook|gmail)\\.com$") && Regex.IsMatch(contraseña, "^[a-zA-Z0-9]*$"))
            {
                esValido = true;
            }
            return esValido;
        }
        private bool StringLargos(string correo, string contraseña)
        {
            var noSonLargos = false;
            if (correo.Length <= 50 || contraseña.Length <=13 )
            {
                noSonLargos = true;
            }
            return noSonLargos;
        }
        #endregion
    }
}
