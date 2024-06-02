using ItaliaPizzaClient.ItaliaPizzaServer;
using Logs;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static ItaliaPizzaClient.MainWindow;


namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ModificarUsuario.xaml
    /// </summary>
    public partial class ModificarEmpleado : Window
    {
        UserManagerClient usuario = new UserManagerClient();
        private int idEmpleados;
        private string Nombre;
        private string ApellidoPaterno;
        private string ApellidoMaterno;
        private string Correo;
        private string Rol;
        private string contrasena;  // Mantén la contraseña existente aquí
        private static readonly ILog Log = Logger.GetLogger();

        public ModificarEmpleado(Empleados usuarios)
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
            tbxNombre.Text = usuarios.Nombre;
            tbxApellidoP.Text = usuarios.ApellidoPaterno;
            tbxApellidoM.Text = usuarios.ApellidoMaterno;
            tbxCorreo.Text = usuarios.Correo;
            cbxTipo.SelectedItem = usuarios.Rol;
            idEmpleados = usuarios.IdEmpleados;
            contrasena = usuarios.Contraseña;  // Almacena la contraseña existente
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica si el ID del usuario autenticado es igual al ID del usuario que se intenta eliminar
            if (idEmpleados == SesionActual.UsuarioId)
            {
                MessageBox.Show("No puede eliminar su propio usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este Usuario?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    usuario.EliminarEmpleados(idEmpleados);
                    MessageBox.Show("Usuario eliminado correctamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
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
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Gerente",
                "Cocinero",
                "Cajero"
            };
            cbxTipo.ItemsSource = items;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = tbxNombre.Text;
            string apellidoPaterno = tbxApellidoP.Text;
            string apellidoMaterno = tbxApellidoM.Text;
            string correo = tbxCorreo.Text;
            string telefono = tbxTelefono.Text;
            string contraseña = txtPassword.Password;
            string rol = cbxTipo.Text;

            string mensajeError;
            if (StringValidos(nombre, apellidoPaterno, apellidoMaterno, correo, telefono, contraseña, rol, out mensajeError))
            {
                try
                {
                    AccionActualizar(nombre, apellidoPaterno, apellidoMaterno, correo, telefono, rol, contraseña);
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

        

        private void AccionActualizar(string nombre, string apellidoPaterno, string apellidoMaterno, string correo, string telefono, string rol, string contraseña)
        {
            if (string.IsNullOrEmpty(contraseña))
            {
                contraseña = this.contrasena; // Mantén la contraseña actual si no se proporciona una nueva
            }

            if (string.IsNullOrEmpty(telefono))
            {
                telefono = tbxTelefono.Text; // Mantén el teléfono actual si no se proporciona uno nuevo
            }

            usuario.ActualizarEmpleado(idEmpleados, nombre, apellidoPaterno, apellidoMaterno, correo, telefono, rol, contraseña);
            Utilidades.Utilidades.MostrarMensaje("El Usuario se ha actualizado exitosamente", "Actualización exitosa", MessageBoxImage.Information);
            this.Close();
        }

        #region Validaciones

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
            if (!string.IsNullOrEmpty(telefono) && !Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                mensajeError = "El teléfono debe contener exactamente 10 dígitos.";
                return false;
            }

            // Validación de la contraseña
            if (!string.IsNullOrEmpty(contrasena))
            {
                mensajeError = ContraseñaValida(contrasena);
                if (!string.IsNullOrEmpty(mensajeError))
                {
                    return false;
                }
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