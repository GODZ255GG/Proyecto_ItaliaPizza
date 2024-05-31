using Logs;
using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarProducto.xaml
    /// </summary>
    public partial class ConsultarProducto : Window
    {
        ProductManagerClient client = new ProductManagerClient();
        OrderManagerClient orderClient = new OrderManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

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
            if (!ValidarProductoAsociado(idProducto))
            {
                return;
            }

            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el producto?", "Confirmar eliminación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    client.EliminarProducto(idProducto);
                    Utilidades.Utilidades.MostrarMensaje("Producto eliminado exitosamente", "Eliminación exitosa", MessageBoxImage.Information);
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

        private void AccionModificar()
        {
            var nuevoNombre = tbxNombre.Text;
            var nuevaMarca = tbxMarca.Text;
            var nuevoTipo = cbxTipo.Text;
            var nuevoPrecio = tbxPrecio.Text;
            var nuevoCodigo = tbxCodigoProducto.Text;

            if (!IsValidDecimal(tbxPrecio.Text))
            {
                MessageBox.Show("Ingrese un número válido en el campo del precio", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                !Regex.IsMatch(marca, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
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

        private void TbxNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbxMarca_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbxPrecio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsValidDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsValidDecimal(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[0-9]*\.?[0-9]{0,2}$");
        }

        private bool ValidarProductoAsociado(int idProducto)
        {
            try
            {
                var productosAsociados = orderClient.RecuperarIdsProductosDePedidos();
                if (productosAsociados.Contains(idProducto))
                {
                    Utilidades.Utilidades.MostrarMensaje("No se puede eliminar el producto porque está asociado a un pedido.", "Eliminación no permitida", MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
                return false;
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
                return false;
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
