using ItaliaPizzaClient.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {

        public RegistrarProducto()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
            tbxCodigoProducto.Text = "PD" + Utilidades.Utilidades.GenerarCodigo();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Utilidades.MostrarMensajeConfirmacionRegresar())
            {
                this.Close();
            }
            
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Pizza",
                "Bebida",
                "Postre",
                "Pasta"
            };
            cbxTipo.ItemsSource = items;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = tbxNombre.Text;
            var marca = tbxMarca.Text;

            if (!CamposVacios())
            {
                if (StringValidos(nombre, marca))
                {
                    try
                    {
                        AccionRegistrar();
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
                    }
                    catch (CommunicationException ex)
                    {
                        Utilidades.Utilidades.MostrarMensajeCommunicationException();
                    }
                    catch (TimeoutException ex)
                    {
                        Utilidades.Utilidades.MostrarMensajeTimeoutException();
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier otra excepción no específica
                        MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButton.OK ,MessageBoxImage.Error);
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
            var codigoProducto = tbxCodigoProducto.Text;
            var marca = tbxMarca.Text;
            var tipo = cbxTipo.Text;
            var precio = tbxPrecio.Text;

            if (!double.TryParse(precio, out double precioDouble))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Error de formato del precio", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ItaliaPizzaServer.ProductManagerClient client = new ItaliaPizzaServer.ProductManagerClient();

            ItaliaPizzaServer.Productos nuevoProducto = new ItaliaPizzaServer.Productos()
            {
                Nombre = nombre,
                CodigoProducto = codigoProducto,
                Marca = marca,
                Tipo = tipo,
                Precio = precioDouble
            };

            if (client.ProductoYaRegistrado(nombre))
            {
                MessageBox.Show("Este producto ya se encuentra registrado en el sistema, intente con otro", "Producto duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            client.RegistrarProducto(nuevoProducto);
            MessageBox.Show("El producto se ha registrado exitosamente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxMarca.Text) ||
                string.IsNullOrEmpty(tbxPrecio.Text) || string.IsNullOrEmpty(tbxCodigoProducto.Text) ||
                string.IsNullOrEmpty(cbxTipo.Text))
            {
                return true;
            }
            return false;
        }

        private bool StringValidos(string nombre, string marca)
        {
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                !Regex.IsMatch(marca, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return false;
            }

            if (nombre.Length > 45 || marca.Length > 45)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
