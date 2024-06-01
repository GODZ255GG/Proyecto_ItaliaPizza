using System;
using Logs;
using log4net;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using ItaliaPizzaClient.ItaliaPizzaServer;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {
        private static readonly ILog Log = Logger.GetLogger();

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

        private void cbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTipo.SelectedItem != null && cbxTipo.SelectedItem.ToString() == "Cliente")
            {
                tbxApellidoP.Visibility = Visibility.Collapsed;
                tbxApellidoM.Visibility = Visibility.Collapsed;
                tbxCorreo.Visibility = Visibility.Collapsed;
                txtPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbxApellidoP.Visibility = Visibility.Visible;
                tbxApellidoM.Visibility = Visibility.Visible;
                tbxCorreo.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Visible;
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = tbxNombre.Text;
            string apellidoPaterno = tbxApellidoP.Text;
            string apellidoMaterno = tbxApellidoM.Text;
            string correo = tbxCorreo.Text;
            string telefono = tbxTelefono.Text;
            string contraseña = txtPassword.Password;
            string rol = cbxTipo.Text;

            if (!CamposVacios())
            {
                string mensajeError;
                if (StringValidos(nombre, apellidoPaterno, apellidoMaterno, correo, telefono, contraseña, rol, out mensajeError))
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
                    Utilidades.Utilidades.MostrarMensaje(mensajeError, "Datos Inválidos", MessageBoxImage.Warning);
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
            var telefono = tbxTelefono.Text;
            var contraseña = txtPassword.Password;
            var rol = cbxTipo.Text;

            if (rol == "Cliente")
            {
                ItaliaPizzaServer.UserManagerClient client = new ItaliaPizzaServer.UserManagerClient();
                ItaliaPizzaServer.Cliente nuevoCliente = new ItaliaPizzaServer.Cliente()
                {
                    Nombre = nombre,
                    Telefono = telefono,
                    Rol = rol
                };
                client.RegistrarClientes(nuevoCliente);
                Utilidades.Utilidades.MostrarMensaje("El Cliente se registró exitosamente", "Registro exitoso", MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                ItaliaPizzaServer.UserManagerClient user = new ItaliaPizzaServer.UserManagerClient();

                ItaliaPizzaServer.Empleados nuevoUsuario = new ItaliaPizzaServer.Empleados()
                {
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Correo = correo,
                    Telefono = telefono,
                    Contraseña = contraseña,
                    Rol = rol,
                };

                if (user.UsuarioYaRegistrado(correo))
                {
                    Utilidades.Utilidades.MostrarMensaje("Este correo ya se encuentra registrado en el sistema, intente con otro", "Correo ya registrado", MessageBoxImage.Error);
                    return;
                }

                user.RegistrarEmpleado(nuevoUsuario);
                Utilidades.Utilidades.MostrarMensaje("El Usuario se ha registrado exitosamente", "Registro exitoso", MessageBoxImage.Information);
                this.Close();
            }
        }

        #region Validaciones

        public bool CamposVacios()
        {
            if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxApellidoP.Text) ||
                string.IsNullOrEmpty(tbxApellidoM.Text) || string.IsNullOrEmpty(tbxCorreo.Text) ||
                string.IsNullOrEmpty(tbxTelefono.Text) || string.IsNullOrEmpty(txtPassword.Password) ||
                string.IsNullOrEmpty(cbxTipo.Text))
            {
                return true;
            }
            return false;
        }

        private bool StringValidos(string nombre, string apellidoP, string apellidoM, string correo, string telefono, string contrasena, string rol, out string mensajeError)
        {
            mensajeError = string.Empty;

            // Validación de nombres y apellidos
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s\-ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                mensajeError = "El nombre solo puede contener letras.";
                return false;
            }

            if (!Regex.IsMatch(apellidoP, @"^[a-zA-Z\s\-ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                mensajeError = "El apellido paterno solo puede contener letras.";
                return false;
            }

            if (!Regex.IsMatch(apellidoM, @"^[a-zA-Z\s\-ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                mensajeError = "El apellido materno solo puede contener letras.";
                return false;
            }

            // Validación de longitud de nombres y apellidos
            if (nombre.Length > 45)
            {
                mensajeError = "El nombre no puede tener más de 45 caracteres.";
                return false;
            }

            if (apellidoP.Length > 45)
            {
                mensajeError = "El apellido paterno no puede tener más de 45 caracteres.";
                return false;
            }

            if (apellidoM.Length > 45)
            {
                mensajeError = "El apellido materno no puede tener más de 45 caracteres.";
                return false;
            }

            // Validación del correo
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensajeError = "El correo electrónico no es válido.";
                return false;
            }

            // Validación del teléfono
            if (!Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                mensajeError = "El teléfono debe contener exactamente 10 dígitos.";
                return false;
            }

            // Validación de la contraseña
            mensajeError = ContraseñaValida(contrasena);
            if (!string.IsNullOrEmpty(mensajeError))
            {
                return false;
            }

            // Validación del rol
            if (string.IsNullOrEmpty(rol))
            {
                mensajeError = "Debe seleccionar un rol.";
                return false;
            }

            return true;
        }

        private string ContraseñaValida(string contrasena)
        {
            if (contrasena.Length < 8 || contrasena.Length > 12)
            {
                return "La contraseña debe tener entre 8 y 12 caracteres.";
            }

            if (!Regex.IsMatch(contrasena, @"[A-Z]"))
            {
                return "La contraseña debe contener al menos una letra mayúscula.";
            }

            if (!Regex.IsMatch(contrasena, @"[a-z]"))
            {
                return "La contraseña debe contener al menos una letra minúscula.";
            }

            if (!Regex.IsMatch(contrasena, @"[0-9]"))
            {
                return "La contraseña debe contener al menos un número.";
            }

            return string.Empty;
        }



        private void tbxTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]+$");
        }

        private void tbxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxTelefono.Text.Length > 10)
            {
                tbxTelefono.Text = tbxTelefono.Text.Substring(0, 10);
                tbxTelefono.CaretIndex = tbxTelefono.Text.Length;
            }
        }

        #endregion
    }
}
