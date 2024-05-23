using Logs;
using log4net;
using System;
using System.Collections.Generic;
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
        private static readonly ILog Log = Logger.GetLogger();
        public RegistrarProducto()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
            tbxCodigoProducto.Text = "PD" + Utilidades.Utilidades.GenerarCodigo();
            tbxMarca.Text = "Italia Pizza";
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
                        Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error",MessageBoxImage.Error);
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
                Utilidades.Utilidades.MostrarMensaje("El precio debe ser un número válido.", "Error de formato del precio", MessageBoxImage.Error);
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
                Utilidades.Utilidades.MostrarMensaje("Este producto ya se encuentra registrado en el sistema, intente con otro", "Producto duplicado", MessageBoxImage.Error);
                return;
            }

            client.RegistrarProducto(nuevoProducto);
            Utilidades.Utilidades.MostrarMensaje("El producto se ha registrado exitosamente: ", "Registro exitoso", MessageBoxImage.Information);
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
