using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarProducto.xaml
    /// </summary>
    public partial class ConsultarProducto : Window
    {
        ProductManagerClient client = new ProductManagerClient();

        private int idProducto;
        private string nombre;
        private string marca;
        private string tipo;
        private string precio;
        private string codigo;

        public ConsultarProducto(Productos producto)
        {
            InitializeComponent();

            Loaded += CbxTipo_Loaded;

            idProducto = producto.IdProductos;
            nombre = producto.Nombre;
            marca = producto.Marca;
            tipo = producto.Tipo;
            precio = producto.Precio.ToString();
            codigo = producto.CodigoProducto;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxPrecio.Text = precio;
            tbxCodigoProducto.Text = codigo;
            cbxTipo.SelectedItem = tipo;
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el producto?", "Confirmar eliminación", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try{
                    client.EliminarProducto(idProducto);
                    Utilidades.Utilidades.MostrarMensaje("Producto eliminado exitosamente", "Eliminación exitosa", MessageBoxImage.Information);
                    this.Close();
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
                    Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
                }
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {

            Title = "Modificar Producto";
            lbTitulo.Content = "Modificar Producto";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = false;
            tbxMarca.IsReadOnly = false;
            tbxPrecio.IsReadOnly = false;
            cbxTipo.IsEnabled = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Utilidades.MostrarMensajeConfirmacionCancelar())
            {
                Title = "Consultar Producto";
                lbTitulo.Content = "Información del Producto";

                btnAceptar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
                btnModificar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;

                tbxNombre.IsReadOnly = true;
                tbxMarca.IsReadOnly = true;
                tbxPrecio.IsReadOnly = true;
                tbxCodigoProducto.IsReadOnly = true;
                cbxTipo.IsEnabled = false;

                tbxNombre.Text = nombre;
                tbxMarca.Text = marca;
                tbxPrecio.Text = precio;
                tbxCodigoProducto.Text = codigo;
                cbxTipo.SelectedItem = tipo;
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                if (StringValidos(nombre, marca))
                {
                    try
                    {
                        AccionModificar();
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

        private void AccionModificar()
        {
            var nuevoNombre = tbxNombre.Text;
            var nuevaMarca = tbxMarca.Text;
            var nuevoTipo = cbxTipo.Text;
            var nuevoPrecio = tbxPrecio.Text;
            var nuevoCodigo = tbxCodigoProducto.Text;

            if (!double.TryParse(nuevoPrecio, out double precioDouble))
            {
                Utilidades.Utilidades.MostrarMensaje("El precio debe ser un número válido.", "Error de formato del precio", MessageBoxImage.Error);
                return;
            }

            client.ActualizarProducto(idProducto, nuevoNombre, nuevoCodigo, nuevaMarca, nuevoTipo, precioDouble);
            Utilidades.Utilidades.MostrarMensaje("Producto se ha actualizado exitosamente", "Actualización exitosa", MessageBoxImage.Information);
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
