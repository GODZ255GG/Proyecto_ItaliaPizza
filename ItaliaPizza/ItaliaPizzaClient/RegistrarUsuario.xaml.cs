using System;
using Logs;
using log4net;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using ItaliaPizzaClient.ItaliaPizzaServer;


namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {
        private static readonly ILog Log = Logger.GetLogger();
        private int telefono = 0;

        public RegistrarUsuario()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
        }
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
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

            if (!CamposVacios())
            {
                if (StringValidos(nombre, apellidoPaterno, apellidoMaterno, correo, contraseña))
                {
                    try
                    {
                        AccionRegistrar();
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
                    Utilidades.Utilidades.MostrarMensajeCamposInvalidos();
                }
            }
            else
            {
                Utilidades.Utilidades.MostrarMensajeCamposVacios();
            }
        }


        private void AccionRegistrar()
        {

            var nombre = tbxNombre.Text;
            var apellidoPaterno = tbxApellidoP.Text;
            var apellidoMaterno = tbxApellidoM.Text;
            var correo = tbxCorreo.Text;
            var contraseña = txtPassword.Password;
            var rol = cbxTipo.Text;


            if (rol == "Cliente" ) {

                ItaliaPizzaServer.UserManagerClient client = new ItaliaPizzaServer.UserManagerClient();
                ItaliaPizzaServer.Cliente nuevoCliente = new ItaliaPizzaServer.Cliente()
                {
                    Nombre = nombre,
                    Rol = rol
                };
                client.RegistrarClientes(nuevoCliente);
                Utilidades.Utilidades.MostrarMensaje("El Cliente se registro exitosamente; ", "Registro exitoso", MessageBoxImage.Information);
                this.Close();


            }
            else {


                ItaliaPizzaServer.UserManagerClient user = new ItaliaPizzaServer.UserManagerClient();

                ItaliaPizzaServer.Empleados nuevoUsuario = new ItaliaPizzaServer.Empleados()
                {
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Correo = correo,
                    Contraseña = contraseña,
                    Rol = rol,
                };


                if (user.UsuarioYaRegistrado(correo))
                {
                    Utilidades.Utilidades.MostrarMensaje("Este correo  ya se encuentra registrado en el sistema, intente con otro", "Correo ya registrado ", MessageBoxImage.Error);
                    return;
                }

                user.RegistrarEmpleado(nuevoUsuario);
                Utilidades.Utilidades.MostrarMensaje("El Usuario se ha registrado exitosamente: ", "Registro exitoso", MessageBoxImage.Information);
                this.Close();


            }
        }


            #region Validaciones
            public bool CamposVacios()
            {
                if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxApellidoP.Text) ||
                    string.IsNullOrEmpty(tbxApellidoM.Text) || string.IsNullOrEmpty(tbxCorreo.Text) ||
                    string.IsNullOrEmpty(txtPassword.Password))
                {
                    return true;
                }
                return false;
            }

            private bool StringValidos(string nombre, string apellidoP, string apellidoM, string correo, string contrasena)
            {
                if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                    !Regex.IsMatch(apellidoP, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                    !Regex.IsMatch(apellidoM, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                    return false;
                }

                if (nombre.Length > 45 || apellidoP.Length > 45 || apellidoM.Length > 45 )
                {
                    return false;
                }

                return true;
            }
            #endregion
        }
    }



