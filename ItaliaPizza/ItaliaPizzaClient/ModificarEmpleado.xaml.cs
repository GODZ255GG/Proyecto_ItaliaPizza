using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;


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
        private string contrasena;
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

        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este Usuario?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {

                usuario.EliminarEmpleados(idEmpleados);
                MessageBox.Show("Usuario eliminado correctamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();


            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre;
            string nuevoApellidoP;
            string nuevoApellidoM;
            string nuevoRol;
            string nuevoCorreo;
            string nuevaContrasena;

            nuevoNombre = tbxNombre.Text;
            nuevoApellidoP = tbxApellidoP.Text;
            nuevoApellidoM = tbxApellidoM.Text;
            nuevoRol = cbxTipo.Text;
            nuevoCorreo = tbxCorreo.Text;
            nuevaContrasena = txtPassword.Password;

            Debug.WriteLine($"Nombre: {tbxNombre.Text}");
            Debug.WriteLine($"Apellido Paterno: {tbxApellidoP.Text}");
            Debug.WriteLine($"Apellido Materno: {tbxApellidoM.Text}");
            Debug.WriteLine($"Correo: {tbxCorreo.Text}");
            Debug.WriteLine($"Tipo: {cbxTipo.Text}");

            if (CamposVacios())
            {
                if (StringValidos(nuevoNombre, nuevoApellidoP, nuevoApellidoM) && StringLargos(nuevoNombre, nuevoApellidoP, nuevoApellidoM))
                {
                    try
                    {
                        usuario.ActualizarEmpleado(idEmpleados, nuevoNombre, nuevoApellidoP, nuevoApellidoM, nuevoCorreo, nuevaContrasena);
                        MessageBox.Show("El cliente se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
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
                else
                {
                    MessageBox.Show("Los datos ingresados no son validos. Verifique sus datos", "Datos Invalidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingrese la información solicitada para continuar", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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


        #region Validaciones
        public bool CamposVacios()
        {
            if (tbxNombre.Text == string.Empty || tbxApellidoP.Text == string.Empty || tbxApellidoM.Text == string.Empty
                || tbxCorreo.Text == string.Empty || cbxTipo.Text == string.Empty)
            {
                return false;
            }
            return true;
        }


        public bool StringValidos(string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            if (nombre == null || apellidoPaterno == null || apellidoMaterno == null)
            {
                return false; // Devuelve false si alguno es nulo.
            }

            // Comprobaciones de regex...
            if (Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s]+$") &&
                Regex.IsMatch(apellidoPaterno, @"^[a-zA-Z0-9]+$") &&
                Regex.IsMatch(apellidoMaterno, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return true;
            }

            return false;
        }

        private bool StringLargos(string nombre, string apellidoP, string apellidoM)
        {
            if (nombre.Length <= 45 || apellidoP.Length <= 45 || apellidoM.Length <= 45)
            {
                return true;
            }
            return false;
        }
        #endregion


    }
}

